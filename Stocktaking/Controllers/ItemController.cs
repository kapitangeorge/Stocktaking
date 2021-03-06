﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            if (user != null)
            {

                var items = database.Items.Where(r => r.OrganizationId == user.OrganizationId).OrderBy(r => r.Status).ToList();
                if (items != null)
                {
                    var itemsViewModels = new List<ItemInRoomViewModel>();
                    foreach (var item in items)
                    {
                        string name, roomName;
                        name = roomName = "";
                        if (item.RoomId != 0)
                        {
                            var room = await database.Rooms.FirstOrDefaultAsync(r => r.Id == item.RoomId);
                            roomName = room.Name;
                        };


                         if (item.UserId != 0)
                        {
                            var itemuser = await database.Users.FirstOrDefaultAsync(r => r.Id == item.UserId);
                            name = itemuser.FirstName + "  " + itemuser.LastName;
                        }

                        itemsViewModels.Add(new ItemInRoomViewModel { Name = item.Name, Description = item.Description, InventoryNumber = item.InventoryNumber, Status = item.Status, RoomName = roomName, Username = name, Id = item.Id, Cost = item.Cost });
                    }
                    return View(itemsViewModels);
                }


            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> AddItemsDisplacement(int displacementId, string whereTo)
        {
            var itemsId = database.ItemDisplacement.Where(r => r.DisplacementId == displacementId).ToList();
            var items = new List<Item>();
            foreach (var id in itemsId)
            {
                var item = await database.Items.FirstOrDefaultAsync(r => r.Id == id.ItemId);
                items.Add(item);
            }

            return View(new AddItemsDisplacementVIewModels { Items = items, DisplacementId = displacementId, WhereTo = whereTo });
        }

        [HttpGet]
        public async Task<IActionResult> AddItem(string displacementId, string WhereTo)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            var room = await database.Rooms.FirstOrDefaultAsync(r => r.Name == WhereTo);
            var model = new AddItemViewModel { DisplacementId = Int32.Parse(displacementId), RoomSelectId = room.Id };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemViewModel model)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);

            if (ModelState.IsValid)
            {
                var item = await database.Items.FirstOrDefaultAsync(r => r.InventoryNumber == model.InventoryNumber && r.OrganizationId == user.OrganizationId);
                if (item == null)
                {
                    item = new Item { Name = model.Name, Description = model.Description, InventoryNumber = model.InventoryNumber, OrganizationId = user.OrganizationId, Status = "Новый", RoomId = model.RoomSelectId, Cost = model.Cost };

                    database.Items.Add(item);
                    await database.SaveChangesAsync();

                    var itemDisplacement = new ItemDisplacement { ItemId = item.Id, DisplacementId = model.DisplacementId };
                    database.ItemDisplacement.Add(itemDisplacement);
                    await database.SaveChangesAsync();
                    var room = await database.Rooms.FirstOrDefaultAsync(r => r.Id == model.RoomSelectId);
                    return RedirectToAction("AddItemsDisplacement", "Item", new { displacementId = model.DisplacementId, whereTo = room.Name });

                }
                else ModelState.AddModelError("", "Инвентарный номер должен быть уникален");

            }
            else ModelState.AddModelError("", "Некоректный ввод");

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int itemId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var item = await database.Items.FirstOrDefaultAsync(r => r.Id == itemId);
            var model = new EditItemViewModel
            {
                Id = item.Id,
                InventoryNumber = item.InventoryNumber,
                Name = item.Name,
                Description = item.Description,
                Cost = item.Cost,
                Status = item.Status,
                OrganizationId = item.OrganizationId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditItem(EditItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (database.Items.Where(r => r.Id != model.Id).All(r => r.InventoryNumber != model.InventoryNumber && r.OrganizationId == model.OrganizationId))
                {
                    var item = await database.Items.FirstOrDefaultAsync(r => r.Id == model.Id);
                    item.InventoryNumber = model.InventoryNumber;
                    item.Name = model.Name;
                    item.Description = model.Description;
                    item.Cost = model.Cost;
                    item.Status = model.Status;
                    database.Update(item);
                    await database.SaveChangesAsync();
                    return RedirectToAction("AllItems");
                }
                else ModelState.AddModelError("", "Инвентарный номер должен быть уникален");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SearchItem(string searchstring)
        {
            if (searchstring != null)
            {
                User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
                ViewBag.Message = searchstring;
                var items = await database.Items.Where(r => ((EF.Functions.Like(r.Name.ToLower().Trim(' '), "%" + searchstring.ToLower() + "%", " ") || r.Name.ToLower().Trim(' ') == searchstring.ToLower().Trim(' ')) && r.OrganizationId == user.OrganizationId)).ToListAsync();
                return View(items);
            }
           
            return View();
        }
    }
}
