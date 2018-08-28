using System;
using System.ComponentModel.DataAnnotations;

namespace DiabeticSystem.Common.Models
{
    public class PatientTestSummary
    {
        public int PatientId { get; set; }

        [Display(Name="Test Date")]
        public DateTime TestDate { get; set; }
        [Display(Name = "Sugar Before Fasting")]
        public int SugarLevelBeforeFasting { get; set; }
        [Display(Name = "Sugar After Fasting")]
        public int SugarLevelAfterFasting { get; set; }
    }
}
