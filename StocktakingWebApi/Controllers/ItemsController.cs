using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocktakingWebApi.Models;

namespace StocktakingWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly ApplicationContext database;

        public ItemsController(ApplicationContext context)
        {
            database = context;
        }
        // GET: api/Items
        [HttpGet("GetItems")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }
            return database.Items.Where(r => r.OrganizationId == user.OrganizationId).OrderBy(r => r.Status).ToList();
        }


        // GET api/Items/5
        [HttpGet("GetUserItems")]
        public async Task<ActionResult<IEnumerable<Item>>> GetUserItems(int userID)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return database.Items.Where(r => r.OrganizationId == user.OrganizationId && r.UserId == user.Id).ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>>FindItem([FromBody]string searchstring)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }

            var items = await database.Items.Where(r => (EF.Functions.Like(r.Name.ToLower().Trim(' '), "%" + searchstring.ToLower() + "%", " ") || r.Name.ToLower().Trim(' ') == searchstring.ToLower().Trim(' '))).ToListAsync();


            return items;
        }

    }
}
