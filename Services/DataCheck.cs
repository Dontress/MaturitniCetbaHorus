using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class DataCheck
    {
        public bool UserAlreadyExists(String userName)
        {
            bool success = false;

            if (userName != null)
            {

                string connectionString = ConnectionString.GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlStatement = "select* from dbo.Zaci where UserName = @username";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = userName;

                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    success = true;
                }

                connection.Close();
            }

            return success;
        }

        public bool UserHasValues(UserModel user)
        {
            bool userHasValues = false;

            if(user.UserName != null && user.Password != null && user.PasswordConfirm != null && user.UserJmeno != null && user.UserPrijmeni != null && user.UserTrida != null)
            {
                userHasValues = true;
            }

            return userHasValues;

        }

        public bool AutorAlreadyExists(String jmeno)
        {

            bool success = false;

            if (jmeno != null)
            {

                string connectionString = ConnectionString.GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlStatement = "select* from dbo.Autori where Jmeno = @jmeno";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@jmeno", System.Data.SqlDbType.VarChar, 75).Value = jmeno;

                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    success = true;
                }

                connection.Close();
            }

            return success;

        }

        public bool MnozstviKnihCheck(List<KnihaModel> knihy)   // jestli je 20 knih
        {
            bool check = false;
            int count = 0;

            foreach (var item in knihy)
                count++;

            if (count == 20)
                check = true;

            return check;
        }

        public bool DuplicitaAutoruCheck(List<KnihaModel> knihy)     // jestli neni vice knih od jednoho autora nez dve
        {
            bool check = false;
            int count = 0;
            int[] autori = new int[ knihy.Count ];

            foreach (var item in knihy)
            {
                autori[count] = item.AutorId;
                count++;
            }

            Array.Sort(autori);

            if ( !(knihy.GroupBy(x => x).Any(g => g.Count() > 2)) )     // tohle nefunguje prosim pekne
            {
                check = true;
            }

            return check;
        }

        public bool ObdobiCheck(List<KnihaModel> knihy, int obdobiId, int knihPotreba)      // jestli je dostatecne knih z urciteho obdobi, urceneho pomoci parametru methody
        {
            bool check = false;
            int count = 0;

            foreach (var item in knihy)
            {
                if (item.ObdobiId == obdobiId)
                    count++;
            }

            if (count >= knihPotreba)
                check = true;

            return check;
        }

        public bool DruhyCheck(List<KnihaModel> knihy, int druhId)      // jestli je kazdy literarni druh zasoupen alespon 2x
        {
            bool check = false;
            int count = 0;

            foreach (var item in knihy)
            {
                if (item.DruhId == druhId)
                    count++;
            }

            if (count >= 2)
                check = true;

            return check;
        }



    }
}
