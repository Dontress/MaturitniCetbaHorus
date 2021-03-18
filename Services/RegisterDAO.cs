using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class RegisterDAO
    {

        public bool CreateUser(UserModel user)
        {

            bool success = false;

            DataCheck dataCheck = new DataCheck();

            if ( dataCheck.UserAlreadyExists(user.UserName) == false && dataCheck.UserHasValues(user) && user.Password == user.PasswordConfirm)
            {
                string connectionString = ConnectionString.GetConnectionString();

                string sqlStatement = "insert into dbo.Zaci(UserName, Password, UserJmeno, UserPrijmeni, UserTrida) VALUES(@username, @password, @userjmeno, @userprijmeni, @usertrida)";

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 155).Value = PasswordCoding.EncodeToBase64(user.Password); 
                cmd.Parameters.Add("@userjmeno", System.Data.SqlDbType.VarChar, 20).Value = user.UserJmeno;
                cmd.Parameters.Add("@userprijmeni", System.Data.SqlDbType.VarChar, 20).Value = user.UserPrijmeni;
                cmd.Parameters.Add("@usertrida", System.Data.SqlDbType.VarChar, 3).Value = user.UserTrida;

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
