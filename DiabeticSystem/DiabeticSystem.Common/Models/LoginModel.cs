using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticSystem.Common.Models
{
    public class LoginModel
    {
        [Display(Name = "User Name")]
        [Required]
        [StringLength(25, ErrorMessage = "Character length between 6 and 25", MinimumLength = 4)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(10, ErrorMessage = "Character length between 6 and 10", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
