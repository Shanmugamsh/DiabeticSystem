using DiabeticSystem.Common.Models;
using DiabeticSystem.WebApi.Repository;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace DiabeticSystem.WebApi.Controllers
{
    public class DiabeticApiController : ApiController
    {
        private IDiabeticSystemRepository repository = new DiabeticSystemRepository();

        [HttpGet]
        public IHttpActionResult CheckUserExists(string username)
        {
            bool result = repository.CheckUserNameExists(username);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult AddPatient(PatientDetails model)
        {
            int userid = 0;
            userid = repository.AddPatientDetails(model);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult PatientLogin(string username, string password)
        {

            PatientSummary patientSummary = repository.GetAPatientDetail(username, password);

            return Ok(patientSummary);
        }

        [HttpGet]
        public IHttpActionResult AdminLogin(string username, string password)
        {

            bool patientSummary = repository.AdminLogin(username, password);

            return Ok(patientSummary);
        }

        [HttpGet]
        public IHttpActionResult GetAllPatientDetails(string bloodgroup, string testdate)
        {
            List<PatientDetails> patientDetails = repository.GetAllPatientDetails(bloodgroup, testdate);

            return Ok(patientDetails);
        }

        [HttpPost]
        public IHttpActionResult RenewMembership(int patientid)
        {
            PatientSummary summary = repository.RenewPatientPlan(patientid);
            return Ok(summary);
        }

        [HttpGet]
        public IHttpActionResult ReadPatientNames()
        {
            Dictionary<int, string> namelist = repository.ReadAllPatientNames();
            return Ok(namelist);
        }

        [HttpPost]
        public IHttpActionResult AddPatientTestResults(PatientTestSummary testsummary)
        {
            List<PatientDetails> testresults = repository.AddPatientTestResult(testsummary);
            return Ok(testresults);

        }

    }
}
