using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Services
{
    public class AuthorizationService
    {

        public static int IsLogged(Microsoft.AspNetCore.Http.HttpContext context)   
        {
            // 1 když je přihlášený jakýkoliv uživatel, 0 když je uživatel admin a -1 když nikdo

            if (context.Session.GetString("SessionUser") != null)
            {
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(context.Session.GetString("SessionUser"));
                if (userInfo.UserName == "Admin")
                    return 0;
                else
                    return 1;

            }

            else
                return -1;


        }
    }
}
