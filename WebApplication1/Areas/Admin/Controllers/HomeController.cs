using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["fbAppid"],
                client_secret = ConfigurationManager.AppSettings["bAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",

            });
            return Redirect(loginUrl.AbsolutePath);
        }
        private Uri RedirectUri
        {
            get

            {
                var uriBuilder = new UriBuilder(Request.Url); ;
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallbadk");
                return uriBuilder.Uri;
            }
        }

        //public ActionResult FacebookCallback(string code)
        //{
        //    var fb = new FacebookClient();
        //    dynamic result = fb.Post("oauth/access_token", new
        //    {
        //        client_id = ConfigurationManager.AppSettings["FbAppId"],
        //        client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
        //        redirect_uri = RedirectUri.AbsoluteUri,
        //        code = code
        //    });
        //    var accessToken = result.access.token;
        //    if (!string.IsNullOrEmpty(accessToken))
        //    {
        //        fb.AccessToken = accessToken;
        //        dynamic me =
        //        fb.Get("me?fields=first_name,middle_name,last_name,id,email");
        //        string email = me.email;
        //        string userName = me.email;
        //        string firstname = me.first_name;
        //        string middlename = me.middle_name;
        //        string lastname = me.last_name;
        //        var resultFetch = new AdminDao().Find(me.email);
        //        if (result != "")
        //        {
        //            var userSession new = new loginModel();
        //            userSession.username = resultFetch;
        //            Session.Add(constants.USER SESSiON, userSession);
        //        }
        //    }
        //    return Redirect("");
        //}




        [HttpPost]
        public ActionResult Login(string user, string password)
        {
            CourseEntities1 db = new CourseEntities1();
            var nhanvien = db.admins.SingleOrDefault(m => m.email.ToLower() == user.ToLower() && m.password == password);
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
        public ActionResult Register(admins _user)
        {
            CourseEntities1 db = new CourseEntities1();

            if (ModelState.IsValid)
            {
                var check = db.admins.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    _user.password = GetMD5(_user.password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.admins.Add(_user);
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