using System;
using System.ComponentModel.DataAnnotations;

namespace DiabeticSystem.Common.Models
{
    public class PatientTestSummary
    {
        public int PatientId { get; set; }

        [Display(Name="Test Date")]
        [RegularExpression(@"^(0?[1-9]|1[0-2])/(0?[1-9]|1[0-9]|2[0-9]|3[01])/\d{4}$",ErrorMessage ="Enter / Choose valid date")]
        public DateTime TestDate { get; set; }

        [Required]
        [Display(Name = "Sugar Before Fasting")]        
        [Range(90, 400, ErrorMessage = "Sugar value should be between 90 and 400")]
        public int SugarLevelBeforeFasting { get; set; }

        [Required]
        [Range(90, 600,ErrorMessage ="Sugar value should be between 90 and 600")]
        [Display(Name = "Sugar After Fasting")]
        public int SugarLevelAfterFasting { get; set; }
    }
}
