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

    public partial class tbl_Diyetisyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Diyetisyen()
        {
            this.tbl_DiyetisyenlerinHastalari = new HashSet<tbl_DiyetisyenlerinHastalari>();
        }
    
        public int ID { get; set; }

        [Required(ErrorMessage = "Name can't be empty")]
        public string DiyetisyenAdi { get; set; }

        [Required(ErrorMessage = "Surname can't be empty")]
        public string DiyetisyenSoyadi { get; set; }

        [Required(ErrorMessage = "Phone Number can't be empty")]
        [MinLength(11, ErrorMessage = "Phone number is not valid")]
        [MaxLength(11, ErrorMessage = "Phone number is not valid")]
        public string TelefonNumarasi { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        public string Sifre { get; set; }
        public Nullable<int> Yetki { get; set; }
        public Nullable<int> il { get; set; }
        public Nullable<int> ilce { get; set; }
    
        public virtual tbl_Yetki tbl_Yetki { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DiyetisyenlerinHastalari> tbl_DiyetisyenlerinHastalari { get; set; }
        public virtual ilceler ilceler { get; set; }
        public virtual iller iller { get; set; }
    }
}
