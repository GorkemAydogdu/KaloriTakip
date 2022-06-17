using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaloriTakip.Models;
namespace KaloriTakip.Models.Class
{
    public class Class1
    {
        public IEnumerable<tbl_Kahvaltı> kahvaltı { get; set; }
        public IEnumerable<tbl_OgleYemegi> ogle { get; set; }
        public IEnumerable<tbl_AksamYemegi> aksam { get; set; }
        public IEnumerable<tbl_AraOgun> ara { get; set; }
    }
}