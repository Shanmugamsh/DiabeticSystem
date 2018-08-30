using DiabeticSystem.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace DiabeticDiagnosticSystem.Helper
{
    public class ApiCall
    {
        private string azureapiaddress = ConfigurationManager.AppSettings["azureapiaddress"];


        public async Task<PatientSummary> RenewPatientMembership(int patientId)
        {
            PatientSummary patientsummary = new PatientSummary();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.PostAsJsonAsync($"api/DiabeticApi/RenewMembership?patientid={patientId}", patientsummary);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    patientsummary = JsonConvert.DeserializeObject<PatientSummary>(response);

                }
            }
            return patientsummary;

        }

        public async Task<List<PatientDetails>> GetAllPatientDetails(string bloodGroup, string appointmentDate)
        {
            List<PatientDetails> patients = new List<PatientDetails>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/DiabeticApi/GetAllPatientDetails?bloodgroup={HttpUtility.UrlEncode(bloodGroup)}&testdate={appointmentDate}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    patients = JsonConvert.DeserializeObject<List<PatientDetails>>(response);

                }
            }

            return patients;
        }

        public async Task<Dictionary<int, string>> GetPatientNames()
        {
            Dictionary<int, string> patientNames = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/DiabeticApi/ReadPatientNames");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    patientNames = JsonConvert.DeserializeObject<Dictionary<int, string>>(response);
                }
            }

            return patientNames;
        }

        public async Task<List<PatientDetails>> SubmitPatientTestResults(PatientTestSummary testsummary)
        {
            List<PatientDetails> patientDetails = new List<PatientDetails>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.PostAsJsonAsync($"api/DiabeticApi/AddPatientTestResults", testsummary);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    patientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(response);
                }
            }

            return patientDetails;
        }

        public async Task<bool> LoginAdmin(LoginModel model)
        {
            bool isAdmin = false;

            List<PatientDetails> patients = new List<PatientDetails>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/DiabeticApi/AdminLogin?username={model.UserName}&password={model.Password}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    isAdmin = JsonConvert.DeserializeObject<bool>(response);
                }
            }

            return isAdmin;
        }

        public async Task<PatientSummary> LoginPatient(LoginModel model)
        {
            PatientSummary summary = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/DiabeticApi/PatientLogin?username={model.UserName}&password={model.Password}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    summary = JsonConvert.DeserializeObject<PatientSummary>(response);

                }
            }

            return summary;
        }

        public async Task<bool> RegisterPatient(PatientDetails model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.PostAsJsonAsync($"api/DiabeticApi/AddPatient", model);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var patientesponse = Res.Content.ReadAsStringAsync().Result;
                }
            }

            return true;
        }

        public async Task<bool> IsUserNameAlreadyExists(string username)
        {
            bool isExists = false;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(azureapiaddress);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"api/DiabeticApi/CheckUserExists?username={username}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    isExists = JsonConvert.DeserializeObject<bool>(response);
                }
            }

            return isExists;
        }
    }
}