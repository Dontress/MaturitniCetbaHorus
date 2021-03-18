using MaturitniCetba;
using MaturitniCetba.Models;
using MaturitniCetba.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(bool loginFailed = false)
        {
            HttpContext.Session.Clear();
            ViewBag.LoginFailed = loginFailed;
            return View();
        }

        public IActionResult ProcessLogin(UserModel userModel)
        {

            LoginService loginService = new LoginService();

            if ( loginService.IsValid(userModel))
            {
                userModel.Password = null;
                userModel.PasswordConfirm = null;
                UserGetId(userModel);

                var userInfo = new UserInfo() { UserId=userModel.Id, UserName=userModel.UserName };
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userInfo));
                HttpContext.Session.SetString("SessionId", userInfo.UserId.ToString());
                HttpContext.Session.SetString("SessionName", userInfo.UserName);
                if (userInfo.UserName == "Admin")
                    return RedirectToAction("Index", "Admin");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", new { loginFailed = true });
            }
                
        }

        public void UserGetId(UserModel user)
        {
            string connectionString = ConnectionString.GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlStatement = "select Id from dbo.Zaci where UserName = @username";
            SqlCommand cmd = new SqlCommand(sqlStatement, connection);

            cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;

            connection.Open();
            SqlDataReader sdr = cmd.ExecuteReader();

            while(sdr.Read())
            {
                user.Id = Int32.Parse(sdr["Id"].ToString());
 
            }
        }

      
    }
}
