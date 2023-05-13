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
    public class TeacherController : Controller
    {
        // GET: Admin/Course
        public ActionResult DanhsachGV()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.teacher.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them_GV()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Them_GV(teacher model)
        {
            CourseEntities1 db = new CourseEntities1();
            db.teacher.Add(model);
            db.SaveChanges();
            return RedirectToAction("DanhsachGV");
        }


        public ActionResult Detail(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            teacher ketqua = db.teacher.Find(id);
            return View(ketqua);
        }


        public ActionResult Update_KhoaHoc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            teacher model2 = db.teacher.Find(id);
            return View(model2);
        }
        [HttpPost]
        public ActionResult Update_KhoaHoc(teacher model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.teacher.Find(model.teacher_id);

            updateModel.email = model.email;
            updateModel.phone = model.phone;
            updateModel.address = model.address;
            updateModel.subject = model.subject;
            updateModel.education_level = model.education_level;
            updateModel.name_Teacher = model.name_Teacher;


            db.SaveChanges();
            return RedirectToAction("DanhsachGV");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.teacher.SingleOrDefault(m => m.teacher_id == id);
            db.teacher.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("DanhsachGV");
        }
    }
}