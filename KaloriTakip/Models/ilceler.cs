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
    
    public partial class ilceler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ilceler()
        {
            this.tbl_Diyetisyen = new HashSet<tbl_Diyetisyen>();
        }
    
        public int id { get; set; }
        public string ilceadi { get; set; }
        public int sehirid { get; set; }
    
        public virtual iller iller { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Diyetisyen> tbl_Diyetisyen { get; set; }
    }
}