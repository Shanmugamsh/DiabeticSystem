using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiabeticDiagnosticSystem.Helper
{
    public class GenerateBloodGroup
    {
        public static IEnumerable<SelectListItem> ReadBloodGroups()
        {
            List<SelectListItem> blood = new List<SelectListItem>() {
                new SelectListItem(){ Text="A+ve",Value="A+ve"},
                new SelectListItem(){ Text="A1+ve",Value="A1+ve"},
                new SelectListItem(){ Text="B+ve",Value="B+ve"},
                new SelectListItem(){ Text="O+ve",Value="O+ve"},
                new SelectListItem(){ Text="AB+ve",Value="AB+ve"},

                new SelectListItem(){ Text="A-ve",Value="A-ve"},
                new SelectListItem(){ Text="A1-ve",Value="A1-ve"},
                new SelectListItem(){ Text="B-ve",Value="B-ve"},
                new SelectListItem(){ Text="O-ve",Value="O-ve"},
                new SelectListItem(){ Text="AB-ve",Value="AB-ve"}
            };

            return blood.AsEnumerable();
        }
    }
}