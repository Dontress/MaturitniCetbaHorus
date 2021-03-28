using MaturitniCetba;
using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            switch (AuthorizationService.IsLogged(HttpContext))     // 1 když je přihlášený jakýkoliv uživatel, 0 když je uživatel admin a -1 když nikdo
            {
                case 1: {
                        int userId = Int32.Parse(HttpContext.Session.GetString("SessionId")); 
               
                        ChosenBooksDAO chosenBooks = new ChosenBooksDAO();
                        ViewBag.Id = userId;
                        return View( chosenBooks.GetChosenBooks(userId) );
                    }      
                case 0: return RedirectToAction("Index", "Admin");      // vrátí home page pro admina
                default: return RedirectToAction("Index", "Login");     // bez autorizace = vrátí uzivatele na login ať se přihásí
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
