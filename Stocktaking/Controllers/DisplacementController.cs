using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    database.Update(item);
                    await database.SaveChangesAsync();

                    var itemDisplacement = new ItemDisplacement { ItemId = item.Id, DisplacementId = displacement.Id };
                    database.ItemDisplacement.Add(itemDisplacement);
                    await database.SaveChangesAsync();
                }

                return RedirectToAction("Displacements", "Displacement");

            }

            return RedirectToAction("DeleteItemDisplacement");

        }

           [HttpGet]
           public async Task<IActionResult> AddDisplacement()
           {
                User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
                var rooms = database.Rooms.Where(r => r.OrganizationId == user.OrganizationId).ToList();
                ViewBag.Rooms = new SelectList(rooms, "Name", "Name");
                return View();
           }

        [HttpPost]
        public async Task<IActionResult> AddDisplacement(AddDisplacementViewModel model)
        {
            var user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var rooms = database.Rooms.Where(r => r.OrganizationId == user.OrganizationId).ToList();
            ViewBag.Rooms = new SelectList(rooms, "Name", "Name");
            if (ModelState.IsValid)
            {
                
                var displasement = new Displacement { FromWhere = model.FromWhere, WhereTo = model.WhereTo , Status = "Поступление", When = DateTime.Now, OrganizationId = user.OrganizationId, WhoAdd = user.Username };
                database.Displacements.Add(displasement);
                await database.SaveChangesAsync();

                return RedirectToAction("AddItemsDisplacement", "Item", new { displacementId = displasement.Id, whereTo = model.WhereTo });

            }
            else ModelState.AddModelError("", "Некоректный ввод");

            return View(model);
        }
        
    }
}
