using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stocktaking.Data;
using Stocktaking.Data.Models;
using Stocktaking.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Controllers
{
    public class UserController : Controller
    {
        private ApplicationContext database;

        public UserController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var users = database.Users.Where(r => r.OrganizationId == user.OrganizationId).ToList();
            var usersWithItemsModel = new List<UserWithItemsViewModels>();

            foreach(var oneuser in users)
            {
                var items = database.Items.Where(r => r.UserId == oneuser.Id && r.Status != "Списан").ToList();
                usersWithItemsModel.Add(new UserWithItemsViewModels { FirstName = oneuser.FirstName, LastName = oneuser.LastName, Position = oneuser.Position, Items = items });
            }
            return View(usersWithItemsModel);
        }
    }
}
