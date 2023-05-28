using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using WebApplication1.Others;
namespace WebApplication1.Controllers
{
    public class courseUserController : Controller
    {
        // GET: courseUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Payment()
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); 
            pay.AddRequestData("vnp_Command", "pay"); 
            pay.AddRequestData("vnp_TmnCode", tmnCode); 
            pay.AddRequestData("vnp_Amount", "100000"); 
            pay.AddRequestData("vnp_BankCode", ""); 
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); 
            pay.AddRequestData("vnp_CurrCode", "VND"); 
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); 
            pay.AddRequestData("vnp_Locale", "vn"); 
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); 
            pay.AddRequestData("vnp_OrderType", "other"); 
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); 
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); 

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; 
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); 
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); 
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); 
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; 

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); 

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                      
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                   
                        ViewBag.Message = "Bạn đã hủy mua khóa học " ;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }
    }
}