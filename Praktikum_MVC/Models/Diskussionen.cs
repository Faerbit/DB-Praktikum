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
    
    public partial class Diskussionen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Diskussionen()
        {
            this.Beiträge = new HashSet<Beiträge>();
        }
    
        public int ID { get; set; }
        public string Titel { get; set; }
        public int AnzahlSichtungen { get; set; }
        public int ForumID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beiträge> Beiträge { get; set; }
        public virtual Foren Foren { get; set; }
    }
}
