using MaturitniCetba.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Controllers
{
    public class SpravaKnihController : Controller
    {
        public IActionResult Index()
        {
            switch (AuthorizationService.IsLogged(HttpContext))
            {
                case 1: return RedirectToAction("Index", "Home"); ;
                case 0:
                    {
                        AllBooksDAO allBooks = new();


                        return View( allBooks.GetAllBooks() );
                    }
                default: return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Smazat(int idKnihy)
        {
            SmazatKnihuDAO mazaniKnihy = new();

            mazaniKnihy.SmazatKnihu(idKnihy);

            return RedirectToAction("Index");
        }
    }
}
