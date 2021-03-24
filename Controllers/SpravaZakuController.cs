using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Controllers
{
    public class SpravaZakuController : Controller
    {
        public IActionResult Index()
        {
            switch (AuthorizationService.IsLogged(HttpContext))
            {
                case 1: return RedirectToAction("Index", "Home"); ;
                case 0: {

                        AllUsersDAO allUsers = new();
                       





                        return View( allUsers.GetAllUsers() );
                    } 
                default: return RedirectToAction("Index", "Login");
            }
        }
    }
}
