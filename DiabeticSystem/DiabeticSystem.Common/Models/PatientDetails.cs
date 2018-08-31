using System.ComponentModel.DataAnnotations;
using System;

namespace DiabeticSystem.Common.Models
{
    public class PatientDetails : PatientTestSummary
    {
        [Display(Name = "Patient ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        
        [StringLength(50, ErrorMessage = "String length must be between 4 and 50", MinimumLength = 4)]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid mail")]
        public string Email { get; set; }

        [Required]
        [Range(10, 100)]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        public int TestRemaining { get; set; }

        public DateTime ExpiresOn { get; set; }

        [StringLength(15, ErrorMessage = "Password Length must between 5 and 15", MinimumLength = 5)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
