using MaturitniCetba;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {

            LoginService loginService = new LoginService();

            if ( loginService.IsValid(userModel))
            {
                var userInfo = new UserInfo() { UserID=userModel.Id, UserName=userModel.UserName };
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userInfo));
                if(userInfo.UserName == "Admin")
                    return RedirectToAction("Index", "Admin");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }
                
        }

      
    }
}
