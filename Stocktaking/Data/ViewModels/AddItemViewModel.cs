using Stocktaking.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class AddItemViewModel
    {
        [Required]
        [Display(Name = "Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [Required]
        [Display(Name = "Наименование предмета")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание предмета")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Стоимость")]
        public double Cost { get; set; }

        public int RoomSelectId { get; set; }

       

        public int DisplacementId { get; set; }
    }
}
