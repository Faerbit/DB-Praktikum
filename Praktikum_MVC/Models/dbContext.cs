using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Praktikum_MVC.Models
{
    public class dbContext : DataContext
    {
        public dbContext() : base(ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString) { }
        public Table<_Modul> Module;
        public Table<_Benutzer> Benutzer;
        public Table<_Professor> Professoren;
    }

    [Table(Name= "Module")]
    public class _Modul
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }
        [Column]
        public int FachNummer { get; set; }
        [Column]
        public string Bezeichnung { get; set; }
        [Column]
        public string Verantwortlicher { get; set; }
    }

    [Table(Name = "Benutzer")]
    public class _Benutzer
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Nickname { get; set; }
        [Column]
        public string Vorname { get; set; }
        [Column]
        public string Nachname { get; set; }
    }

    [Table(Name = "Professoren")]
    public class _Professor
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string Nickname { get; set; }
        [Column]
        public string AkademischerTitel { get; set; }
    }
}