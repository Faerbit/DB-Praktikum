using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Praktikum_MVC.Models
{
    public class Professor {
        public string titel {get; set; }
        public string name {get; set; }
        public string nickname {get; set; }

        public static List<Professor> getAll() {
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"
                select AkademischerTitel, Nachname, Professoren.Nickname 
                from Professoren
                join Benutzer on Professoren.Nickname = Benutzer.Nickname";
            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            List<Professor> result = new List<Professor>();
            while(reader.Read()) {
                var professor = new Professor
                {
                    name = reader["Nachname"].ToString(),
                    titel = Praktikum_MVC.PrakHelpers.profTitelHelper(reader["AkademischerTitel"].ToString()),
                    nickname = reader["Nickname"].ToString()
                };
                result.Add(professor);
            }
            return result;
        }
    }

    public class ProfSummary
    {
        public string titel { get; set; }
        public string name { get; set; }
        public List<Forum> foren { get; set; }
        public List<Dokument> dokumente { get; set; }
        public List<Modul> module { get; set; }

        public static Boolean exists(string nickname) {
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"
                select * 
                from Professoren
                where Professoren.Nickname = @nickname";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nickname", nickname);
            var reader = command.ExecuteReader();
            if (reader.Read()) {
                return true;
            }
            else {
                return false;
            }
        }

        public static ProfSummary load(string nickname)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"
                select AkademischerTitel, Nachname 
                from Professoren
                join Benutzer on Professoren.Nickname = Benutzer.Nickname
                where Professoren.Nickname = @nickname";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nickname", nickname);
            var reader = command.ExecuteReader();
            reader.Read();
            var result = new ProfSummary
            {
                name = reader["Nachname"].ToString(),
                titel = Praktikum_MVC.PrakHelpers.profTitelHelper(reader["AkademischerTitel"].ToString()),
                dokumente = Dokument.getDokumentByUser(nickname),
                foren = Forum.getForenByUser(nickname),
                module = Modul.getByUser(nickname)
            };
            
            return result;
        }
    }

    public class Forum
    {
        public string name {get; set;}
        public List<Beitrag> beitraege {get; set; }

        public static List<Forum> getForenByUser(string user) {
            List<Forum> result = new List<Forum>();
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"
                select Bezeichnung 
                from Beiträge 
                join Diskussionen on Beiträge.DiskussionsID = Diskussionen.ID
                join Foren on Diskussionen.ForumID = Foren.ID
                where Benutzer = @user
                group by Bezeichnung
                order by Bezeichnung";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@user", user);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var forum = new Forum
                    {
                        name = reader["Bezeichnung"].ToString(),
                        beitraege = Beitrag.getBeitraegeByUserAndForum(user, reader["Bezeichnung"].ToString())
                    };
                result.Add(forum);
            }
            return result;
        }
    }

    public class Beitrag {
        public string titel {get; set; }
        public string alter {get; set; }

        public static List<Beitrag> getBeitraegeByUserAndForum(string user, string forum) {
            List<Beitrag> result = new List<Beitrag>();
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var query = @"
                select Mitteilung, datediff(day, Änderungsdatum, CURRENT_TIMESTAMP) as dayDiff
                from Beiträge
                join Diskussionen on Beiträge.DiskussionsID = Diskussionen.ID
                join Foren on Diskussionen.ForumID = Foren.ID
                where Benutzer = @user and Bezeichnung = @forum
                order by dayDiff";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@user", user);
            command.Parameters.AddWithValue("@forum", forum);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string titel = reader["Mitteilung"].ToString();
                if (titel.Length > 35) {
                    titel = titel.Substring(0, 32) + "...";
                }
                var beitrag = new Beitrag
                    {
                        titel = titel,
                        alter = reader["dayDiff"].ToString()
                    };
                result.Add(beitrag);
            }
            return result;
        }
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