using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class RegisterService
    {

        RegisterDAO registerDAO = new RegisterDAO();

        public bool CreateUser(UserModel user)
        {
            return registerDAO.CreateUser(user);
        }
       

       

       

       
   
            
    }
}
