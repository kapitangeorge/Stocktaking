using Microsoft.EntityFrameworkCore;
using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Displacement> Displacements { get; set; }

        public DbSet<ItemDisplacement> ItemDisplacement { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
