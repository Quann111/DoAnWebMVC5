using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Areas.Admin.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        // GET: Admin/Course
        public ActionResult DanhsachTinTuc()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.posts.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them_tk()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Them_tk(posts model)
        {
            CourseEntities1 db = new CourseEntities1();
            db.posts.Add(model);
            db.SaveChanges();
            return RedirectToAction("DanhsachTinTuc");
        }

        public ActionResult Detail(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            posts ketqua = db.posts.Find(id);
            return View(ketqua);
        }

        public ActionResult Update_tk(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            posts model2 = db.posts.Find(id);
            return View(model2);
        }
        [HttpPost]
        public ActionResult Update_tk(posts model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.posts.Find(model.id);

            updateModel.title = model.title;
            updateModel.author = model.author;
            updateModel.content = model.content;
            updateModel.category = model.category;





            db.SaveChanges();
            return RedirectToAction("DanhsachTinTuc");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.posts.SingleOrDefault(m => m.id == id);
            db.posts.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("DanhsachTinTuc");
        }
    }
}