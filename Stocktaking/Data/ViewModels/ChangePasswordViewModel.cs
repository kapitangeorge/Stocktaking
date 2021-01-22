using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        [Display(Name="Старый пароль")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name ="Новый пароль")]
        public string NewPassword { get; set; }
    }
}
