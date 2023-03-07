using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using Whisker.Models;
using CCA.Util;
using System.Data.SqlClient;

namespace Whisker.Controllers
{
    public class CCAPaymentController : Controller
    {
        //
        // GET: /CCAPayment/
        UserLoginInfo objUserInfo = new UserLoginInfo();
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        Order objOrder;
        public ActionResult Index()
        {
            //prcCreateRequest 'sc001',11111,'P2',200,'INR'
            GetOrderID("P2", Convert.ToDecimal(200.00), "INR");
            ViewBag.Ammount = 200.00;
            return View("Index");
        }


        public ActionResult Plan2()
        {
            //prcCreateRequest 'sc001',11111,'P2',200,'INR'
            GetOrderID("P3", Convert.ToDecimal(300.00), "INR");
            ViewBag.Ammount = 300.00;
            return View("Index");
        }
        
        public ActionResult ccavRequestHandler()
        {
            CCACrypto ccaCrypto = new CCACrypto();

            string workingKey = string.Empty;
            workingKey = ConfigurationManager.AppSettings["CcAvenueWorkingKey"].ToString();
            string ccaRequest = "";
            string strEncRequest = "";
            string strAccessCode = string.Empty;
            strAccessCode = ConfigurationManager.AppSettings["CcAvenueAccessCode"].ToString();


            //foreach (string name in Request.Form)
            //{
            //    if (name != null)
            //    {
            //        if (!name.StartsWith("_"))
            //        {
            //            ccaRequest = ccaRequest + name + "=" + Request.Form[name] + "&";
            //            /* Response.Write(name + "=" + Request.Form[name]);
            //              Response.Write("</br>");*/
            //        }
            //    }
            //}
            //tid=1518154615868
            string tid = DateTime.Now.ToString("yyyyMMddmmss");
            objOrder = (Order)Session["OrderDetails"];


            if (objOrder != null)
            {
                // ccaRequest = "tid="+ tid + "&merchant_id=165356&order_id=123654789&currency=INR&redirect_url=http://stg.buttermilktraining.com/CCAPayment/ccavResponseHandler&cancel_url=http://stg.buttermilktraining.com/home/register&amount=1.00&";
                ccaRequest = "tid=" + objOrder.tid + "&merchant_id="+objOrder.merchant_id+"&order_id="+objOrder.order_id+"&currency="+objOrder.currency+"&redirect_url="+objOrder.redirect_url+"&cancel_url="+objOrder.cancel_url+"&amount="+objOrder.amount+"&";
                strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            }
            else
            {

            }

            ViewBag.strEncRequest = strEncRequest;
            ViewBag.strAccessCode = strAccessCode;
            return View();
        }

        public ActionResult ccavResponseHandler()
        {
            int intCount = 0;
            string strException ="No excepion";
            string encResp = "No encResp";
            string encResponse = "Nothing";
            try
            {
                string workingKey = string.Empty;//put in the 32bit alpha numeric key in the quotes provided here
                workingKey = ConfigurationManager.AppSettings["CcAvenueWorkingKey"].ToString();
                CCACrypto ccaCrypto = new CCACrypto();
                
                encResp = Request.Form["encResp"];
                encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                NameValueCollection Params = new NameValueCollection();
                string[] segments = encResponse.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }
                intCount = Params.Count;
                for (int i = 0; i < Params.Count; i++)
                {
                   // Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
                }

            }
            catch(Exception ex)
            {
                strException = ex.Message;
            }
           
            ViewBag.PramCnt = intCount;
            ViewBag.strException = strException;
            ViewBag.encResp = encResp;
            ViewBag.encResponse = encResponse;
            return View();
        }

        private void GetOrderID(string PlanName, decimal Amount, string currency)
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
           var vUserID =  objUserInfo.UserID;
           string tid = DateTime.Now.ToString("yyyyMMddmmss");
            objDBentity.prcCreateRequest(vUserID, Convert.ToInt64(tid), PlanName, Amount, currency);
            var userSuppliedID = new SqlParameter("@USERID", vUserID);
            //select top 1 [USERID],[TransactionID],[OrderID] from OrderMaster where [USERID]=@USERID order by createdon desc
            var OrderDetails = objDBentity.OrderMasters.Where(x => x.USERID == vUserID).FirstOrDefault();
           // var ty=  objDBentity.OrderMasters.SqlQuery("select top 1 [USERID],[TransactionID],[OrderID] from OrderMaster where [USERID]=@USERID order by createdon desc", userSuppliedID).FirstOrDefault();
            if(OrderDetails != null)
            {
                objOrder = new Order();
                objOrder.UserID = OrderDetails.USERID;
                objOrder.order_id = Convert.ToString(OrderDetails.OrderID);
                objOrder.currency = OrderDetails.currency;
                objOrder.amount =Convert.ToDecimal(OrderDetails.Amount);
                objOrder.tid = Convert.ToString(OrderDetails.TransactionID);
                Session["OrderDetails"] = objOrder;
                
            }
            else
            {
              
            }
        }




    }
}
