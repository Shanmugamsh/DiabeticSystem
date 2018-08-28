using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabeticDiagnosticSystem.Models
{
    public class PatientLogin
    {
        [Display(Name = "Patient Name")]
        [Required]
        [StringLength(25, ErrorMessage = "Character length between 6 and 25", MinimumLength = 5)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(10, ErrorMessage = "Character length between 6 and 10", MinimumLength = 5)]
        public string Password { get; set; }
    }
}
