using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class LoginDAO
    {
        

        public bool FindUser(UserModel user)
        {
            bool success = false;
            bool exists = false;
            
            if (user.UserName != null && user.Password != null)
            {

                String password = "";
               

                string connectionString = ConnectionString.GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlStatement = "select* from dbo.Zaci where UserName = @username";
                SqlCommand cmd = new SqlCommand(sqlStatement, connection);

                cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;

                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    password = sdr.GetString(2);  
                    exists = true;
                }

                if (exists)
                {

                    PasswordCoding pc = new PasswordCoding();

                    if ( pc.DecodeFrom64(password) == user.Password)
                    {
                        
                        connection.Close();
                        success = true;
                    }
                }
                else
                {
                    connection.Close();
                }
                   
            }

            return success;
        }
    }
}
