using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using System.IO;
using Whisker.CommonClass;
namespace Whisker.App_Start
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        Whisker.CommonClass.CommonFunc cmnF = new Whisker.CommonClass.CommonFunc();
        UserLoginInfo objUserInfo = new UserLoginInfo();

        public void OnException(ExceptionContext filterContext)
        {
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            try {
                
                objUserInfo = (UserLoginInfo)HttpContext.Current.Session["LoginInfo"];
                var UserID = objUserInfo.UserID;
                HttpContext.Current.Session["LoginInfo"] = null;
                HttpContext.Current.Session["DownloadGetKeyPlanExcel"] = null;

             
                Exception e = filterContext.Exception;
                var ExceptionMessage = filterContext.Exception.Message;
                string StackTrace = filterContext.Exception.StackTrace;

                cmnF.SendErrorMail("vibheshmishra@gmail.com", "Admin", controllerName, actionName, filterContext.Exception.Message, StackTrace, "whisskers Error");
                objDBentity.prcCreateErrLog(UserID, controllerName, actionName, ExceptionMessage, StackTrace, false);

                //Start Delete one day old csv file
                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/ExcelFiles/"));
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastAccessTime < cmnF.getCurrentDateTime().AddDays(-1))
                        fi.Delete();
                }

                

                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                };
            }
            catch
            {
                objDBentity.prcCreateErrLog("ExceptionError", controllerName, actionName, "ExceptionError", "Error in Exception Filter", false);
                HttpContext.Current.Session["LoginInfo"] = null;
                HttpContext.Current.Session["DownloadGetKeyPlanExcel"] = null;
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                };
            }
        }
    }
}