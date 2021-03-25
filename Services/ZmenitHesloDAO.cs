using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class ZmenitHesloDAO
    {

        public bool ZmenitHeslo(UserModel user)
        {
            bool success = false;

            try
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "UPDATE dbo.Zaci SET Password = @password WHERE Id = @userId";

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 155).Value = PasswordCoding.EncodeToBase64(user.Password);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 40).Value = user.Id;


                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Chyba ve změně hesla u určitého žáka" + e);
            }

            return success;
        }
    }
}
