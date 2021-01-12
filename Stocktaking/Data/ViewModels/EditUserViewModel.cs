using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class EditUserViewModel
    {

        [Required]
        [Display(Name="Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Фамилия")]
        public string LastName { get; set; }

        public int UserId { get; set; }
    }
}
