using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class AllBooksDAO
    {

        public List<KnihaModel> GetAllBooks()
        {
            List<KnihaModel> allBooks = new();

            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlStatement = "select Knihy.Nazev, Autori.Jmeno, Knihy.Id, Knihy.ObdobiId, Knihy.DruhId, Druhy.Nazev FROM dbo.Knihy INNER JOIN dbo.Autori on Knihy.AutorId = Autori.Id INNER JOIN dbo.Druhy on Knihy.DruhId = Druhy.Id";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    allBooks.Add(new KnihaModel { Nazev = (String)reader[0], AutorJmeno = (String)reader[1], Id = (int)reader[2], ObdobiId = (int)reader[3], DruhId = (int)reader[4], DruhNazev = (string)reader[5] });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "chyba v GetFirstBooks");
            }

            connection.Close();

            return allBooks;
        }

    }
}
