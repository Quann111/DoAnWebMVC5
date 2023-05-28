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
    public class LoaiBaiVietController : Controller
    {
        // GET: Admin/Course
        public ActionResult DanhsachLoaiBV()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.LoaiBaiViet.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them_loai()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Them_loai(LoaiBaiViet model)
        {
            if(string.IsNullOrEmpty(model.TenLoai) == true)
            {
                ModelState.AddModelError("", "Thieu thong tin bai viet ");
                return View(model);
            }
            CourseEntities1 db = new CourseEntities1();
            try
            {
                db.LoaiBaiViet.Add(model);
                db.SaveChanges();
                return RedirectToAction("DanhsachLoaiBV");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        //public ActionResult Detail(int id)
        //{
        //    CourseEntities1 db = new CourseEntities1();
        //    Course ketqua = db.Course.Find(id);
        //    return View(ketqua);
        //}


        public ActionResult Update_KhoaHoc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            LoaiBaiViet model2 = db.LoaiBaiViet.Find(id);
            return View(model2);
        }
        [HttpPost]
        public ActionResult Update_KhoaHoc(LoaiBaiViet model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.LoaiBaiViet.Find(model.ID);

            updateModel.TenLoai = model.TenLoai;

            db.SaveChanges();
            return RedirectToAction("DanhsachLoaiBV");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.LoaiBaiViet.SingleOrDefault(m => m.ID == id);
            db.LoaiBaiViet.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("DanhsachLoaiBV");
        }
    }
}