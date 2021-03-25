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

        public bool SmazatStare(int userId)
        {
            bool success = false;

            try
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "delete from Zaci_has_Knihy where IdZaka = @userId";

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 40).Value = userId;


                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                success = true;
            }
            catch (Exception e) {
                Console.WriteLine("Chyba v mazani knih urciteho zaka" + e);
            }
            

            return success;
        }

        public bool VlozitNove(List<KnihaModel> knihyList, int userId)
        {
            bool success = false;

            try
            {

                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "insert into Zaci_has_Knihy (IdZaka, IdKnihy) VALUES(@userId,@idKnihy)";


                foreach (var item in knihyList)
                {

                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand(sqlStatement);
                    cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 40).Value = userId;
                    cmd.Parameters.Add("@IdKnihy", System.Data.SqlDbType.Int, 40).Value = item.Id;
                    cmd.Connection = connection;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Chyba ve vkládání knih urciteho zaka" + e);
            }


            return success;
        }
    }
}
