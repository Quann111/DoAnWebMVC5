using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult List()
        {
            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.posts.ToList());
            }

            //return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}