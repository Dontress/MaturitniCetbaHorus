using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class DataCheck
    {
        public bool UserAlreadyExists(String userName)
        {
            bool success = false;

            if (userName != null)
            {

                string connectionString = ConnectionString.GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlStatement = "select* from dbo.Zaci where UserName = @username";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = userName;

                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    success = true;
                }

                connection.Close();
            }

            return success;
        }

        public bool UserHasValues(UserModel user)
        {
            bool userHasValues = false;

            if(user.UserName != null && user.Password != null && user.PasswordConfirm != null && user.UserJmeno != null && user.UserPrijmeni != null && user.UserTrida != null)
            {
                userHasValues = true;
            }

            return userHasValues;

        }

        public bool AutorAlreadyExists(String jmeno)
        {

            bool success = false;

            if (jmeno != null)
            {

                string connectionString = ConnectionString.GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlStatement = "select* from dbo.Autori where Jmeno = @jmeno";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@jmeno", System.Data.SqlDbType.VarChar, 75).Value = jmeno;

                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    success = true;
                }

                connection.Close();
            }

            return success;

        }

    }
}
