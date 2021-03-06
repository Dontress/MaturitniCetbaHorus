using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace MaturitniCetba.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessRegistration(UserModel userModel)
        {

            RegisterService registerService = new RegisterService();

            if(registerService.CreateUser(userModel))
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "Login");
        }
    }
}
