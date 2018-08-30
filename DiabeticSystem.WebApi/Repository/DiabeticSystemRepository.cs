using DiabeticSystem.Common;
using DiabeticSystem.Common.Models;
using DiabeticSystem.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DiabeticSystem.WebApi.Repository
{
    public class DiabeticSystemRepository : IDiabeticSystemRepository
    {

        public int AddPatientDetails(PatientDetails patientModel)
        {
            int patientId = 0;
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    PatientPersonal personal = new PatientPersonal()
                    {
                        Name = patientModel.Name,
                        Age = patientModel.Age,
                        BloodGroup = patientModel.BloodGroup,
                        Email = patientModel.Email,
                        Password = patientModel.Password
                    };



                    context.PatientPersonals.Add(personal);
                    context.SaveChanges();
                    patientId = (from p in context.PatientPersonals
                                 orderby p.PatientId descending
                                 select p.PatientId).Take(1).SingleOrDefault();


                    PatientMembershipDetail membership = new PatientMembershipDetail()
                    {
                        PatientId = patientId,
                        TestRemaining = 6,
                        BookedDate = DateTime.Now.Date,
                        ExpirationDate = DateTime.Now.Date
                    };

                    membership.ExpirationDate = membership.ExpirationDate.Value.AddMonths(3);
                    context.PatientMembershipDetails.Add(membership);
                    context.SaveChanges();
                }
            }
            catch (Exception err)
            {

                throw;
            }

            return patientId;
        }



        public bool CheckUserNameExists(string username)
        {
            bool isExists = false;
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    var user = (from u in context.PatientPersonals
                                where u.Name.ToLower() == username.ToLower()
                                select u).FirstOrDefault();

                    if (user != null)
                    {
                        isExists = true;
                    }
                }
            }
            catch (Exception err)
            {

                throw;
            }
            return isExists;
        }

        public List<PatientDetails> GetAllPatientDetails(string bloodgroup, string testdate)
        {
            List<PatientDetails> patients = new List<PatientDetails>();

            DateTime patientTestDate = (testdate != string.Empty) ? Convert.ToDateTime(testdate).Date : default(DateTime);

            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    if ((bloodgroup == string.Empty || bloodgroup == null) && (testdate == string.Empty || testdate == null))
                    {
                        patients = (from personal in context.PatientPersonals
                                    join test in context.PatientTestResults
                                    on personal.PatientId equals test.PatientId
                                    join member in context.PatientMembershipDetails
                                    on personal.PatientId equals member.PatientId
                                    select new PatientDetails()
                                    {
                                        PatientId = personal.PatientId,
                                        Name = personal.Name,
                                        Age = personal.Age,
                                        Email = personal.Email,
                                        BloodGroup = personal.BloodGroup,
                                        SugarLevelBeforeFasting = test.SugarBeforeFasting,
                                        SugarLevelAfterFasting = test.SugarAfterFasting,
                                        TestDate = test.TestDate.Value,
                                        //TestRemaining = member.TestRemaining,
                                        //ExpiresOn = Convert.ToString(member.BookedDate.AddMonths(3))
                                    }).ToList();
                    }
                    else if (bloodgroup != string.Empty && (testdate == string.Empty || testdate == null))
                    {
                        patients = (from personal in context.PatientPersonals
                                    join test in context.PatientTestResults
                                    on personal.PatientId equals test.PatientId
                                    join member in context.PatientMembershipDetails
                                    on personal.PatientId equals member.PatientId
                                    where personal.BloodGroup == bloodgroup
                                    select new PatientDetails()
                                    {
                                        PatientId = personal.PatientId,
                                        Name = personal.Name,
                                        Age = personal.Age,
                                        Email = personal.Email,
                                        BloodGroup = personal.BloodGroup,
                                        SugarLevelBeforeFasting = test.SugarBeforeFasting,
                                        SugarLevelAfterFasting = test.SugarAfterFasting,
                                        TestDate = test.TestDate.Value,
                                        //TestDate=Convert.ToString(test.TestDate),
                                        //TestRemaining = member.TestRemaining,
                                        //ExpiresOn = Convert.ToString(member.BookedDate.AddMonths(3))
                                    }).ToList();
                    }
                    else if ((bloodgroup == string.Empty || bloodgroup == null) && testdate != string.Empty)
                    {
                        patients = (from personal in context.PatientPersonals
                                    join test in context.PatientTestResults
                                    on personal.PatientId equals test.PatientId
                                    join member in context.PatientMembershipDetails
                                    on personal.PatientId equals member.PatientId

                                    where test.TestDate == patientTestDate
                                    select new PatientDetails()
                                    {
                                        PatientId = personal.PatientId,
                                        Name = personal.Name,
                                        Age = personal.Age,
                                        Email = personal.Email,
                                        BloodGroup = personal.BloodGroup,
                                        SugarLevelBeforeFasting = test.SugarBeforeFasting,
                                        SugarLevelAfterFasting = test.SugarAfterFasting,
                                        TestDate = test.TestDate.Value,
                                        //TestDate=Convert.ToString(test.TestDate),
                                        //TestRemaining = member.TestRemaining,
                                        //ExpiresOn = Convert.ToString(member.BookedDate.AddMonths(3))
                                    }).ToList();
                    }
                    else
                    {
                        patients = (from personal in context.PatientPersonals
                                    join test in context.PatientTestResults
                                    on personal.PatientId equals test.PatientId
                                    join member in context.PatientMembershipDetails
                                    on personal.PatientId equals member.PatientId
                                    where personal.BloodGroup == bloodgroup && test.TestDate == patientTestDate
                                    select new PatientDetails()
                                    {
                                        PatientId = personal.PatientId,
                                        Name = personal.Name,
                                        Age = personal.Age,
                                        Email = personal.Email,
                                        BloodGroup = personal.BloodGroup,
                                        SugarLevelBeforeFasting = test.SugarBeforeFasting,
                                        SugarLevelAfterFasting = test.SugarAfterFasting,
                                        TestDate = test.TestDate.Value,
                                        //TestDate=Convert.ToString(test.TestDate),
                                        //TestRemaining = member.TestRemaining,
                                        //ExpiresOn = Convert.ToString(member.BookedDate.AddMonths(3))
                                    }).ToList();
                    }


                }
            }
            catch (Exception err)
            {

                throw;
            }

            return patients;
        }

        public List<PatientDetails> AddPatientTestResult(PatientTestSummary patientModel)
        {
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    PatientTestResult testresult = new PatientTestResult()
                    {
                        PatientId = patientModel.PatientId,
                        SugarBeforeFasting = patientModel.SugarLevelBeforeFasting,
                        SugarAfterFasting = patientModel.SugarLevelAfterFasting,
                        TestDate = patientModel.TestDate
                    };
                    context.PatientTestResults.Add(testresult);
                    context.SaveChanges();

                    PatientMembershipDetail membership = (from m in context.PatientMembershipDetails
                                                          where m.PatientId == patientModel.PatientId
                                                          select m).FirstOrDefault();
                    membership.TestRemaining = membership.TestRemaining - 1;
                    context.Entry(membership).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception err)
            {

                throw;
            }
            return GetAllPatientDetails(string.Empty, string.Empty);
        }

        public PatientSummary GetAPatientDetail(string username, string password)
        {
            PatientSummary summary = new PatientSummary();
            PatientPersonal personalDetail = new PatientPersonal();
            List<PatientTestSummary> testsummary = new List<PatientTestSummary>();
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    int userid = (from p in context.PatientPersonals
                                  where p.Name.ToLower() == username.ToLower() &&
                                  p.Password == password.ToLower()
                                  select p.PatientId).FirstOrDefault();
                    if (userid > 0)
                    {
                        summary = (from p in context.PatientPersonals
                                   join member in context.PatientMembershipDetails
                                   on p.PatientId equals member.PatientId
                                   where p.PatientId == userid
                                   select new PatientSummary()
                                   {

                                       Id = p.PatientId,
                                       Name = p.Name,
                                       Age = p.Age,
                                       Email = p.Email,
                                       BloodGroup = p.BloodGroup,
                                       TestRemaining = member.TestRemaining,
                                       ExpiresOn = member.BookedDate //member.BookedDate.AddMonths(3)
                                   }).FirstOrDefault();

                        summary.ExpiresOn = summary.ExpiresOn.AddMonths(3);

                        testsummary = (from p in context.PatientPersonals
                                       join test in context.PatientTestResults
                                       on p.PatientId equals test.PatientId
                                       where p.PatientId == userid
                                       select new PatientTestSummary()
                                       {
                                           PatientId = p.PatientId,
                                           SugarLevelBeforeFasting = test.SugarBeforeFasting,
                                           SugarLevelAfterFasting = test.SugarAfterFasting,
                                           TestDate = test.TestDate.Value
                                       }).ToList();

                        summary.PatientTestResults = testsummary;
                    }
                    else
                    {
                        summary = null;
                    }
                }
            }
            catch (Exception err)
            {

                throw;
            }

            return summary;
        }

        public PatientSummary RenewPatientPlan(int patientid)
        {
            PatientSummary patientData = new PatientSummary();
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    PatientMembershipDetail memberDetail = (from member in context.PatientMembershipDetails
                                                            where member.PatientId == patientid
                                                            select member).FirstOrDefault();

                    int remainingtest = memberDetail.TestRemaining;
                    memberDetail.TestRemaining = (remainingtest + (Constants.MembershipMonths * 2));
                    memberDetail.BookedDate = System.DateTime.Now.Date;
                    memberDetail.ExpirationDate = DateTime.Now.AddMonths(3);
                    context.Entry(memberDetail).State = EntityState.Modified;
                    context.SaveChanges();

                    patientData = (from p in context.PatientPersonals
                                   join member in context.PatientMembershipDetails
                                   on p.PatientId equals member.PatientId
                                   where p.PatientId == patientid
                                   select new PatientSummary()
                                   {

                                       Id = p.PatientId,
                                       Name = p.Name,
                                       Age = p.Age,
                                       Email = p.Email,
                                       BloodGroup = p.BloodGroup,
                                       TestRemaining = member.TestRemaining,
                                       ExpiresOn = member.BookedDate //member.BookedDate.AddMonths(3)
                                   }).FirstOrDefault();

                    patientData.ExpiresOn = patientData.ExpiresOn.AddMonths(3);
                }
            }
            catch (Exception err)
            {

                throw;
            }

            return patientData;
        }

        public Dictionary<int, string> ReadAllPatientNames()
        {
            // SelectList patientnames = null;
            Dictionary<int, string> nameValue = new Dictionary<int, string>();
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    var names = (from p in context.PatientPersonals
                                 select new { Value = p.PatientId, Text = p.Name }).ToList();

                    foreach (var item in names)
                    {
                        nameValue.Add(item.Value, item.Text);
                    }

                    // patientnames = new SelectList(nameValue, "Key", "Value");
                }
            }
            catch (Exception err)
            {

                throw;
            }

            return nameValue;
        }

        public bool AdminLogin(string username, string password)
        {
            // List<PatientDetails> patients = new List<PatientDetails>();
            bool isAdmin = false;
            try
            {
                using (DiabeticSystemEntities context = new DiabeticSystemEntities())
                {
                    var model = (from p in context.DiabeticAdmins
                                 where p.Name == username && p.Password == password
                                 select p).FirstOrDefault();

                    if (model != null)
                    {
                        //return GetAllPatientDetails(string.Empty, string.Empty);
                        isAdmin = true;
                    }
                }
            }
            catch (Exception err)
            {

                throw;
            }

            return isAdmin;
        }
    }
}