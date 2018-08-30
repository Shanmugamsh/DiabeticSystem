using DiabeticDiagnosticSystem.Helper;
using DiabeticSystem.Common;
using DiabeticSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace DiabeticDiagnosticSystem.Controllers
{
    public class DiabeticDiagnosticController : Controller
    {
        private string azureapiaddress = ConfigurationManager.AppSettings["azureapiaddress"];

        ApiCall apiCall = new ApiCall();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        public async Task<ActionResult> Index(string bloodGroup, string appointmentDate)
        {

            if (Session["IsAdmin"]!=null && (bool)Session["IsAdmin"])
            {
                List<PatientDetails> patients = await apiCall.GetAllPatientDetails(bloodGroup, appointmentDate);
                return View(patients);
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
        

        [HttpGet]
        public async Task<ActionResult> AllPatientDetails(string bloodgroup, string appointmentdate)
        {

            List<PatientDetails> patients = await apiCall.GetAllPatientDetails(bloodgroup, appointmentdate);
            return PartialView("_PatientDetails", patients.AsEnumerable());
        }

        [HttpGet]  
       
        public async Task<ActionResult> AddPatientTestResult()
        {
            if (Session["IsAdmin"] != null && (bool)Session["IsAdmin"])
            {
                List<SelectListItem> patients = new List<SelectListItem>();
                Dictionary<int, string> patientNames = null;

                var myListItems = new List<SelectListItem>();

                patientNames = await apiCall.GetPatientNames();

                patients.AddRange(patientNames.Select(keyValuePair => new SelectListItem()
                {
                    Value = keyValuePair.Key.ToString(),
                    Text = keyValuePair.Value
                }));

                ViewBag.PatientNames = patients;
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [HttpPost]        
        public async Task<ActionResult> AddPatientTestResult(PatientTestSummary testsummary)
        {
            List<PatientDetails> patientDetails = new List<PatientDetails>();
            try
            {
                patientDetails = await apiCall.SubmitPatientTestResults(testsummary);                
                return RedirectToAction(nameof(Index), "DiabeticDiagnostic");
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "Something went wrong. Please try again");
                return View();
            }

        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdminLogin(LoginModel model)
        {

            bool isAdmin = await apiCall.LoginAdmin(model);

            if (isAdmin)
            {
                Session["IsAdmin"] = true;
                FormsAuthentication.SetAuthCookie("Suresh", true);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, Constants.InvalidUserPassword);

                return View();
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult PatientLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PatientLogin(LoginModel model)
        {
            PatientSummary patients = null;
            patients = await apiCall.LoginPatient(model);

            if (patients != null)
            {
                FormsAuthentication.SetAuthCookie(patients.Name, true);
                Session["LoggedInUser"] = patients;
                return RedirectToAction(nameof(PatientSummary));
            }
            else
            {
                ModelState.AddModelError(string.Empty, Constants.InvalidUserPassword);
                return View(nameof(PatientLogin));
            }

        }

        [HttpGet]        
        public ActionResult PatientSummary()
        {
            
            if (Session["LoggedInUser"] != null)
            {
                PatientSummary patients = (PatientSummary)Session["LoggedInUser"];
                return View(patients);
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
           
        }

        [HttpGet]
        public ActionResult PatientRegister()
        {
            ViewBag.BloodGroup = GenerateBloodGroup.ReadBloodGroups();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PatientRegister(PatientDetails model)
        {

            await apiCall.RegisterPatient(model);
            return RedirectToAction(nameof(PatientLogin));
        }

        [HttpGet]
        public async Task<ActionResult> CheckUserNameExists(string username)
        {
            string patientesponse = string.Empty;

            bool isusernameExists = await apiCall.IsUserNameAlreadyExists(username);

            patientesponse = isusernameExists ? Constants.UserExists : string.Empty;

            return Json(patientesponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RenewPatientMembership(int patientId)
        {
            if (Session["LoggedInUser"] != null)
            {
                PatientSummary summary = await apiCall.RenewPatientMembership(patientId);
                Session["LoggedInUser"] = summary;
                return PartialView("_PatientPersonalDetails", summary);
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public ActionResult Logout()
        {
            ClearSession();
            return RedirectToAction(nameof(Home));
        }

        public ActionResult Error()
        {
            return View();
        }
               
        private void ClearSession()
        {
            Session["IsAdmin"] = null;
            Session["LoggedInUser"] = null;
            FormsAuthentication.SignOut();
            Session.Abandon();
        }
    }
}