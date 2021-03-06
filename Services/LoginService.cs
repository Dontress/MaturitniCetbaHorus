using MaturitniCetba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class LoginService
    {

        ZaciDAO zaciDAO = new ZaciDAO();

        public bool IsValid(UserModel user)
        {
           
            return zaciDAO.FindUser(user);  
        }

    }
}
