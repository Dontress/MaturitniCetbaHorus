using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class CheckingBooksDAO
    {

        public List<KnihaModel> GetChosenBooksById(int[] knihy)
        {
            List<KnihaModel> knihyList = new();

            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            foreach(var item in knihy)
            {
                string sqlStatement = "select Knihy.Id, Knihy.AutorId, Knihy.DruhId, Knihy.ObdobiId FROM dbo.Knihy WHERE Knihy.Id = @knihaId";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@knihaId", System.Data.SqlDbType.Int).Value = item;

                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        knihyList.Add(new KnihaModel { Id = (int)reader[0], AutorId = (int)reader[1], DruhId = (int)reader[2], ObdobiId = (int)reader[3] });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "chyba v GetChosenBooksById");
                }

                connection.Close();
            }
            

            return knihyList;
        }
    }
}
