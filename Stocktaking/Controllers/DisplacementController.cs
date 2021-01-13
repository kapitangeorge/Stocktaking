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

        public DisplacementController(ApplicationContext context)
        {
            database = context;
        }


        public async Task<IActionResult> Displacements()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var displacements = database.Displacements.Where(r => r.OrganizationId == user.OrganizationId).OrderByDescending(r => r.When).ToList();

            var model = new List<DisplacementWithItems>();
            foreach(var displacement in displacements)
            {
                var itemsId = database.ItemDisplacement.Where(r => r.DisplacementId == displacement.Id).ToList();
                var itemsForModel = new List<Item>();
                foreach (var itemId in itemsId)
                {
                    var item = await database.Items.FirstOrDefaultAsync(r => r.Id == itemId.ItemId);
                    itemsForModel.Add(item);

                }
                model.Add(new DisplacementWithItems { Items = itemsForModel, FromWhere = displacement.FromWhere, Status = displacement.Status, When = displacement.When, WhereTo = displacement.WhereTo, WhoAdd = displacement.WhoAdd });
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteItemsDisplacement()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var items = database.Items.Where(r => r.OrganizationId == user.OrganizationId && r.Status != "Списан").ToList();
            var model = new DeleteItemsViewModel();
            foreach (var item in items)
            {
                model.DeleteItems.Add(new DeleteItemViewModel { Id = item.Id, InventoryNumber = item.InventoryNumber, Name = item.Name, Status = item.Status });
            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteItemsDisplacement(DeleteItemsViewModel model)
        {

            var user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var displacement = new Displacement { OrganizationId = user.OrganizationId, When = DateTime.Now, Status = "Списание", WhoAdd = User.Identity.Name };
            database.Displacements.Add(displacement);
            await database.SaveChangesAsync();

            foreach (var itemSelect in model.DeleteItems)
            {
                if (itemSelect.Select)
                {
                    var item = await database.Items.FirstOrDefaultAsync(r => r.Id == itemSelect.Id);
                    item.Status = "Списан";
                    item.RoomId = 0;
                    database.Update(item);
                    await database.SaveChangesAsync();

                    var itemDisplacement = new ItemDisplacement { ItemId = item.Id, DisplacementId = displacement.Id };
                    database.ItemDisplacement.Add(itemDisplacement);
                    await database.SaveChangesAsync();
                }
            }

            return RedirectToAction("AllItems", "Item");



            

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

                var displasement = new Displacement { FromWhere = model.FromWhere, WhereTo = model.WhereTo, Status = "Поступление", When = DateTime.Now, OrganizationId = user.OrganizationId, WhoAdd = user.FirstName + " " + user.LastName };
                database.Displacements.Add(displasement);
                await database.SaveChangesAsync();

                return RedirectToAction("AddItemsDisplacement", "Item", new { displacementId = displasement.Id, whereTo = model.WhereTo });

            }
            else ModelState.AddModelError("", "Некоректный ввод");

            return View(model);
        }

    }
}
