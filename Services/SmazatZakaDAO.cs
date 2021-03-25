using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class SmazatZakaDAO
    {
        public bool SmazatZaka(int idZaka)
        {
            bool success = false;

            try
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "delete from Zaci where Id = @userId";

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 40).Value = idZaka;


                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Chyba v mazani urciteho zaka");
            }

            return success;
        }

    }
}
