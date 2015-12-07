using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Praktikum_MVC.Models
{
    public class Modul
    {
        public string fachNr {get; set; }
        public string bezeichnung {get; set; }
        public string titel {get; set; }
        public string name {get; set; }
        public string verantwortlicher {get; set; }

        public static List<Modul> getAll() {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
            select Module.FachNummer, Module.Bezeichnung, Professoren.AkademischerTitel, Benutzer.Nachname, Module.Verantwortlicher
            from Module
            inner join Professoren on Module.Verantwortlicher = Professoren.Nickname
            join Benutzer on Professoren.Nickname = Benutzer.Nickname
            order by Module.FachNummer";
            var sqlcmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            List<Modul> result = new List<Modul>();
            while(reader.Read()) {
                Modul modul = new Modul {
                    fachNr = reader["FachNummer"].ToString(),
                    bezeichnung = reader["Bezeichnung"].ToString(),
                    titel = Praktikum.PrakHelpers.profTitelHelper(reader["AkademischerTitel"].ToString()),
                    name = reader["Nachname"].ToString(),
                    verantwortlicher = reader["Verantwortlicher"].ToString()
                };
                result.Add(modul);
            }
            connection.Close();
            return result;
        }
    }
}