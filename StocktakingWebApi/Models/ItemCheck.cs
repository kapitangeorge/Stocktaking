using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocktakingWebApi.Models
{
    public class ItemCheck
    {
        public int Id { get; set; }

        public string InventoryNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RoomId { get; set; }

        public string Status { get; set; }

        public int InventoryReportId { get; set; }

        public int UserId { get; set; }

        public bool Check { get; set; }

        public double Cost { get; set; }
    }
}
