using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class SmazatKnihuDAO
    {
        public bool SmazatKnihu(int idKnihy)
        {
            bool success = false;

            try
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "delete from Knihy where Id = @idKnihy";

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@idKnihy", System.Data.SqlDbType.Int, 40).Value = idKnihy;


                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Chyba v mazani urcite knihy");
            }

            return success;
        }

    }
}
