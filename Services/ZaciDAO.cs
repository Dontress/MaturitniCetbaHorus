using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ZaciDAO
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = MaturitniCetba; Integrated Security = True; Connect Timeout = 30;   Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool FindUser(UserModel user)
        {
            bool success = false;

            string sqlStatement = "Select* FROM dbo.Zaci WHERE UserName = @username AND Password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try{

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }

            return success;

        }
    }
}
