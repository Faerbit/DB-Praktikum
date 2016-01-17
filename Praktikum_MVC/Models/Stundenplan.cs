using System;
using System.Collections.Generic;

namespace Praktikum_MVC.Models
{
    public class Stundenplan
    {
        public int FachSemester { get; set; }
        public DateTime Aktualisiert { get; set; }
        public string Studiengang { get; set; }
        public List<Tag> Tage { get; set; }

        public Stundenplan()
        {
            Tage = new List<Tag>();
        }

        public static Stundenplan GetMockupDaten()
        {
            string[] zeiten = { "08:15", "10:15", "12:15", "14:15", "15:45", "17:15" };
            string[] tage = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag" };

            var result = new Stundenplan
                             {
                                 Aktualisiert = DateTime.Now,
                                 FachSemester = 1,
                                 Studiengang = "databases for novice",
                             };

            // montag
            foreach(var t in tage)
            {
                var tag = new Tag {Name = t, Bloecke = new List<Block>()};
                
                foreach (var zeit in zeiten)
                {
                    tag.Bloecke.Add(new Block
                    {
                        Zeit = zeit,
                        FachNr = 973,
                        Typ = "V",
                        Veranstaltung = "databases for professionals",
                        profUsername = "ritz",
                        profName = "Thomas Ritz"
                    });
                }

                result.Tage.Add(tag);
            }

            return result;
        }
    }

    public class Tag
    {
        public string Name { get; set; }
        public List<Block> Bloecke { get; set; }

        public Tag()
        {
            Bloecke = new List<Block>();
        }   
    }

    public class Block
    {
        public string Zeit { get; set; }
        public string Veranstaltung { get; set; }
        public string Typ { get; set; }
        public int FachNr { get; set; }
        public string profUsername { get; set; }
        public string profName { get; set; }
    }
}