//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Praktikum_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Foren
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Foren()
        {
            this.Foren1 = new HashSet<Foren>();
            this.Diskussionen = new HashSet<Diskussionen>();
        }
    
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public Nullable<int> OberforumID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Foren> Foren1 { get; set; }
        public virtual Foren Foren2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diskussionen> Diskussionen { get; set; }
    }
}
