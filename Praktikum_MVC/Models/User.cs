using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Praktikum_MVC.Models
{
    public class User
    {
        public string username {get; set;}
        public string password {get; set; }
        public string vorname {get; set; }
        public string nachname {get; set; }
        public string email {get; set; }

        public bool checkPassword() {
            if (username == null || password == null) {
                return false;
            }
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                    select convert(varchar(32), Passwort, 2) as Passwort from Benutzer where Nickname = @username";
            var sqlcmd = new SqlCommand(query, connection);
            sqlcmd.Parameters.AddWithValue("@username", username);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            if (reader.Read()) {
                if(System.Web.Helpers.Crypto.Hash(password, "md5").ToString() == reader["Passwort"].ToString()) {
                    return true;
                }
            }

            return false;
        }

        public static List<User> getAll() {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                select Nickname, Vorname, Nachname, Email
                from Benutzer
                order by Nickname";
            var sqlcmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            List<User> result = new List<User>();
            while(reader.Read()) {
                User user = new User {
                    username = reader["Nickname"].ToString(),
                    vorname = reader["Vorname"].ToString(),
                    nachname = reader["Nachname"].ToString(),
                    email = reader["email"].ToString()
                };
                result.Add(user);
            }
            connection.Close();
            return result;
        }
    }

    public class NewUser {
        [Required (ErrorMessage="Benutzername ist erforderlich")]
        public string username {get; set;}
        [Required (ErrorMessage="Passwort ist erforderlich")]
        public string password {get; set; }
        [Compare("password")]
        public string password_confirm {get; set; }
        public string vorname {get; set; }
        public string nachname {get; set; }
        [Required (ErrorMessage="E-mail ist erforderlich")]
        [EmailAddress (ErrorMessage = "Eine gültige E-mail ist erforderlich")]
        public string email {get; set; }

        public void register() {
            string connStr = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
            var connection = new SqlConnection(connStr);
            var query = @"
                    INSERT [dbo].[Benutzer] ([Nickname], [Vorname], [Nachname], [Passwort], [Email]) 
                    VALUES (@username, @vorname, @nachname, CONVERT(BINARY(16), HASHBYTES('MD5', @password)), 
                    @email)";
            var sqlcmd = new SqlCommand(query, connection);
            sqlcmd.Parameters.AddWithValue("@username", username);
            if (vorname == null) {
                sqlcmd.Parameters.AddWithValue("@vorname", DBNull.Value);
            }
            else {
                sqlcmd.Parameters.AddWithValue("@vorname", vorname);
            }
            if (nachname == null) {
                sqlcmd.Parameters.AddWithValue("@nachname", DBNull.Value);
            }
            else {
                sqlcmd.Parameters.AddWithValue("@nachname", nachname);
            }
            sqlcmd.Parameters.AddWithValue("@email", email);
            sqlcmd.Parameters.AddWithValue("@password", password);
            connection.Open();
            SqlDataReader reader = sqlcmd.ExecuteReader();
            reader.Read();
            connection.Close();
        }
    }
}