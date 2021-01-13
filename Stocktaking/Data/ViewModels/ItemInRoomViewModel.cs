using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class ItemInRoomViewModel
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string RoomName { get; set; }

        public string Status { get; set; }

        public string Username { get; set; } 

        public double Cost { get; set; }
    }
}
