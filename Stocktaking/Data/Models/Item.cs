using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string InventoryNumber { get; set; }

        public string Name { get; set; }

        public int RoomId { get; set; }

        public string Status { get; set; }

        public string Location { get; set; } //Имя сотрудника, если предмет находится в личном пользовании 
    }
}
