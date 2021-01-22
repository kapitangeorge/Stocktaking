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
    public class InventoryReportController : Controller
    {
        private readonly ApplicationContext database;

        public InventoryReportController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public async Task<IActionResult> AllInventoryReports()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);

            if (user == null) return NotFound();

            var inventoryReports = await database.InventoryReports.Where(r => r.OrganizationId == user.OrganizationId && r.EndInventory == true).ToListAsync();
            var model = new List<InventoryReportWithItemsViewModel>();

            foreach(var invReport in inventoryReports)
            {
                var invReportWithItems = new InventoryReportWithItemsViewModel(invReport);
                var items = await database.ItemsCheck.Where(r => r.InventoryReportId == invReport.Id).ToListAsync();
                invReportWithItems.Items = items;
                invReportWithItems.Cost = items.Where(r => r.Check == false).Sum(s => s.Cost);
                model.Add(invReportWithItems);
            }

            return View(model);
        }
    }
}
