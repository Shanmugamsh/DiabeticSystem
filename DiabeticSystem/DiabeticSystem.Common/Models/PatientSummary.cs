using System.Collections.Generic;

namespace DiabeticSystem.Common.Models
{
    public class PatientSummary:PatientDetails
    {
        public PatientDetails PatientPersonal { get; set; }
        public  List<PatientTestSummary> PatientTestResults { get; set; }
    }
}
