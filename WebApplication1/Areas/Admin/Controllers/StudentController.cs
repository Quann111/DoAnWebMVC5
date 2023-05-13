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
    public class StudentController : Controller
    {
        // GET: Admin/Course
        public ActionResult Danhsachsv()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.student.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them_sv()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Them_sv(student model)
        {
            CourseEntities1 db = new CourseEntities1();
            db.student.Add(model);
            db.SaveChanges();
            return RedirectToAction("Danhsachsv");
        }


        public ActionResult Detail(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            student ketqua = db.student.Find(id);
            return View(ketqua);
        }


        public ActionResult Update_sv(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            student model2 = db.student.Find(id);
            return View(model2);
        }
        [HttpPost]
        public ActionResult Update_sv(student model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.student.Find(model.user_id);

            updateModel.email = model.email;
            updateModel.name_Student = model.name_Student;
            updateModel.phone = model.phone;
            updateModel.address = model.address;
            updateModel.payment_method = model.payment_method;
            updateModel.status = model.status;


            db.SaveChanges();
            return RedirectToAction("Danhsachsv");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.student.SingleOrDefault(m => m.user_id == id);
            db.student.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Danhsachsv");
        }
    }
}