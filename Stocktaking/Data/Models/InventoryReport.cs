using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.Models
{
    public class InventoryReport
    {
        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public DateTime StartInventory { get; set; }

        public bool EndInventory { get; set; }
    }
}
