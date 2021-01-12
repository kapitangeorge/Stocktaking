using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class AddRoomViewModel
    {
        [Required]
        [Display(Name="Название помещения")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Описание помещения")]
        public string Description { get; set; }
    }
}
