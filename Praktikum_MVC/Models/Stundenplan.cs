using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

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

        public static Stundenplan getDaten()
        {
            var doc = XDocument.Load(System.Web.HttpContext.Current.Server.MapPath("content/Stundenplan.xml"));
            string[] zeiten = { "08:15", "10:15", "12:15", "14:15", "15:45", "17:15" };
            string[] tage = { "Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag" };

            var result = new Stundenplan
            {
                Aktualisiert = DateTime.Parse(doc.Element("stundenplan").Attribute("Aktualisiert").Value),
                FachSemester = int.Parse(doc.Element("stundenplan").Attribute("FachSemester").Value),
                Studiengang = doc.Element("stundenplan").Attribute("Studiengang").Value,
            };

            var nummern = GetFachnummern(doc.Root);
            var verzeichnis = new Dictionary<int, Tuple<string, string>>();
            var context = new dbContext();
            foreach (int nr in nummern)
            {
                string nickname = context.Module.Where(m => m.FachNummer == nr).First().Verantwortlicher;
                string name = context.Module.Where(m => m.FachNummer == nr)
                              .Join(context.Benutzer, m => m.Verantwortlicher, b => b.Nickname, (m, b) => new { _Modul = m, _Benutzer = b })
                              .First()._Benutzer.Vorname;
                var tup = new Tuple<string, string>(nickname, name);
                verzeichnis.Add(nr, tup);
            }

            var plan = doc.Element("stundenplan").Descendants("Tag");

            foreach (var tag in tage)
            {
                var newTag = new Tag { Name = tag, Bloecke = new List<Block>() };

                foreach (var zeit in zeiten)
                {
                    var block = plan.Where(t => t.Attribute("Name").Value.Equals(tag))
                            .Descendants("Block").Where(b => b.Attribute("Zeit").Value.Equals(zeit))
                            .First();
                    if (block.Attribute("Typ") != null)
                    {
                        int nr = int.Parse(block.Attribute("FachNr").Value);
                        newTag.Bloecke.Add(new Block
                        {
                            Zeit = zeit,
                            FachNr = nr,
                            Typ = "(" + block.Attribute("Typ").Value + ")",
                            Veranstaltung = block.Value,
                            profUsername = verzeichnis[nr].Item1,
                            profName = verzeichnis[nr].Item2
                        });
                    }
                    else
                    {
                        newTag.Bloecke.Add(new Block
                        {
                            Zeit = zeit,
                            FachNr = 0,
                            Typ = "",
                            Veranstaltung = "",
                            profUsername = "",
                            profName = ""
                        });
                    }
                }

                result.Tage.Add(newTag);
            }

            return result;
        }

        public static IEnumerable<int> GetFachnummern(XElement root)
        {
            return root.Descendants("Tag")
                .Descendants("Block")
                .Where(b => b.Attribute("FachNr") != null).Attributes("FachNr")
                .Select(x => x.Value).Select(int.Parse).Distinct();
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