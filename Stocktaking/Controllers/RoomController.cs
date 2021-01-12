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
    public class RoomController : Controller
    {
        private ApplicationContext database;

        public RoomController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllRooms()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var rooms = database.Rooms.Where(r => r.OrganizationId == user.OrganizationId).ToList();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom (AddRoomViewModel model)
        {
            if (ModelState.IsValid)
            {

                User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
                var room = new Room { Name = model.Name, Description = model.Description, OrganizationId = user.OrganizationId };

                database.Rooms.Add(room);
                await database.SaveChangesAsync();

                return RedirectToAction("AllRooms");
            }
            else ModelState.AddModelError("", "");

            return View(model);

        }
    }
}
