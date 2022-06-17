using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace KaloriTakip.Models.Class
{
    public class Class2
    {
        public IEnumerable<SelectListItem> Sehirler{ get; set; }
        public IEnumerable<SelectListItem> Ilceler { get; set; }
    }
}