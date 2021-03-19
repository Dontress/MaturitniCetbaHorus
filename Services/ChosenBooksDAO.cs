using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class ChosenBooksDAO
    {

        public List<KnihaModel> GetChosenBooks(int userId)
        {
            List<KnihaModel> chosenBooks = new();

            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlStatement = "select Knihy.Nazev from dbo.Knihy inner join dbo.Zaci_has_Knihy on Knihy.Id = Zaci_has_Knihy.IdKnihy WHERE Zaci_has_Knihy.IdZaka = @UserId";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            try
            {
                cmd.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = userId;

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    chosenBooks.Add(new KnihaModel { Nazev = (String)reader[0] });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "chyba v getChosenBooks");
            }

            connection.Close();

            return chosenBooks;
        }
    }
}
