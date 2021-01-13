using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class AddItemsDisplacementVIewModels
    {

        public int DisplacementId { get; set; }
        public List<Item> Items { get; set; }

        public string WhereTo { get; set; }
    }
}
