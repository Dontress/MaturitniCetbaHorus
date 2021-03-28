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
                case 1: return RedirectToAction("Index", "Home");
                case 0: {

                        AllUsersDAO allUsers = new();

                        return View(allUsers.GetAllUsers());
                    }
                default: return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Smazat(int idZaka)
        {
            CheckingBooksDAO mazaniVybranychKnih = new();
            SmazatZakaDAO mazaniZaka = new();

            mazaniVybranychKnih.SmazatStare(idZaka);

            mazaniZaka.SmazatZaka(idZaka);

            return RedirectToAction("Index");
        }

        public IActionResult Zmenit(int idZaka, int success = 0)
        {

            switch (AuthorizationService.IsLogged(HttpContext))
            {
                case 1: return RedirectToAction("Index", "Home"); ;
                case 0: {

                        UserModel user = new();
                        user.Id = idZaka;

                        ViewBag.Success = success;

                        return View(user); 
                    } 
                default: return RedirectToAction("Index", "Login");
            }



        } 

        public IActionResult ProcessZmenit(UserModel user)
        {
            int success;

            ZmenitHesloDAO meneniHesla = new();

            if (meneniHesla.ZmenitHeslo(user))
                success = 1;
            else
                success = -1;

            return RedirectToAction("Zmenit",new { idZaka = user.Id , success = success});
        }
    }
}
