using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}