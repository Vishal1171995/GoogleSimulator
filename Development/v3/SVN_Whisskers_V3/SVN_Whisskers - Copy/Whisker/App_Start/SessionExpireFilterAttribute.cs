﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Newtonsoft.Json;

using Whisker.Models;
using Whisker.Areas.Students.Models;
using Whisker.App_Start;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Web.Routing;

namespace Whisker.App_Start
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        UserLoginInfo objUserInfo = new UserLoginInfo();
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;


            HttpContext ctx = HttpContext.Current;
            filterContext.Result = null;
            // check  sessions here
            if (HttpContext.Current.Session["LoginInfo"] == null)
            {
                filterContext.Result = new RedirectResult("~");
                return;
            }
            if (HttpContext.Current.Session["LoginInfo"] != null)
            {
                objUserInfo = (UserLoginInfo)HttpContext.Current.Session["LoginInfo"];
                if (objUserInfo.Status == "NSRFA")
                {
                    var RFAStatus = objDBentity.CheckRFAStatus(Convert.ToString(objUserInfo.AccCode), Convert.ToString(objUserInfo.UserID)).ToList();
                    if (RFAStatus.Count > 0)
                    {
                        objUserInfo.Status = "YSRFA";
                        HttpContext.Current.Session["LoginInfo"] = null;
                        filterContext.Result = null;
                        return;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
        

    }
}