using Microsoft.AspNetCore.Mvc;
using Stocktaking.Data;
using Stocktaking.Data.Models;
using Stocktaking.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Controllers
{
    public class DisplacementController : Controller
    {
        private ApplicationContext database;

        public DisplacementController (ApplicationContext context)
        {
            database = context;
        }

        
        public IActionResult Displacements()
        {
            User user = database.Users.FirstOrDefault(r => r.Username == User.Identity.Name);
            var displacements = database.Displacements.Where(r => r.OrganizationId == user.OrganizationId).ToList();


            return View(displacements);
        }


        [HttpGet]
        public IActionResult DeleteItemsDisplacements()
        {
            User user = database.Users.FirstOrDefault(r => r.Username == User.Identity.Name);
            var items = database.Items.Where(r => r.OrganizationId == user.OrganizationId);
            return View(items);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteItemsDisplacement(List<Item> items)
        {
            if (items != null)
            {
                var displacement = new Displacement { OrganizationId = items[0].OrganizationId, When = DateTime.Now, Status = "Списание", WhoAdd = User.Identity.Name };
                database.Displacements.Add(displacement);
                await database.SaveChangesAsync();

                foreach (var item in items)
                {
                    item.Status = "Списан";
                    item.RoomId = 0;
                    item.Location = ""; 
                    database.Update(item);
                    await database.SaveChangesAsync();

                    var itemDisplacement = new ItemDisplacement { IventoryNumber = item.InventoryNumber, DisplacemnetId = displacement.Id };
                    database.ItemDisplacement.Add(itemDisplacement);
                    await database.SaveChangesAsync();
                }

                return RedirectToAction("Displacements", "Displacement");

            }

            return RedirectToAction("DeleteItemDisplacement");

        }

            //[HttpPost]
            ////public async Task<IActionResult> AddItemsDisplacements(AddDisplacementViewModel model)
            ////{

            ////}
        }
}
