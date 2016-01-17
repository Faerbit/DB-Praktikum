using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace Praktikum_MVC.Models
{
    public class Kategorie
    {
        public string kategorie { get; set; }
    }

    public class Dokumente
    {
        public string titel { get; set; }
        public string datum { get; set; }
        public string groesse { get; set; }
        public string bereitsteller { get; set; }

        public static List<Dokumente> getDokumenteByKategorie(Kategorie kategorie)
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                select Dokumente.Titel, Dokumente.Bereitstellungsdatum, DATALENGTH(Datei) as Groesse, Benutzer.Vorname, Benutzer.Nachname
                from Dokumente join Benutzer on Dokumente.Benutzer = Benutzer.Nickname
                where Kategorie = @kategorie";
            var sqlcmd = new SqlCommand(query, connection);
            sqlcmd.Parameters.AddWithValue("@kategorie", kategorie.kategorie);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            List<Dokumente> result = new List<Dokumente>();
            while (reader.Read())
            {
                string groesse = "";
                if (reader["Groesse"].ToString() == "") {
                    groesse = "0 Bytes";
                }
                else {
                    groesse = reader["Groesse"].ToString() + " Bytes";
                }
                Dokumente dokument = new Dokumente
                {
                    titel = reader["Titel"].ToString(),
                    bereitsteller = reader["Vorname"].ToString() + " " + reader["Nachname"].ToString(),
                    datum = reader["Bereitstellungsdatum"].ToString().Substring(0, 10),
                    groesse = groesse
                };
                result.Add(dokument);
            }
            connection.Close();
            return result;
        }

        public static List<string> getAllKategorien() {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                select Kategorie
                from Dokumente 
                group by Kategorie
                order by Kategorie";
            var sqlcmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            List<string> result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader["Kategorie"].ToString());
            }
            connection.Close();
            return result;
        }

        public static List<Tuple<string, string>> getKategorieStatistik()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                select Kategorie, count(Kategorie) as count
                from Dokumente 
                group by Kategorie
                order by Kategorie";
            var sqlcmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            while (reader.Read())
            {
                var tuple = new Tuple<string, string>(reader["Kategorie"].ToString() 
                    + " (" + reader["count"].ToString() + ")",
                    reader["count"].ToString());
                result.Add(tuple);
            }
            connection.Close();
            return result;
        }

        public static List<Tuple<string, string>> getStatistikByKategorie(Kategorie kategorie) {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                select Vorname, Nachname, count from (
                select Dokumente.Benutzer, count(Dokumente.Benutzer) as count
                from Dokumente 
                where Kategorie = @kategorie
                group by Dokumente.Benutzer) as query1,
                (select * from Benutzer) as query2
                where query1.Benutzer = query2.Nickname";
            var sqlcmd = new SqlCommand(query, connection);
            sqlcmd.Parameters.AddWithValue("@kategorie", kategorie.kategorie);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            while (reader.Read())
            {
                var tuple = new Tuple<string, string>(reader["Vorname"].ToString() 
                    + " " + reader["Nachname"].ToString()
                    + " (" + reader["count"].ToString() + ")",
                    reader["count"].ToString());
                result.Add(tuple);
            }
            connection.Close();
            return result;
        }
    }
}