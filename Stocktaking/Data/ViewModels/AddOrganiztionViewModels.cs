using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class AddOrganiztionViewModels
    {
        [Required]
        [Display(Name = "Наименование организации")]
        public string Name { get; set; }


        public string Username { get; set; }
    }
}
