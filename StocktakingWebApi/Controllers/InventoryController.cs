using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StocktakingWebApi.Models;



namespace StocktakingWebApi.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly ApplicationContext database;

        public InventoryController(ApplicationContext context)
        {
            database = context;
        }

        // GET: api/<controller>/StartInventory
        [HttpGet("StartInventory")]
        public async Task<ActionResult<IEnumerable<ItemCheck>>> StartInventory()
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }

            var inventoryReport = new InventoryReport { OrganizationId = user.OrganizationId, StartInventory = DateTime.Now};
            database.InventoryReports.Add(inventoryReport);
            await database.SaveChangesAsync();

            var items = database.Items.Where(r => r.OrganizationId == inventoryReport.OrganizationId && r.Status != "Списан");
            var checkitems = new List<ItemCheck>();
            foreach(var item in items)
            {
                var checkItem = new ItemCheck
                {
                    InventoryNumber = item.InventoryNumber,
                    Description = item.Description,
                    InventoryReportId = inventoryReport.Id,
                    RoomId = item.RoomId,
                    UserId = item.UserId,
                    Name = item.Name,
                    Status = item.Status,
                    Cost = item.Cost,
                    Check = false
                };

                database.ItemsCheck.Add(checkItem);
               

                checkitems.Add(checkItem);
            }

            await database.SaveChangesAsync();
            return checkitems;
        }

        // GET api/<controller>/5
        [HttpGet("{inventoryReportId}")]
        public async Task<ActionResult<IEnumerable<ItemCheck>>> Get(int inventoryReportId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }

            return await database.ItemsCheck.Where(r => r.InventoryReportId == inventoryReportId).ToListAsync();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<ItemCheck>> Post([FromForm]string inventoryNumber)
        {
            var itemCheck = await database.ItemsCheck.FirstOrDefaultAsync(r => r.InventoryNumber == inventoryNumber);

            if(itemCheck == null)
            {
                return NotFound();
            }

            itemCheck.Check = true;
            database.Update(itemCheck);
            await database.SaveChangesAsync();
            return itemCheck;
        }

       
    }
}
