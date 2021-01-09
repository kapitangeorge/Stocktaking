using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stocktaking.Data;
using Stocktaking.Data.Models;
using Stocktaking.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stocktaking.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationContext database;

        public AccountController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            string url = Request.Headers["Referer"].ToString();
            string returnUrl = new Regex(@"(.*):(\d*)").Replace(url, string.Empty);
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await database.Users.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Username);

                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await database.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (user == null)
                {
                    
                    database.Users.Add(new User { Username = model.Username, Password = model.Password, FirstName = model.FirstName, LastName = model.LastName, Position = model.Position });
                    await database.SaveChangesAsync();

                    await Authenticate(model.Username); 

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string username)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}

