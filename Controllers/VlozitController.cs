using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MaturitniCetba.Controllers
{
    public class VlozitController : Controller
    {
        public IActionResult Index(int vlozeno = 0)
        {
            
            switch (AuthorizationService.IsLogged(HttpContext))     
            {
                case 1: return RedirectToAction("Index", "Home");     //uzivatel neni admin => vraceni na home zaka  
                case 0: {                                             // prihlaseny uzivatel je admin => vytvori se dropdown listy a vrati pohled na vlozeni knihy
                        ViewBag.Vlozeno = vlozeno;

                        KnihaModel dropdownlist = new KnihaModel
                        {
                            ObdobiList = GetObdobiList(),
                            AutoriList = GetAutoriList(),
                            DruhyList = GetDruhyList()
                        };

                        return View(dropdownlist);
                    }  
                default: return RedirectToAction("Index", "Login");    // bez autorizace = vrátí uzivatele na login ať se přihásí
            }
        }

        public IActionResult ProcessVlozeni(KnihaModel kniha)
        {

            if (kniha.Nazev != null && kniha.AutorId != 0 && kniha.DruhId != 0 && kniha.ObdobiId != 0)
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "insert into dbo.Knihy(Nazev, AutorId, DruhId, ObdobiId) VALUES(@nazev, @autorId, @druhId, @obdobiId)";

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sqlStatement);

                 
                cmd.Parameters.Add("@nazev", System.Data.SqlDbType.VarChar, 50).Value = kniha.Nazev;
                cmd.Parameters.Add("@autorId", System.Data.SqlDbType.Int).Value = kniha.AutorId;
                cmd.Parameters.Add("@druhId", System.Data.SqlDbType.Int).Value = kniha.DruhId;
                cmd.Parameters.Add("@obdobiId", System.Data.SqlDbType.Int).Value = kniha.ObdobiId;

                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction("Index", new { vlozeno = 1 });
            }

            return RedirectToAction("Index", new { vlozeno = -1 }); 
        }

        public IActionResult Autor(int vlozeno = 0)
        {
            switch (AuthorizationService.IsLogged(HttpContext))
            {
                case 1: return RedirectToAction("Index", "Home");     //uzivatel neni admin => vraceni na home zaka  
                case 0:
                    {                                             // prihlaseny uzivatel je admin => vytvori se dropdown listy a vrati pohled na vlozeni knihy
                        ViewBag.Vlozeno = vlozeno;

                        return View();
                    }
                default: return RedirectToAction("Index", "Login");    // bez autorizace = vrátí uzivatele na login ať se přihásí
            }
  
        }

        public IActionResult ProcessAutor(AutorModel autor)
        {

            DataCheck dataCheck = new();

            if( autor.Jmeno != null && dataCheck.AutorAlreadyExists(autor.Jmeno) == false)
            {
                string connectionString = ConnectionString.GetConnectionString();
                string sqlStatement = "insert into dbo.Autori(Jmeno) VALUES(@jmeno)";

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sqlStatement);

                cmd.Parameters.Add("@jmeno", System.Data.SqlDbType.VarChar, 75).Value = autor.Jmeno;

                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();


                return RedirectToAction("Autor", new { vlozeno = 1 });
            }
        
            return RedirectToAction("Autor", new { vlozeno = -1 }) ;
        }

        public List<ObdobiList> GetObdobiList()
        {

            var connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select Id, Nazev as Nazev From dbo.Obdobi", connection);

            connection.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            List<ObdobiList> obdobi = new List<ObdobiList>();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    obdobi.Add(new ObdobiList
                    {
                        Id = Convert.ToInt32(sdr["Id"]),

                        Nazev = Convert.ToString(sdr["Nazev"]),
                    });
                }
            }

            connection.Close();
            return obdobi;
        }

        public List<AutoriList> GetAutoriList()
        {

            var connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select Id, Jmeno as Nazev From dbo.Autori", connection);

            connection.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            List<AutoriList> autori = new List<AutoriList>();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    autori.Add(new AutoriList
                    {
                        Id = Convert.ToInt32(sdr["Id"]),

                        Nazev = Convert.ToString(sdr["Nazev"]),
                    });
                }
            }

            connection.Close();
            return autori;
        }

        public List<DruhyList> GetDruhyList()
        {

            var connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select Id, Nazev as Nazev From dbo.Druhy", connection);

            connection.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            List<DruhyList> druhy = new List<DruhyList>();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    druhy.Add(new DruhyList
                    {
                        Id = Convert.ToInt32(sdr["Id"]),

                        Nazev = Convert.ToString(sdr["Nazev"]),
                    });
                }
            }

            connection.Close();
            return druhy;
        }
    }
}
