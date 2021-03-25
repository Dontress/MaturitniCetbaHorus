using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class VlozitAutoraDAO
    {

        public bool VlozitAutora(AutorModel autor)
        {
            bool success = false;

            DataCheck dataCheck = new();

            if (autor.Jmeno != null && dataCheck.AutorAlreadyExists(autor.Jmeno) == false)
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "insert into dbo.Autori(Jmeno) VALUES(@jmeno)";

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@jmeno", System.Data.SqlDbType.VarChar, 75).Value = autor.Jmeno;

                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                success = true;
            }

                return success;
        }

    }
}
