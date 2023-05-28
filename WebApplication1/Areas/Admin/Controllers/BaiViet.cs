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
    public class BaiVietController : Controller
    {
        // GET: Admin/Course
        public ActionResult DanhsachBV()
        {

            if (Session["user"] != null)
            {
                CourseEntities1 db = new CourseEntities1();
                return View(db.BaiViet.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Them()
        {

            return View(new BaiViet());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(BaiViet model)
        {
            CourseEntities1 db = new CourseEntities1();
            if (string.IsNullOrEmpty(model.TenBaiViet) == true)
            {
                ModelState.AddModelError("", "Thieu thong tin bai viet ");
                return View(model);
            }

            try
            {
                db.BaiViet.Add(model);
                db.SaveChanges();
                return RedirectToAction("DanhsachBV");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        public ActionResult Update_KhoaHoc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            BaiViet model2 = db.BaiViet.Find(id);
            return View(model2);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update_KhoaHoc(BaiViet model)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.BaiViet.Find(model.ID);

            updateModel.HinhAnh = model.HinhAnh;
            updateModel.TenBaiViet = model.TenBaiViet;
            updateModel.NoiDUNG = model.NoiDUNG;
            updateModel.Mota = model.Mota;
            updateModel.NguoiViet = model.NguoiViet;
            updateModel.HienThi = model.HienThi;



            db.SaveChanges();
            return RedirectToAction("DanhsachBV");
        }

        public ActionResult XoaTrangSuc(int id)
        {
            CourseEntities1 db = new CourseEntities1();
            var updateModel = db.BaiViet.SingleOrDefault(m => m.ID == id);
            db.BaiViet.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("DanhsachBV");
        }
    }
}