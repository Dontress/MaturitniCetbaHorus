using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class RegisterDAO
    {

        public bool CreateUser(UserModel user)
        {

            bool success = false;

            string connectionString = ConnectionString.GetConnectionString();
           


            string sqlStatement = "insert into dbo.Zaci(UserName, Password, UserJmeno, UserPrijemni, UserTrida) VALUES(@username, @password, @userjmeno, @userprijmeni, @usertrida)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);


                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;
                command.Parameters.Add("@userjmeno", System.Data.SqlDbType.VarChar, 40).Value = user.UserJmeno;
                command.Parameters.Add("@userprijmeni", System.Data.SqlDbType.VarChar, 40).Value = user.UserPrijmeni;
                command.Parameters.Add("@usertrida", System.Data.SqlDbType.VarChar, 40).Value = user.UserTrida;

                try
                {

                    connection.Open();

                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction("SampleTransaction");
                    command.Transaction = transaction;

                    command.ExecuteNonQuery();
                    connection.Close();
                    success = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }


                return success;
            }
        }
    }
}
