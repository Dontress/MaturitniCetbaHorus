using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class GetUserByIDDAO
    {

        public UserModel GetUserByID(int userId)
        {
            UserModel user = new();

            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlStatement = "SELECT Zaci.UserJmeno, Zaci.UserPrijmeni, Zaci.UserTrida FROM dbo.Zaci WHERE Id = @userId";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = userId;

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.UserJmeno = (String)reader[0];
                    user.UserPrijmeni = (String)reader[1];
                    user.UserTrida = (String)reader[2];
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "chyba v GetUserByID");
            }

            connection.Close();

            return user;
        }
    }
}
