using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class DeleteItemViewModel
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public bool Select { get; set; }
    }
}
