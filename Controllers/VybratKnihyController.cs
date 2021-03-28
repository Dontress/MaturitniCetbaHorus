using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Controllers
{
    public class VybratKnihyController : Controller
    {
        public IActionResult Index(ChybaVyberuModel chyba)
        {

            switch (AuthorizationService.IsLogged(HttpContext))
            {
                case 1:
                    {
                        
                            ViewBag.DvacetKnih = chyba.DvacetKnih;
                            ViewBag.Proza = chyba.DruhProza;
                            ViewBag.Poezie = chyba.DruhPoezie;
                            ViewBag.Drama = chyba.DruhDrama;
                            ViewBag.Prvni = chyba.ObdobiPrvni;
                            ViewBag.Druhe = chyba.ObdobiDruhe;
                            ViewBag.Treti = chyba.ObdobiTreti;
                            ViewBag.Ctvrte = chyba.ObdobiCtvrte;
                            ViewBag.Autori = chyba.DuplicitaAutoru;
                        
                        AllBooksDAO allBooks = new();

                        return View( allBooks.GetAllBooks() );
                    }
                case 0: return RedirectToAction("Admin", "Home");
                default: return RedirectToAction("Index", "Login");    // bez autorizace = vrátí uzivatele na login ať se přihásí
            }
            
        }

        public IActionResult ProcessVybrat([FromForm] int[] knihyId, ChybaVyberuModel chyba)
        {
            CheckingBooksDAO checkingBooks = new();

            List<KnihaModel> knihyList = checkingBooks.GetChosenBooksById(knihyId);

            DataCheck check = new();
   
            // jestli je dvacet knih
            chyba.DvacetKnih = check.MnozstviKnihCheck(knihyList);

            // jestli je dost prozy
            chyba.DruhProza = check.DruhyCheck(knihyList, 1);

            // jestli je dost poezie
            chyba.DruhPoezie = check.DruhyCheck(knihyList, 2);

            // jestli je dost dramatu
            chyba.DruhDrama = check.DruhyCheck(knihyList, 3);

            // jestli je dost knih v prvnim obodbi
            chyba.ObdobiPrvni = check.ObdobiCheck(knihyList, 1, 2);

            // jestli je dost knih v prvnim obodbi
            chyba.ObdobiDruhe = check.ObdobiCheck(knihyList, 2, 3);

            // jestli je dost knih v prvnim obodbi
            chyba.ObdobiTreti = check.ObdobiCheck(knihyList, 3, 4);

            // jestli je dost knih v prvnim obodbi
            chyba.ObdobiCtvrte = check.ObdobiCheck(knihyList, 4, 5);
           
            // jestli neni jeden autor vice nez 2x
            chyba.DuplicitaAutoru = check.DuplicitaAutoruCheck(knihyList);

            if (check.BooksAreValid(chyba))
            {
                int userId = Int32.Parse(HttpContext.Session.GetString("SessionId"));

                checkingBooks.SmazatStare(userId);
                checkingBooks.VlozitNove(knihyList, userId);

                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", chyba);
           
        }
    }
}
