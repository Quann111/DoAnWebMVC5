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
    public class CourseController : Controller
    {
        // GET: Admin/Course
        public ActionResult DanhsachKhoaHoc()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.Course.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them_KhoaHoc()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Them_KhoaHoc(Course model)
        {
            CourseEntities1 db = new CourseEntities1();
            db.Course.Add(model);
            db.SaveChanges();
            return RedirectToAction("DanhsachKhoaHoc");
        }


        public ActionResult Detail(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            Course ketqua = db.Course.Find(id);
            return View(ketqua);
        }


        public ActionResult Update_KhoaHoc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            Course model2 = db.Course.Find(id);
            return View(model2);
        }
        [HttpPost]
        public ActionResult Update_KhoaHoc(Course model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.Course.Find(model.product_id);

            updateModel.price = model.price;
            updateModel.name_Course = model.name_Course;
            updateModel.teacher = model.teacher;
            updateModel.description = model.description;
            updateModel.image = model.image;


            db.SaveChanges();
            return RedirectToAction("DanhsachKhoaHoc");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.Course.SingleOrDefault(m => m.product_id == id);
            db.Course.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("DanhsachKhoaHoc");
        }
    }
}