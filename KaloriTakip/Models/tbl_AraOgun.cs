//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KaloriTakip.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_AraOgun
    {
        public int ID { get; set; }
        public Nullable<int> Kullanici { get; set; }

        [Required(ErrorMessage = "Date can't be empty")]
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<int> Yemek { get; set; }
        public Nullable<double> PorsiyonMiktari { get; set; }
        public Nullable<double> Kalori { get; set; }
    
        public virtual tbl_Kullanici tbl_Kullanici { get; set; }
        public virtual tbl_Yemek tbl_Yemek { get; set; }
    }
}
