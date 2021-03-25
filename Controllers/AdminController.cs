using MaturitniCetba.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            switch (AuthorizationService.IsLogged(HttpContext))     
            {
                case 1: return RedirectToAction("Index", "Home"); ;      
                case 0: {

                        GetUserWihoutBookDAO withoutBook = new();

                        return View( withoutBook.GetUserWihoutBook() ); 
                    }    
                default: return RedirectToAction("Index", "Login");     
            }
        }
    }
}
