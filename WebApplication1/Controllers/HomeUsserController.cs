using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Controllers
{
    public class HomeUsserController : Controller
    {
        // GET: HomeUsser
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
            CourseEntities1 db = new CourseEntities1();
            var nhanvien = db.users.SingleOrDefault(m => m.email.ToLower() == user.ToLower() && m.password == password);
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
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "HomeUsser");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(users _user)
        {
            CourseEntities1 db = new CourseEntities1();

            if (ModelState.IsValid)
            {
                var check = db.users.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.users.Add(_user);
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