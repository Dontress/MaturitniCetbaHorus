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

        public List<KnihaModel> GetFirstBooks()
        {
            List<KnihaModel> allBooks = new();

            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlStatement = "select Knihy.Nazev, Autori.Jmeno, Knihy.Popis, Knihy.Id FROM dbo.Knihy INNER JOIN dbo.Autori on Knihy.AutorId = Autori.Id";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    allBooks.Add(new KnihaModel { Nazev = (String)reader[0], AutorJmeno = (String)reader[1], Popis = (String)reader[2], Id = (int)reader[3] });
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
