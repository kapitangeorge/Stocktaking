using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class InventoryReportWithItemsViewModel
    {
        public InventoryReportWithItemsViewModel(InventoryReport inventory)
        {
            Id = inventory.Id;
            OrganizationId = inventory.OrganizationId;
            StartInventory = inventory.StartInventory;
            EndInventory = inventory.EndInventory;
        }

        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public DateTime StartInventory { get; set; }

        public bool EndInventory { get; set; }

        public List<ItemCheck> Items { get; set; }

        public double Cost { get; set; }
    }
}
