using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class DisplacementWithItems
    {
        public string WhereTo { get; set; }

        public string FromWhere { get; set; }

        public string WhoAdd { get; set; }

        public DateTime When { get; set; }

        public string Status { get; set; }

        public List<Item> Items { get; set; }
    }
}
