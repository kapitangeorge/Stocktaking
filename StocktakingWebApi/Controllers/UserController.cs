using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StocktakingWebApi.Models;



namespace StocktakingWebApi.Controllers
{
    
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        
        private readonly ApplicationContext database;
        private readonly JWTSettings _jwtSettings;

        public UserController(ApplicationContext context, IOptions<JWTSettings> jwtsettings)
        {
            database = context;
            _jwtSettings = jwtsettings.Value;
        }


        // GET: api/<controller>
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);

            if (user == null)
                return NotFound();
            else return user;
        }

         [HttpGet("Login")]
         public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        {
            user = await database.Users.FirstOrDefaultAsync(r => r.Username == user.Username && r.Password == user.Password);

            UserWithToken userWithToken = new UserWithToken(user);

            if (userWithToken == null)
            {
                return NotFound();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMonths(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token = tokenHandler.WriteToken(token);

            return userWithToken;
        }

        // POST api/<controller>
        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, [FromForm]string firstName, [FromForm] string lastName)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Id == id);
            if(user == null && user.Username != User.Identity.Name)
            {
                return BadRequest();
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            database.Update(user);

            await database.SaveChangesAsync();
            return Accepted();
        }

       
    }
}
