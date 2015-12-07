using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Praktikum_MVC.Models
{
    public class User
    {
        public string username {get; set;}
        public string password {get; set; }

        public bool checkPassword() {
            if (username == "" || password == "") {
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
    }
}