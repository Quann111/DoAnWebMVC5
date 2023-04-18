using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Areas.Admin.Controllers
{

    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string user, string password)
        {
            webcourseEntities2 db = new webcourseEntities2();
            var nhanvien = db.Users.SingleOrDefault(m => m.Username.ToLower() == user.ToLower() && m.Password == password);
            if (nhanvien != null)
            {
                Session["user"] = nhanvien;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Tai khoan khong hop le";
                return View();
            }



        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            webcourseEntities2 db = new webcourseEntities2();

            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }

          public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}