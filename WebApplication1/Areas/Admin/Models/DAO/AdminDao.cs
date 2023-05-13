//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace WebApplication1.Areas.Admin.Models.DAO
//{
//    public class AdminDao
//    {
//        private CourseEntities db;

//        public AdminDao()
//        {
//            db = new CourseEntities();

//        }

//        public string Find(string email)
//        {
//            var result = db.admins.SingleOrDefault(x => x.email.Contains(email));
//            if (result != null)
//            { return result.name; }
//            else
//            {
//                return "";
//            }
//        }
//        public int Login(string user, string pass)
//        {
//            var result = db.admins.SingleOrDefault(x => x.email.Contains(user) && x.password.Contains(pass));
//            if (result == null)
//                return 0;
//            else
//            {
//                return 1;
//            }

//        }


//    }
//}