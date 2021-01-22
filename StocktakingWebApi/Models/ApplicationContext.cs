using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocktakingWebApi.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Displacement> Displacements { get; set; }

        public DbSet<ItemDisplacement> ItemDisplacement { get; set; }

        public DbSet<InsideItems> InsideItems { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<InventoryReport> InventoryReports { get; set; }

        public DbSet<ItemCheck> ItemsCheck { get; set; }
    }
}
