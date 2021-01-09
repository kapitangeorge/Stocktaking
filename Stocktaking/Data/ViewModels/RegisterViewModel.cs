using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stocktaking.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name="Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name="Должность")]
        public string Position { get; set; }


        public int OrganizationId { get; set; }

        [Required]
        [Display(Name="Email")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
