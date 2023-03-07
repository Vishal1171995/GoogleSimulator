using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using System.Web.Mvc.Ajax;
using Whisker.App_Start;

namespace Whisker.Areas.Admin.Controllers
{
    [SessionExpireFilterAttribute]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();

        public ActionResult Index()
        {
            try
            {
                ViewBag.TrainerDetails = objDBentity.prcUserDetailsOnDemand(1).ToArray();
                ViewBag.Location = objDBentity.LocationMasters.ToList();
                GetAccountDetails();
                BindTime();
                return View();
            }
            catch
            {
                return View();
            }
        }
        private ActionResult GetAccountDetails()
        {
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
        private void getBatchs(string userID, string cCode)
        {
            // var unicornName = TempData["email"].ToString();// Request.QueryString["email"];
            // var unicornName = Request.QueryString["email"].ToString();
            var usrbatch = objDBentity.prcGetCampaign(userID, cCode);
            //var usrbatch = objDBentity
            //* var userType = objDBentity.prcGetUserDetails("student@gmail.com");
            /* foreach (var item in userCampaign)
             {
                // item.CampaignName
                // item.Budget

             }*/
            ViewBag.uCampaign = usrbatch;

            // ViewBag.data = "khgij";
            /*ViewBag.FName = userInfo.FirstName;
            ViewBag.AccName = userInfo.AccName;
            ViewBag.ImagePath = userInfo.ImagePath;
            ViewBag.Description = userInfo.Description;
            ViewBag.AccFullName = userInfo.AccFullName;*/
        }
        public JsonResult getBatchesOnDemand(string BatchTime, string TrainerCode, string LocationCode)
        {
            try
            {
                string TmpTrainerCode = string.IsNullOrEmpty(TrainerCode) ? null : TrainerCode;
                string TmpLocationCode = string.IsNullOrEmpty(LocationCode) ? null : LocationCode;

                GetAccountDetails();
                var getBatches = objDBentity.prcBatchesOnDemand(Convert.ToInt32(BatchTime), TmpTrainerCode, TmpLocationCode, null).ToArray();
                return Json(getBatches, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }
        private void BindTime()
        {
            // Set the start time (00:00 means 12:00 AM)
            DateTime StartTime = DateTime.ParseExact("00:00", "HH:mm", null);
            // Set the end time (23:55 means 11:55 PM)
            DateTime EndTime = DateTime.ParseExact("23:59", "HH:mm", null);
            //Set 5 minutes interval
            TimeSpan Interval = new TimeSpan(1, 0, 0);
            //To set 1 hour interval
            //TimeSpan Interval = new TimeSpan(1, 0, 0); 
            List<SelectListItem> BindTime = new List<SelectListItem>();
            while (StartTime <= EndTime)
            {
                BindTime.Add(new SelectListItem
                { Text = StartTime.ToShortTimeString(), Value = Interval.ToString() });
                StartTime = StartTime.Add(Interval);

            }
            ViewBag.BindTime = BindTime;
        }



        //public JsonResult GetAllAccountDetails(string accCode)
        //{
        //    var allAccounts = objDBentity.AccountsMasters.ToList();
        //    return Json(allAccounts, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetAllBatches(string BatchCode)
        {
            GetAccountDetails();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                BatchCode = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault();
            }
            List<object> AllAccountDetail = new List<object>();
            object AllBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();
            object BatchDetails = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", BatchCode).FirstOrDefault();
            object StudentDetails = objDBentity.prcSrudentsPerBatches(BatchCode, "").ToArray();
            object AccountDetails = objDBentity.prcGetRemainingAccounts("", objUserInfo.UserID).ToArray();
            object SavedAccountDetails = objDBentity.prcGetAccntsDetails(objUserInfo.UserID).ToArray();

            AllAccountDetail.Add(AllBatches);
            AllAccountDetail.Add(BatchDetails);
            AllAccountDetail.Add(StudentDetails);
            AllAccountDetail.Add(AccountDetails);
            AllAccountDetail.Add(SavedAccountDetails);

            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateAccount(string id)
        {
            ViewBag.Active = "#li_ManagBtch";
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];

            if (id != null)
            {
                ViewBag.ID = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID && x.BatchCode == id).FirstOrDefault();
                if (ViewBag.ID != null)
                {
                    ViewBag.ID = id;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View();
            }
            //ViewBag.BatchDetails = objDBentity.prcGetCurrentBatches(objUserInfo.UserID,"a",id).FirstOrDefault();
            //ViewBag.AllBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();
        }
        public JsonResult EditAccountDetails(string BatchCode, string AccCode)
        {
            GetAccountDetails();
            // objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                BatchCode = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault();
            }
            List<object> AllAccountDetail = new List<object>();
            object AllBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();
            object BatchDetails = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", BatchCode).FirstOrDefault();
            object StudentDetails = objDBentity.prcSrudentsPerBatches(BatchCode, AccCode).ToArray();
            object AccountDetails = objDBentity.prcGetRemainingAccounts(AccCode, objUserInfo.UserID);
            object SavedAccountDetails = objDBentity.prcGetAccntsDetails(objUserInfo.UserID).ToArray();

            AllAccountDetail.Add(AllBatches);
            AllAccountDetail.Add(BatchDetails);
            AllAccountDetail.Add(StudentDetails);
            AllAccountDetail.Add(AccountDetails);
            AllAccountDetail.Add(SavedAccountDetails);

            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveAccount(string AccountDetails, string StudentDetails, int Status)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            var SaveAccDetails = objDBentity.prcSaveAccount(StudentDetails, AccountDetails, Status, TrainerCode);
            return Json(SaveAccDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateAccount(string AccountDetails, string StudentDetails, int Status)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            var AccCode = AccountDetails.TrimEnd(',');
            var qry = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                      where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
                      select BatchAndStudentMapping;

            foreach (BatchAndStudentMapping p in qry)
            {
                p.AccCode = null;
                p.Status = null;
                p.TrainerCode = null;
            }

            objDBentity.SaveChanges();
            var SaveAccDetails = objDBentity.prcSaveAccount(StudentDetails, AccountDetails, Status, TrainerCode);
            return Json(SaveAccDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FSaveAccount(int Status)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            var qry = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                      where BatchAndStudentMapping.TrainerCode == TrainerCode
                      select BatchAndStudentMapping;
            if (qry.Any())
            {
                foreach (BatchAndStudentMapping p in qry)
                {
                    p.Status = 1;
                }
                objDBentity.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult DeleteAccountDetails(string BatchCode, string AccCode)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            var qry = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                      where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
                      select BatchAndStudentMapping;

            foreach (BatchAndStudentMapping p in qry)
            {
                p.AccCode = null;
                p.Status = null;
                p.TrainerCode = null;
            }
            objDBentity.SaveChanges();
            var SaveAccDetails = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                                 where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
                                  && BatchAndStudentMapping.Status == 2
                                 select BatchAndStudentMapping;

            return Json(SaveAccDetails, JsonRequestBehavior.AllowGet);
        }
    }
}
