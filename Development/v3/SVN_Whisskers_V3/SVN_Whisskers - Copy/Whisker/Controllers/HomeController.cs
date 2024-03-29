﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.App_Start;
using Whisker.Models;
using Whisker.CommonClass;
using Whisker.Areas.Admin.Models;
using System.Globalization;
namespace Whisker.Controllers
{
    
    public class HomeController : Controller
    {
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();
        Student stdtDtl;
        string randomPwd = "";
        CommonFunc cmnF = new CommonFunc();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            #region Cookies Implementation
            //var Cookies = Request.Cookies["MainCookie"];
            //if (Cookies != null)
            //{
            //    Int16 UserType = Convert.ToInt16(Request.Cookies["MainCookie"]["UserType"]);
            //    var URL = "";
            //    if (UserType == 0)
            //    {
            //        //URL = "Students/Home";
            //        URL = "Home";
            //    }
            //    if (UserType == 1)
            //    {
            //        URL = "Trainers/Home";

            //    }
            //    return RedirectToAction("Index", URL);
            //}
            //else
            //{
            //    return View();
            //}
            #endregion
            if (Session["LoginInfo"] != null)
            {

                objUserInfo = (UserLoginInfo)Session["LoginInfo"];
                var j = objUserInfo.UserType;
                if (objUserInfo.UserType == 0)
                {
                    return RedirectToAction("Index", "Home", new { area = "Students" });
                }
                if (objUserInfo.UserType == 1)
                {
                    return RedirectToAction("Index", "Home", new { area = "Trainers" });
                }
                else
                {
                    return RedirectToAction("Batches", "Manage", new { area = "Admin" });
                }
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            var emailid = fc["EmailId"];
            var password = fc["password"];
            var Cookies = Request.Cookies["MainCookie"];
            //Getting Unique row on the basis of userid and password
            var userDetails = objDBentity.UserMasters.Where(x => x.Email == emailid && x.Password == password && x.IsActive == true).FirstOrDefault();
            if (userDetails != null)
            {
                if (userDetails.UserType == 0)
                {
                    getAccountDetails(emailid);
                    ViewBag.Account = objDBentity.BatchAndStudentMappings.Where(x => x.UserID == objUserInfo.UserID && x.isMappingActive == true && x.Status == 1 && x.AccCode != null).OrderBy(x => x.BatchCode).FirstOrDefault();
                    if (ViewBag.Account == null)
                    {
                      
                        return Content("Account is not Assigned");
                    }
                    else
                    {
                        Int32 IntBatchCode = Convert.ToInt32(objUserInfo.BatchCode);
                        var BatchInformation = objDBentity.BatchMasters.Where(x => x.BatchCode == IntBatchCode && x.isActive == true).FirstOrDefault();
                        var BEndDate = BatchInformation.EndDate;

                        CultureInfo provider = CultureInfo.InvariantCulture;
                        DateTime dateFromString;
                        var BatchEndDateFormated = BatchInformation.EndDate.Value.ToString("yyyy-MM-dd", provider);
                        bool isSuccess1 = DateTime.TryParseExact(BatchEndDateFormated, "yyyy-MM-dd", provider, DateTimeStyles.None, out dateFromString);

                        //DateTime dateFromString = DateTime.Parse(BatchInformation.EndDate.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        DateTime BatchEndDate = new DateTime(dateFromString.Year, dateFromString.Month, dateFromString.Day);
                        DateTime CurrentDate = DateTime.Today;
                        int CompareDateResult = DateTime.Compare(CurrentDate,BatchEndDate);
                        if(CompareDateResult <= 0)
                            return JavaScript("window.location = '/Students/Home/Index'");
                        else
                            return Content("Batch is Expired");
                    }
                }
                else if (userDetails.UserType == 1)
                {
                    getAccountDetails(emailid);
                    ViewBag.Batches = objDBentity.BatchAndTrainerMappings.Where(x => x.UserID == objUserInfo.UserID && x.isMappingActive == true).OrderBy(x => x.BatchCode).FirstOrDefault();
                    if (ViewBag.Batches == null)
                    {
                       // return Content("Batch is not Assigned");
                        return JavaScript("window.location = '/Trainers/Home/Index'");
                    }
                    else
                    {
                        //return RedirectToRoute("default");
                        return JavaScript("window.location = '/Trainers/Home/Index'");
                    }
                }
                else if (userDetails.UserType == 2)
                {
                    getAccountDetails(emailid);
                    return JavaScript("window.location = '/Admin/Manage/Batches'");
                }
                else
                {
                    //return RedirectToAction("Index", "Home", new { area = "Admin" });
                    return Content("You have entered an invalid username or password");
                }
            }
            else
            {
                //return RedirectToAction("Index", "Home", new { area = "Admin" });
                return Content("You have entered an invalid username or password");
            }
        }
        public ActionResult Logout()
        {
            Session["LoginInfo"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Forgot()
        {
            return View();
        }
        [SessionExpireFilterAttribute]
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changePasswordSave(string OldPwd, string NewPwd, string ConfrmNewPwd)
        {
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(OldPwd) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Enter Old Password");
                goto Exitlabel;
                //"Enter New Password","Enter Confirm New Password"

            }
            if (string.IsNullOrEmpty(NewPwd) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Enter New Password");
                goto Exitlabel;
                //"Enter New Password","Enter Confirm New Password"

            }
            if (string.IsNullOrEmpty(ConfrmNewPwd) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Enter Confirm New Password");
                goto Exitlabel;
                //"Enter New Password","Enter Confirm New Password"

            }
            if (NewPwd != ConfrmNewPwd)
            {
                ContentResult.Add("0");
                ContentResult.Add("New password and Confirm New Password is not matched");
                goto Exitlabel;
                //"Enter New Password","Enter Confirm New Password"
            }
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var UserExist = objDBentity.UserMasters.Where(x => x.Email == objUserInfo.EMAILID && x.Password == OldPwd).FirstOrDefault();
            if (string.IsNullOrEmpty(Convert.ToString(UserExist)))
            {
                ContentResult.Add("0");
                ContentResult.Add("Password does not match with old password");
                goto Exitlabel;
            }
            if (NewPwd == OldPwd)
            {
                ContentResult.Add("0");
                ContentResult.Add("New password can not be same as old password.");
                goto Exitlabel;
                //"Enter New Password","Enter Confirm New Password"
            }
            if (!string.IsNullOrEmpty(Convert.ToString(UserExist)))
            {
                objDBentity.sp_resetTrainerPwd(ConfrmNewPwd, objUserInfo.UserID);
                ContentResult.Add("1");
                ContentResult.Add("Password has been changed");
                Session["LoginInfo"] = null;
                goto Exitlabel;
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult forgotPassword(string UserId)
        {
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(UserId) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Email/User ID is required.");
                goto Exitlabel;

            }
            var UserExist = objDBentity.UserMasters.Where(x=> x.Email == UserId).FirstOrDefault();
            if (string.IsNullOrEmpty(Convert.ToString(UserExist)) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("User does not exist.");
                goto Exitlabel;
            }
            ResetPwd(UserExist.UserID);
            ContentResult.Add("1");
            ContentResult.Add("Successful");
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        private void getAccountDetails(string emailid)
        {
            // var unicornName = TempData["email"].ToString();// Request.QueryString["email"];
            // var unicornName = Request.QueryString["email"].ToString();
            var userInfo = objDBentity.prcGetUserDetails(emailid).FirstOrDefault();
            /* var userType = objDBentity.prcGetUserDetails("student@gmail.com");
             foreach (var item in userType)
             { 
                item.
             }*/
            // ViewBag.data = "khgij";
            objUserInfo.FirstName = userInfo.FirstName;
            objUserInfo.AccCode = Convert.ToInt32(userInfo.AccCode);
            objUserInfo.AccName = userInfo.AccName;
            objUserInfo.EMAILID = emailid;
            objUserInfo.UserID = userInfo.UserID;
            objUserInfo.UserType = (Int16)userInfo.UserType;
            objUserInfo.BatchCode = userInfo.BatchCode.ToString();
            objUserInfo.BatchName = userInfo.BatchName.ToString();
            var RFAStatus = objDBentity.CheckRFAStatus(Convert.ToString(objUserInfo.AccCode), Convert.ToString(objUserInfo.UserID)).ToList();
            if (RFAStatus.Count > 0)
            {
                objUserInfo.Status = "YSRFA"; //YSRFA means Yes Student RFA (Go For Auction has  been done)
            }
            else
            {
                objUserInfo.Status = "NSRFA";  //NSRFA means No Student RFA (Go For Auction has not been done)
            }
            Session["LoginInfo"] = objUserInfo;

            //#region Cookies Implementation
            //HttpCookie MainCookie = new HttpCookie("MainCookie");
            //MainCookie.Values.Add("Name", objUserInfo.FirstName);
            //MainCookie.Values.Add("Id", objUserInfo.UserID);
            //MainCookie.Values.Add("EmailId", objUserInfo.EMAILID);
            //MainCookie.Values.Add("UserType", objUserInfo.UserType.ToString());
            //MainCookie.Expires = DateTime.Now.AddDays(5);
            //Response.Cookies.Add(MainCookie);
            //#endregion
        }
        public JsonResult ResetPwd(String UserId)
        {
            stdtDtl = new Student();
            string TmpUserId = string.IsNullOrEmpty(UserId) ? null : UserId;
            Int32 resetPwdStatus = 0;
            if (TmpUserId != null)
            {
                var UserDetails = objDBentity.UserMasters.Where(x => x.UserID == TmpUserId).FirstOrDefault();
                stdtDtl.Email = UserDetails.Email;
                stdtDtl.FirstName = UserDetails.FirstName;
                randomPwd = "";
                randomPwd = cmnF.RandomString(6);
                objDBentity.sp_resetTrainerPwd(randomPwd, TmpUserId);
                cmnF.SendPasscodeToMail(stdtDtl.Email, stdtDtl.FirstName, randomPwd, stdtDtl.Email, "whisskers Reset Password");
                resetPwdStatus = 1;
            }
            else if (TmpUserId == null)
            {
                resetPwdStatus = 0;
            }
            return Json(resetPwdStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Opportunities()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            return View();
        }
        public ActionResult Resources()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            return View();
        }
        private ActionResult GetAccountDetails()
        {

            //ViewBag.FName = Request.Cookies["MainCookie"]["Name"];
            //ViewBag.UserID = Request.Cookies["MainCookie"]["Id"];
            //objUserInfo.UserID = ViewBag.UserID;
            //var userDetail = objDBentity.UserMasters.Where(x => x.UserID == objUserInfo.UserID).FirstOrDefault();
            //objUserInfo.FirstName = userDetail.FirstName;
            //objUserInfo.EMAILID = userDetail.Email;
            //objUserInfo.UserID = userDetail.UserID;

            if (Session["LoginInfo"] == null)
            {
                return RedirectToRoute("default");
            }
            else
            {
                objUserInfo = (UserLoginInfo)Session["LoginInfo"];
                string unicornName = objUserInfo.EMAILID;

                var userInfo = objDBentity.prcGetUserDetails(unicornName).FirstOrDefault();
                ViewBag.FName = userInfo.FirstName;
                ViewBag.AccCode = userInfo.AccCode;
                ViewBag.AccName = userInfo.AccName;
                ViewBag.ImagePath = userInfo.ImagePath;
                ViewBag.Description = userInfo.Description;
                ViewBag.AccFullName = userInfo.AccFullName;
                ViewBag.UserID = userInfo.UserID;
                return null;
            }
        }
        public ActionResult NotFound(string aspxerrorpath)
        {
            if (!string.IsNullOrWhiteSpace(aspxerrorpath))
                return RedirectToAction("NotFound");

            return View("Error");
        }
        public ActionResult Exception()
        {
            return View("Exception");
        }
        public ActionResult SignUP()
        {
            return View("SignUP");
        }
        [HttpPost]
        public ActionResult Register()
        {
            return View("RegistrationSuccessful");
        }
        #region ErrorHandling
        public ActionResult ErrorBoard()
        {
            var result = objDBentity.getErrorBaordOnDemand().ToArray();
            ViewBag.TotalRecord = result.Count();
            ViewBag.Result = result.ToList();
            GetAccountDetails();
            return View();
        }
        #endregion
    }
}
