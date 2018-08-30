using DiabeticSystem.Common.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DiabeticSystem.WebApi.Repository
{
    public interface IDiabeticSystemRepository
    {
        bool CheckUserNameExists(string username);

        int AddPatientDetails(PatientDetails patientModel);

        PatientSummary GetAPatientDetail(string username, string passwordl);

        List<PatientDetails> GetAllPatientDetails(string bloodgroup, string testdate);

        PatientSummary RenewPatientPlan(int patientid);

        List<PatientDetails> AddPatientTestResult(PatientTestSummary patientModel);

        Dictionary<int, string> ReadAllPatientNames();

        bool AdminLogin(string username, string password);


    }
}
