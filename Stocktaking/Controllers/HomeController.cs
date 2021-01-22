using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stocktaking.Data;
using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext database;

        public HomeController(ApplicationContext context)
        {
            database = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
                if(user == null) RedirectToAction("Login", "Account");
                var items = await database.Items.Where(r => r.OrganizationId == user.OrganizationId && r.UserId == user.Id && r.Status != "Списан").ToListAsync();
                return View(items);
            }
           

            return RedirectToAction("Login","Account");
        }
    }
}
