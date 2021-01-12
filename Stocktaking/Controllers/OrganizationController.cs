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
    public class OrganizationController : Controller
    {
        private ApplicationContext database;

        public OrganizationController(ApplicationContext context)
        {
            database = context;
        }

        [HttpGet]
        public IActionResult Organizations()
        {

            List<Organization> organizations = database.Organizations.ToList();

            return View(organizations);
        }

        [HttpGet]
        public IActionResult AddOrganization() => View();

        [HttpPost]
        public async Task<IActionResult> AddOrganization(AddOrganiztionViewModels model)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == model.Username);
            var organization = new Organization { Name = model.Name };

            database.Organizations.Add(organization);
            await database.SaveChangesAsync();

            if(user != null)
            {
                user.OrganizationId = organization.Id;
                user.Role = "admin";
                database.Update(user);
                await database.SaveChangesAsync();
            }
            

            return RedirectToAction("AllRooms","Room");
        }

        [HttpPost]
        public async Task<IActionResult> ChoiceOrganizations(string username, int organizationId)
        {
            User user = await database.Users.FirstOrDefaultAsync(r => r.Username == username);
            if (user != null)
            {
                user.OrganizationId = organizationId;
                user.Role = "user";
                database.Update(user);
                await database.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
