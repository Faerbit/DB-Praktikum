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
    
    public partial class Beiträge
    {
        public int ID { get; set; }
        public string Mitteilung { get; set; }
        public int DiskussionsID { get; set; }
        public string Benutzer { get; set; }
        public Nullable<System.DateTime> Änderungsdatum { get; set; }
    
        public virtual Benutzer Benutzer1 { get; set; }
        public virtual Diskussionen Diskussionen { get; set; }
    }
}
