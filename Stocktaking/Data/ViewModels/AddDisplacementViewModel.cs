using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class AddDisplacementViewModel
    {
        [Required]
        [Display(Name = "Откуда")]
        public string FromWhere { get; set; }

        [Required]
        [Display(Name = "Куда")]
        public string WhereTo { get; set; }

        
    }
}
