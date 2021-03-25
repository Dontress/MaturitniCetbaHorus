using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class VlozitKnihuDAO
    {

        public bool VlozitKnihu(KnihaModel kniha)
        {
            bool success = false;

            if (kniha.Nazev != null && kniha.AutorId != 0 && kniha.DruhId != 0 && kniha.ObdobiId != 0)
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "insert into dbo.Knihy(Nazev, AutorId, DruhId, ObdobiId) VALUES(@nazev, @autorId, @druhId, @obdobiId)";

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sqlStatement);


                cmd.Parameters.Add("@nazev", System.Data.SqlDbType.VarChar, 50).Value = kniha.Nazev;
                cmd.Parameters.Add("@autorId", System.Data.SqlDbType.Int).Value = kniha.AutorId;
                cmd.Parameters.Add("@druhId", System.Data.SqlDbType.Int).Value = kniha.DruhId;
                cmd.Parameters.Add("@obdobiId", System.Data.SqlDbType.Int).Value = kniha.ObdobiId;

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
