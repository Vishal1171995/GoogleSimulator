using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Collections.Specialized;
using CCA.Util;

namespace Whisker.Controllers
{
    public class CCAPaymentController : Controller
    {
        //
        // GET: /CCAPayment/

        public ActionResult Index()
        {
            ViewBag.Ammount = 1.00;
            return View("Index");
        }
        public ActionResult Plan2()
        {
            ViewBag.Ammount = 2.00;
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
            ccaRequest = "tid="+ tid + "&merchant_id=165356&order_id=123654789&currency=INR&redirect_url=http://stg.buttermilktraining.com/CCAPayment/ccavResponseHandler&cancel_url=http://stg.buttermilktraining.com/home/register&amount=1.00&";
            strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);

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



    }
}
