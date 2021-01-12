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
    public class ItemController : Controller
    {

        private ApplicationContext database;

        public ItemController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllItems()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if(user != null)
            {
                var items = database.Items.Where(r => r.OrganizationId == user.OrganizationId);
                if (items != null)
                {
                    var itemsViewModels = new List<ItemInRoomViewModel>();
                    foreach (var item in items)
                    {
                        var room = await database.Rooms.FirstOrDefaultAsync(r => r.Id == item.RoomId);
                        itemsViewModels.Add(new ItemInRoomViewModel { Name = item.Name, Description = item.Description, Location = item.Location, InventoryNumber = item.InventoryNumber, Status = item.Status, RoomName = room.Name });
                    }
                    return View(itemsViewModels);
                }

                
            }
            return RedirectToAction("Login", "Account");
        } 

    }
}
