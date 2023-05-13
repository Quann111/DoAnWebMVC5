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
    public class AccountController : Controller
    {
        // GET: Admin/Course
        public ActionResult DanhsachTK()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.users.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them_tk()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Them_tk(users model)
        {
            CourseEntities1 db = new CourseEntities1();
            db.users.Add(model);
            db.SaveChanges();
            return RedirectToAction("DanhsachTK");
        }

        public ActionResult Detail(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            users ketqua = db.users.Find(id);
            return View(ketqua);
        }

        public ActionResult Update_tk(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            users model2 = db.users.Find(id);
            return View(model2);
        }
        [HttpPost]
        public ActionResult Update_tk(users model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.users.Find(model.user_id);

            updateModel.email = model.email;
            updateModel.password = model.password;
            updateModel.name = model.name;
            updateModel.phone = model.phone;
            updateModel.address = model.address;



            db.SaveChanges();
            return RedirectToAction("DanhsachTK");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.users.SingleOrDefault(m => m.user_id == id);
            db.users.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("DanhsachTK");
        }
    }
}