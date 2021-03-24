using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class AllUsersDAO
    {

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> allUsers = new();

            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlStatement = "select Zaci.Id, Zaci.UserName, Zaci.UserJmeno, Zaci.UserPrijmeni, Zaci.UserTrida FROM dbo.Zaci ";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    allUsers.Add(new UserModel { Id = (int)reader[0], UserName = (String)reader[1], UserJmeno = (String)reader[2], UserPrijmeni = (String)reader[3], UserTrida = (String)reader[4] });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "chyba v GetAllUsers");
            }

            connection.Close();

            return allUsers;
        }
    }
}
