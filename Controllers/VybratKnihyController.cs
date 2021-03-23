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

                        return View( allBooks.GetAllBooks() );
                    }
                case 0: return RedirectToAction("Admin", "Home");
                default: return RedirectToAction("Index", "Login");    // bez autorizace = vrátí uzivatele na login ať se přihásí
            }
            
        }

        public IActionResult ProcessVybrat([FromForm] int[] knihyId)
        {
            CheckingBooksDAO CheckingBooks = new();

            List<KnihaModel> knihyList = CheckingBooks.GetChosenBooksById(knihyId);

            DataCheck check = new();


            check.MnozstviKnihCheck(knihyList);

            check.DruhyCheck(knihyList, 1);

            check.ObdobiCheck(knihyList, 1, 2);
            check.ObdobiCheck(knihyList, 2, 3);
            check.ObdobiCheck(knihyList, 3, 4);
            check.ObdobiCheck(knihyList, 4, 5);

            check.DuplicitaAutoruCheck(knihyList);



            return RedirectToAction("Index");
        }
    }
}
