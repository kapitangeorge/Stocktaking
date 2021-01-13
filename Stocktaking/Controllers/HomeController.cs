using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = database.Users.FirstOrDefault(r => r.Username == User.Identity.Name);
                var items = database.Items.Where(r => r.OrganizationId == user.OrganizationId && r.UserId == user.Id && r.Status != "Списан").ToList();
                return View(items);
            }
           

            return RedirectToAction("Login","Account");
        }
    }
}
