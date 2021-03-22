using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Controllers
{
    public class VybratKnihyController : Controller
    {
        public IActionResult Index()
        {

            switch (AuthorizationService.IsLogged(HttpContext))
            {
                case 1:
                    {
                        AllBooksDAO allBooks = new();

                        return View( allBooks.GetFirstBooks() );
                    }
                case 0: return RedirectToAction("Admin", "Home");
                default: return RedirectToAction("Index", "Login");    // bez autorizace = vrátí uzivatele na login ať se přihásí
            }
            
        }

        public IActionResult ProcessVybrat([FromForm] int[] knihy)
        {
            


            return RedirectToAction("Index");
        }
    }
}
