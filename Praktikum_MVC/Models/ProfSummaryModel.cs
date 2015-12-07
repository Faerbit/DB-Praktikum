using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Praktikum_MVC.Models
{
    public class ProfSummary
    {
        public string nickname { get; set; }
        public List<ForumBeitrag> beitraege { get; set; }
        public List<Dokument> dokumente { get; set; }
        public List<Modul> module { get; set; }

        public static ProfSummary Load(string nickname)
        {
            var result = new ProfSummary
            {
                nickname = nickname,
                dokumente = Dokument.getDokumentByUser(nickname),
                //Beiträge = 
                module = Modul.getByUser(nickname)
            };
            
            return result;
        }
    }

    public class ForumBeitrag
    {
        
    }

    public class Dokument
    {
        public string titel { get; set; }
        public string bereitstellung { get; set; }

        public static List<Dokument> getDokumentByUser(string nickname)
        {
            var result = new List<Dokument>();
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"
                select Titel, datediff(day, Bereitstellungsdatum, CURRENT_TIMESTAMP) as dayDiff 
                from Dokumente where Benutzer = @nickname
                order by dayDiff";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nickname", nickname);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dokument = new Dokument
                    {
                        bereitstellung = reader["dayDiff"].ToString(),
                        titel = reader["Titel"].ToString()
                    };
                result.Add(dokument);
            }
            return result;
        }
    }
}