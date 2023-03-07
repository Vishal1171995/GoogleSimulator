﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using System.Web.Mvc.Ajax;
using Whisker.App_Start;
using Whisker.Areas.Admin.Models;
using Whisker.CommonClass;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;
using System.Linq.Dynamic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Whisker.Areas.Admin.Controllers
{
    [SessionExpireFilterAttribute]
    [ExceptionHandler]
    public class ManageController : Controller
    {
        //
        // GET: /Admin/ManageBatches/
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();
        Batch BtchDtl = new Batch();
        Student stdtDtl;
        Account accDtl;
        Keyword KeyDtl;
        Phrase PhraseDtl;
        string randomPwd = "";
        CommonFunc cmnF = new CommonFunc();

        public ActionResult Index()
        {
            try
            {
                ViewBag.TrainerDetails = objDBentity.prcUserDetailsOnDemand(1).ToArray();
                ViewBag.Location = objDBentity.LocationMasters.OrderBy(x => x.LocationName).ToList();
                GetAccountDetails();
                BindTime();
                return View();
            }
            catch
            {
                return View();
            }
        }

        #region Batches
        public ActionResult Batches()
        {
            ViewBag.TrainerDetails = objDBentity.prcUserDetailsOnDemand(1).ToArray();
            ViewBag.Location = objDBentity.sp_GetAllLocation().ToList();
            GetAccountDetails();
            BindTime();
            return View();
        }
        
        public JsonResult getBatchesOnDemand(string BatchTime, string TrainerCode, string LocationCode)
        {
            string TmpTrainerCode = string.IsNullOrEmpty(TrainerCode) ? null : TrainerCode;
            string TmpLocationCode = string.IsNullOrEmpty(LocationCode) ? null : LocationCode;
            GetAccountDetails();
            var getBatchesOld = objDBentity.prcBatchesOnDemand(Convert.ToInt32(BatchTime), TmpTrainerCode, TmpLocationCode, null).ToArray();
            var getBatchesNew = objDBentity.prcGetBatchesOnDemand(Convert.ToInt32(BatchTime), TmpTrainerCode, TmpLocationCode, null).ToArray();
            return Json(getBatchesNew, JsonRequestBehavior.AllowGet);

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
        [HttpPost]
        public ActionResult CreateBatch(FormCollection fc)
        {
            //string s = fc["dropBindTime"].ToString();
            //TimeSpan ts = DateTime.ParseExact(fc["dropBindTime"].ToString(), "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            //if you really need a TimeSpan this will get the time elapsed since midnight:

            //TimeSpan time = DateTime.ParseExact(fc["dropBindTime"].ToString(), "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            //string amanj = ts.ToString(@"hh\:mm");
            //TimeSpan time = TimeSpan.Parse("07:35 AM");
            //var aman = TimeSpan.Parse(fc["dropBindTime"]);
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(fc["txtBatchName"].Trim()) == true)
            {
                ContentResult.Add("Batch name is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtBatchName"].Length >= 30)
            {
                ContentResult.Add("Batch name must be less than 30 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtLocation"].Trim()) == true)
            {
                ContentResult.Add("location is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            int n;
            bool isNumeric = int.TryParse(fc["txtLocation"].Trim(), out n);
            if (isNumeric == true)
            {
                ContentResult.Add("location can not be numeric.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtLocation"].Length >= 20)
            {
                ContentResult.Add("Location must be less than 20 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtAdress"].Trim()) == true)
            {
                ContentResult.Add("Address is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtAdress"].Length >= 100)
            {
                ContentResult.Add("Address must be less than 100 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtBxBatchStartDate"]) == true)
            {
                ContentResult.Add("Start date is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtBxBatchEndDate"]) == true)
            {
                ContentResult.Add("End date is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["dropBindTime"]) == true)
            {
                ContentResult.Add("Batch time is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            //if (fc["dropBindTime"] == "")
            //{
            //    ContentResult.Add("Time is required.");
            //    ContentResult.Add("0");
            //    goto Exitlabel;
            //}
            BtchDtl.StartDate = cmnF.StringToDate(fc["txtBxBatchStartDate"]);
            BtchDtl.EndDate = cmnF.StringToDate(fc["txtBxBatchEndDate"]);
            if (BtchDtl.StartDate > BtchDtl.EndDate)
            {
                ContentResult.Add("Start date should be less than end date.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            BtchDtl.BatchName = fc["txtBatchName"].Trim();
            BtchDtl.Location = fc["txtLocation"].Trim();
            BtchDtl.Address = fc["txtAdress"].Trim();
            BtchDtl.StringStartDate = fc["txtBxBatchStartDate"];
            BtchDtl.StringEndDate = fc["txtBxBatchEndDate"];
            BtchDtl.Time = DateTime.ParseExact(fc["dropBindTime"].ToString(), "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            /*Uncomment it for 24 hr format
            BtchDtl.Time = TimeSpan.Parse(fc["dropBindTime"].ToString());
            */
            //
            //.ToString(@"hh\:mm");

            var TempResult1 = cmnF.GetIntegerPrimaryKeyOnDataExist("BatchMaster", "BatchName", BtchDtl.BatchName, "BatchCode");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                var t1 = fc["hdnBatchCode"].ToUpper();
                if (fc["hdnBatchCode"].ToUpper() == TempResult1.ToUpper())
                {

                }
                else
                {
                    ContentResult.Add("Batch by this name already exists. Please try with a different batch name.");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
            }
            var TempResult2 = cmnF.GetPrimaryKeyOnDataExist("LocationMaster", "LocationName", BtchDtl.Location, "LocationCode");
            if (string.IsNullOrEmpty(TempResult2))
            {
                BtchDtl.LocationCode = null;
            }
            else
            {
                BtchDtl.LocationCode = TempResult2;
            }
            var u = fc["hdnBatchCode"].ToUpper();
            if (fc["hdnBatchCode"].ToUpper() == "0")
            {
                objDBentity.prcCreateBatch(BtchDtl.BatchName, BtchDtl.Location, BtchDtl.Address, BtchDtl.StartDate, BtchDtl.EndDate, BtchDtl.LocationCode, BtchDtl.Time, null, "INSERT");
                ContentResult.Add("0");
                ContentResult.Add("1");
            }
            if (fc["hdnBatchCode"].ToUpper() != "0")
            {
                objDBentity.prcCreateBatch(BtchDtl.BatchName, BtchDtl.Location, BtchDtl.Address, BtchDtl.StartDate, BtchDtl.EndDate, BtchDtl.LocationCode, BtchDtl.Time, fc["hdnBatchCode"], "UPDATE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleBatchDetails(string BatchCode)
        {
            //GetAccountDetails();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return null;
            }
            List<object> SingleBatchDetails = new List<object>();
            var Detail1 = objDBentity.prcGetBatchesOnDemand(0, null, null, BatchCode).ToArray();
            //var Detail1 = objDBentity.BatchMasters.Where(x => x.BatchCode == BatchCode);
            SingleBatchDetails.Add(Detail1);
            var vg = Json(SingleBatchDetails, JsonRequestBehavior.AllowGet);
            return Json(SingleBatchDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteSingleBatch(string BatchCode)
        {
            List<string> ContentResult = new List<string>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return null;
            }
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            Int32 TmpIntegerBatchCode = Convert.ToInt32(TmpBatchCode);
            var TempResult1 = (from BSM in objDBentity.BatchAndStudentMappings
                               where BSM.BatchCode == TmpIntegerBatchCode
                               select BSM.BatchCode).ToList();
            var TempResult2 = (from BSM in objDBentity.BatchAndTrainerMappings
                               where BSM.BatchCode == TmpIntegerBatchCode
                               select BSM.BatchCode).ToList();
            if (TempResult1.Count > 0 || TempResult2.Count > 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult1.Count <= 0 && TempResult2.Count <= 0)
            {
                objDBentity.prcCreateBatch(null, null, null, null, null, null, null, BatchCode, "DELETE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BatchDownloadExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "Batch" + ".xls";
            //Bind data list from edmx
            var getKeywords = objDBentity.prcGetBatchesOnDemand(0, null, null, null).ToList();

            DataTable dt = new DataTable();
            DataTable dtChild = new DataTable();
            dt = LINQToDataTable(getKeywords);
            string oldBatchCode = "";
            string TrainerName = "";


            sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
            sb.Append("<caption>Batch List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
            sb.Append("<thead><tr>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>S.No</b></th>");
            sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Batches</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Trainers</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Students</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Locations</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Address</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Start Date</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>End Date</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Time</b></th>");
            sb.Append("</thead></tr><tbody>");

            int inc = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (oldBatchCode != dt.Rows[i]["BatchCode"].ToString())
                {

                    oldBatchCode = dt.Rows[i]["BatchCode"].ToString();
                    TrainerName = "";
                    DataRow[] filteredRows = dt.Select("BatchCode = '" + oldBatchCode + "'");

                    foreach (DataRow childItem in filteredRows)
                    {
                        TrainerName = TrainerName + ", " + childItem["TrainerName"].ToString();
                    }


                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + removdash(dt.Rows[i]["BatchName"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(TrainerName.Substring(1)) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["StudentCount"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["Location"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["Address"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["StartDate"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["EndDate"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["Time"].ToString()) + "</td>");
                    sb.Append("</tr>");


                }
            }
            sb.Append("</tbody></table>");
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }

        #region BatchView

        public ActionResult BatchView()
        {
            GetAccountDetails();
            return View();
        }
        public JsonResult getStudentToBatch(string StudentName)
        {
            var StudentToBatch = from UM in objDBentity.UserMasters
                                 join BSM in objDBentity.BatchAndStudentMappings
                                 on UM.UserID equals BSM.UserID into Info
                                 from temp in Info.DefaultIfEmpty()
                                 where UM.UserType == 0 && UM.IsActive == true && temp.BatchCode == null && (UM.FirstName + " " + UM.MiddleName + " " + UM.LastName + " ( " + UM.Email + " ) ").ToLower().Contains(StudentName.ToLower())
                                 orderby UM.FirstName, UM.MiddleName, UM.LastName ascending
                                 select new
                                 {
                                     ID = UM.UserID,
                                     //var jbjk = ((string.IsNullOrEmpty(UM.LastName)) ? UM.MiddleName : "");
                                     Name = UM.FirstName + " " + UM.MiddleName + " " + UM.LastName + " ( " + UM.Email + " )",
                                 };

            return Json(StudentToBatch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getTrainerToBatch(string TrainerName, string BatchCode)
        {
            string TmpTrainerName = string.IsNullOrEmpty(TrainerName) ? null : TrainerName;
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            var TrainerToBatch = objDBentity.getTrainerToBatch(TmpTrainerName, TmpBatchCode).ToArray();
            return Json(TrainerToBatch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAssignedStudentsPerBatch(string BatchCode)
        {
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            var getBatches = objDBentity.getAssignedStudentsPerBatch(TmpBatchCode, "STUDENT").ToArray();
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAssignedTrainersPerBatch(string BatchCode)
        {
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            var getBatches = objDBentity.getAssignedStudentsPerBatch(TmpBatchCode, "TRAINER").ToArray();
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AssignedStudentPerBatch(string BatchCode, string UserID)
        {
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            string TmpUserID = string.IsNullOrEmpty(UserID) ? null : UserID;

            var getBatches = objDBentity.prcAssignedStudentsAndTrainer(TmpBatchCode, TmpUserID, "STUDENT", "0", "");
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AssignedTrainerPerBatch(string BatchCode, string UserID)
        {
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            string TmpUserID = string.IsNullOrEmpty(UserID) ? null : UserID;

            var getBatches = objDBentity.prcAssignedStudentsAndTrainer(TmpBatchCode, TmpUserID, "TRAINER", "0", "");
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteAssignedStudentPerBatch(string BatchCode, string UserID)
        {
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            string TmpUserID = string.IsNullOrEmpty(UserID) ? null : UserID;

            var getBatches = objDBentity.prcAssignedStudentsAndTrainer(TmpBatchCode, TmpUserID, "STUDENT", "", "DELETE");
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteAssignedTrainerPerBatch(string BatchCode, string UserID)
        {
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            string TmpUserID = string.IsNullOrEmpty(UserID) ? null : UserID;

            var getBatches = objDBentity.prcAssignedStudentsAndTrainer(TmpBatchCode, TmpUserID, "TRAINER", "", "DELETE");
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FinalAssignStudentAndTrainersToBatch(string BatchCode)
        {
            List<string> ContentResult = new List<string>();

            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            Int32 TmpIntegerBatchCode = Convert.ToInt32(TmpBatchCode);

            var TempResult1 = (from BSM in objDBentity.BatchAndStudentMappings
                               where BSM.BatchCode == TmpIntegerBatchCode
                               where BSM.isMappingActive == false
                               select BSM.BatchCode).ToList();
            var TempResult2 = (from BSM in objDBentity.BatchAndTrainerMappings
                               where BSM.BatchCode == TmpIntegerBatchCode
                               where BSM.isMappingActive == false
                               select BSM.BatchCode).ToList();
            if (TempResult1.Count <= 0 && TempResult2.Count <= 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult1.Count > 0 || TempResult2.Count > 0)
            {
                var AssignedStudents = objDBentity.prcAssignedStudentsAndTrainer(TmpBatchCode, "", "STUDENT", "1", "");
                var AssignedTrainers = objDBentity.prcAssignedStudentsAndTrainer(TmpBatchCode, "", "TRAINER", "1", "");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region Trainers

        public ActionResult Trainers()
        {
            ViewBag.TrainerDetails = objDBentity.prcUserDetailsOnDemand(1).ToArray();
            ViewBag.BatchDetails = objDBentity.BatchMasters.Where(x => x.isActive == true).OrderBy(x => x.BatchName).ToList();
            ViewBag.Location = objDBentity.sp_Get_Location_UserMaster(1).ToList();
            GetAccountDetails();
            return View();

        }
        public JsonResult getTrainersOnDemand(string TrainerCode, string BatchCode, string LocationCode)
        {
            string TmpTrainerCode = string.IsNullOrEmpty(TrainerCode) ? null : TrainerCode;
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            string TmpLocationCode = string.IsNullOrEmpty(LocationCode) ? null : LocationCode;

            GetAccountDetails();
            var getTrainers = objDBentity.prcTrainerOnDemand(TmpBatchCode, TmpLocationCode, TmpTrainerCode).ToArray();
            List<string> getTrainersList = new List<string>();

            for (int i = 0; i < getTrainers.Length; i++)
            {

            }
            return Json(getTrainers, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CreateTrainer(FormCollection fc)
        {
            stdtDtl = new Student();
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(fc["txtFirstName"].Trim()) == true)
            {
                ContentResult.Add("First Name is required.");//to show error calidation message
                ContentResult.Add("0");                      //to show response of stored procedure has not executed
                goto Exitlabel;
                //return Content("Batch Name is required.");

            }
            if (fc["txtFirstName"].Length >= 15)
            {
                ContentResult.Add("First name must be less than 15 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            //if (string.IsNullOrEmpty(fc["txtMiddleName"]) == true)
            //{
            //    ContentResult.Add("Middle Name is required.");
            //    ContentResult.Add("0");
            //    goto Exitlabel;
            //    //return Content("Batch Name is required.");
            //}
            if (string.IsNullOrEmpty(fc["txtLastName"].Trim()) == true)
            {
                ContentResult.Add("Last Name is required.");
                ContentResult.Add("0");
                goto Exitlabel;
                //return Content("Batch Name is required.");

            }
            if (fc["txtLastName"].Length >= 15)
            {
                ContentResult.Add("Last name must be less than 15 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtLocation"].Trim()) == true)
            {
                ContentResult.Add("Location is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            int n;
            bool isNumeric = int.TryParse(fc["txtLocation"].Trim(), out n);
            if (isNumeric == true)
            {
                ContentResult.Add("location can not be numeric.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtLocation"].Length >= 20)
            {
                ContentResult.Add("Location must be less than 20 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtEmail"].Trim()) == true)
            {
                ContentResult.Add("Email ID is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtContact"].Trim()) == true)
            {

                ContentResult.Add("Contact is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtContact"].Length != 10)
            {
                ContentResult.Add("Contact must be in 10 digit.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            stdtDtl.FirstName = fc["txtFirstName"].Trim();
            stdtDtl.MiddleName = fc["txtMiddleName"].Trim();
            stdtDtl.LastName = fc["txtLastName"].Trim();
            stdtDtl.Location = fc["txtLocation"].Trim();
            stdtDtl.Email = fc["txtEmail"].Trim();
            stdtDtl.Contact = fc["txtContact"].Trim();

            var TempResult1 = cmnF.GetPrimaryKeyOnDataExist("UserMaster", "Email", fc["txtEmail"].Trim(), "UserID");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                if (fc["hdnTrainerCode"].ToUpper() == TempResult1.ToUpper())
                {

                }
                else
                {
                    ContentResult.Add("This Email ID already exists. Please try with a different Email ID.");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
            }

            var TempResult2 = cmnF.GetPrimaryKeyOnDataExist("LocationMaster", "LocationName", stdtDtl.Location, "LocationCode");
            if (string.IsNullOrEmpty(TempResult2))
            {
                stdtDtl.LocationCode = null;
            }
            else
            {
                stdtDtl.LocationCode = TempResult2;
            }
            var u = fc["hdnTrainerCode"].ToUpper();

            //if hidden field- fc["hdnStudentCode"] value is zero means create code will work
            //if hidden field- fc["hdnStudentCode"] value is zero means Edit code will work
            if (fc["hdnTrainerCode"].ToUpper() == "0")
            {
                randomPwd = "";
                randomPwd = cmnF.RandomString(6);

                //randomPwd = "abc@123";

                objDBentity.prcCreateTrainer(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, randomPwd, stdtDtl.Contact, stdtDtl.LocationCode, null, "INSERT");
                cmnF.SendPasscodeToMail(stdtDtl.Email, stdtDtl.FirstName, randomPwd, stdtDtl.Email, "whisskers login details");
                ContentResult.Add("0");//to show response of create message
                ContentResult.Add("1");//to show response of stored procedure working correctly
            }
            if (fc["hdnTrainerCode"].ToUpper() != "0")
            {
                objDBentity.prcCreateTrainer(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, null, stdtDtl.Contact, stdtDtl.LocationCode, fc["hdnTrainerCode"], "UPDATE");
                ContentResult.Add("1");//to show response of edit message
                ContentResult.Add("1");//to show response of stored procedure working correctly
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleTrainerDetails(string TrainerCode)
        {
            //GetAccountDetails();
            if (TrainerCode == null || TrainerCode == "" || TrainerCode == "undefined")
            {
                return null;
            }
            List<object> SingleTrainerDetails = new List<object>();
            var Detail1 = objDBentity.prcTrainerOnDemand(null, null, TrainerCode).ToArray();
            //var Detail1 = objDBentity.BatchMasters.Where(x => x.BatchCode == BatchCode);
            SingleTrainerDetails.Add(Detail1);
            var vg = Json(SingleTrainerDetails, JsonRequestBehavior.AllowGet);
            return Json(SingleTrainerDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteSingleTrainer(string TrainerCode)
        {
            List<string> ContentResult = new List<string>();
            if (TrainerCode == null || TrainerCode == "" || TrainerCode == "undefined")
            {
                return null;
            }
            string TmpTrainerCode = string.IsNullOrEmpty(TrainerCode) ? null : TrainerCode;
            var TempResult2 = (from BTM in objDBentity.BatchAndTrainerMappings
                               where BTM.UserID == TmpTrainerCode
                               select BTM.BatchCode).ToList();
            if (TempResult2.Count > 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult2.Count <= 0)
            {
                objDBentity.prcCreateTrainer(null, null, null, null, null, null, null, null, TrainerCode, "DELETE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveTrainerExcel(FormCollection fc, IEnumerable<HttpPostedFileBase> excelfile)
        {
            List<string> ContentResult = new List<string>();
            //try
            //{
                var ExcelImagePath = "";
                var FileExtension = "";
                var fileName = "";
                var newPath = "";
                var path = "";
                var errDropdown = "";
                DataTable errDT = new DataTable();
                errDT.Columns.Add("Row No");
                errDT.Columns.Add("Description");
                DataSet ds = new DataSet();
                DataTable objDt1 = ds.Tables.Add("DataTable1");
                ds.Tables[0].Columns.Add("FirstName");
                ds.Tables[0].Columns.Add("MiddleName");
                ds.Tables[0].Columns.Add("LastName");
                ds.Tables[0].Columns.Add("Location");
                ds.Tables[0].Columns.Add("Email");
                ds.Tables[0].Columns.Add("Contact");
                ds.Tables[0].Columns.Add("LocationCode");
                ds.Tables[0].Columns.Add("Status");
                stdtDtl = new Student();

                HttpPostedFileBase UploadedFile = excelfile.FirstOrDefault();

                if (UploadedFile == null && fc["hdnIdenType"].ToUpper() != "TRAINER")
                {
                    ContentResult.Add("Please Upload Excel");//to show error calidation message
                    ContentResult.Add("0");                           //to show response of stored procedure has not executed
                    goto Exitlabel;
                }
                if (UploadedFile != null)
                {
                    if (UploadedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                        string[] AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".csv" };
                        if (!AllowedFileExtensions.Contains
                           (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.'))))
                        {
                            //ContentResult.Add("Choose valid file types are: " + string.Join(", ", AllowedFileExtensions));//to show error calidation message
                            ContentResult.Add("csv file is required");
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;

                        }
                        else if (UploadedFile.ContentLength > MaxContentLength)
                        {
                            ContentResult.Add("Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");//to show error calidation message
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;
                        }
                        fileName = Path.GetFileName(UploadedFile.FileName);
                        newPath = cmnF.getCurrentDateTime().ToString("yyyyMMddHHmmss") + '_' + fileName;
                        FileExtension = (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.')));
                    }
                }
                ExcelImagePath = "/ExcelFiles/" + newPath;
                if (fc["hdnIdenType"].ToUpper() == "TRAINER")
                {
                    path = Path.Combine(Server.MapPath("~/ExcelFiles/"), newPath);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    UploadedFile.SaveAs(path);
                    string excelConnectionString = string.Empty;


                var ValidCSV = cmnF.ValidateCsv(path.ToString(), 6);
                if (ValidCSV == false)
                {
                    ContentResult.Add("Invalid csv file");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
                cmnF.ConvertCsvToDatatable(path.ToString(), ds.Tables[0]);


                    //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    ////connection String for xls file format.
                    //if (FileExtension == ".xls")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////connection String for xlsx file format.
                    //else if (FileExtension == ".xlsx")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////Create Connection to Excel work book and add oledb namespace
                    //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    //excelConnection.Open();
                    //DataTable dt = new DataTable();

                    //dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //if (dt == null)
                    //{
                    //    return null;
                    //}
                    //String[] excelSheets = new String[dt.Rows.Count];
                    //int t = 0;
                    //int mailuid = 0; ;
                    ////excel data saves in temp file here.
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    excelSheets[t] = row["TABLE_NAME"].ToString();
                    //    t++;
                    //}
                    //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    //{
                    //    dataAdapter.Fill(ds);
                    //}
                    List<string> TempContentResult = new List<string>();
                    List<string> InCorrectRows = new List<string>();
                    int unsuccessfulCount = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        stdtDtl.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString().Trim();
                        stdtDtl.MiddleName = ds.Tables[0].Rows[i]["MiddleName"].ToString().Trim();
                        stdtDtl.LastName = ds.Tables[0].Rows[i]["LastName"].ToString().Trim();
                        stdtDtl.Location = ds.Tables[0].Rows[i]["Location"].ToString().Trim();
                        stdtDtl.Email = ds.Tables[0].Rows[i]["Email"].ToString().Trim();
                        stdtDtl.Contact = ds.Tables[0].Rows[i]["Contact"].ToString().Trim();


                        ds.Tables[0].Rows[i]["FirstName"] = ds.Tables[0].Rows[i]["FirstName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["MiddleName"] = ds.Tables[0].Rows[i]["MiddleName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["LastName"] = ds.Tables[0].Rows[i]["LastName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Location"] = ds.Tables[0].Rows[i]["Location"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Email"] = ds.Tables[0].Rows[i]["Email"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Contact"] = ds.Tables[0].Rows[i]["Contact"].ToString().Trim();

                        TempContentResult = CheckValidStudent();
                        if (TempContentResult[0] == "1")
                        {
                            ds.Tables[0].Rows[i]["LocationCode"] = stdtDtl.LocationCode;
                            ds.Tables[0].Rows[i]["Status"] = "RIGHT";
                            //objDBentity.prcCreateTrainer(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, null, stdtDtl.Contact, stdtDtl.LocationCode, null, "INSERT");
                        }
                        if (TempContentResult[0] == "0")
                        {
                            DataRow dr = errDT.NewRow();

                            dr[0] = i + 1;
                            dr[1] = TempContentResult[1];
                            errDT.Rows.Add(dr);

                            InCorrectRows.Add((i + 1).ToString());
                            unsuccessfulCount++;
                        }
                    }
                    if (InCorrectRows.Count == 0 && unsuccessfulCount == 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["Status"].ToString() == "RIGHT")
                            {
                                objDBentity.prcCreateTrainer(ds.Tables[0].Rows[i]["FirstName"].ToString(), ds.Tables[0].Rows[i]["MiddleName"].ToString(), ds.Tables[0].Rows[i]["LastName"].ToString(), ds.Tables[0].Rows[i]["Location"].ToString(), ds.Tables[0].Rows[i]["Email"].ToString(), null, ds.Tables[0].Rows[i]["Contact"].ToString(), ds.Tables[0].Rows[i]["LocationCode"].ToString(), null, "INSERT");
                            }
                        }
                        ContentResult.Add("1");
                    }
                    else
                    {
                        ContentResult.Add("0");
                    }
                    ContentResult.Add("Total rows " + ds.Tables[0].Rows.Count);
                    ContentResult.Add("Correct rows " + (Convert.ToInt32(ds.Tables[0].Rows.Count) - unsuccessfulCount).ToString());
                    ContentResult.Add("Incorrect rows " + InCorrectRows.Count);
                    string res = String.Join(Environment.NewLine, errDT.Rows.OfType<DataRow>().Select(x => String.Join(",", x.ItemArray)));
                    if (InCorrectRows.Count > 0)
                    {
                        ContentResult.Add("Incorrect rows index are  " + string.Join(", ", InCorrectRows));
                    }
                    else
                    {
                        ContentResult.Add("");
                    }
                    ContentResult.Add(res);
                    //excelConnection.Close();
                    // objDBentity.prcCreateAccount(accDtl.AccountName, accDtl.ImagePath, accDtl.AccountDescription, null, "INSERT");
                    // ContentResult.Add("0");//to show response of create message
                    // ContentResult.Add("1");//to show response of stored procedure working correctly
                }
            Exitlabel:
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                errDropdown = String.Join(Environment.NewLine, (from r in errDT.AsEnumerable()
                                                                select r["Description"]).Distinct().ToList());
                ContentResult.Add(errDropdown);
                return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    ContentResult.Clear();
            //    ContentResult.Add("Invalid csv file");
            //    ContentResult.Add("0");
            //    return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
        }
        public List<string> CheckValidStudent()
        {
            //1 means true and zero means false
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(stdtDtl.FirstName) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid first name");
                goto Exitlabel;
            }
            if (stdtDtl.FirstName.Length >= 15)
            {
                ContentResult.Add("0");
                ContentResult.Add("First name must be less than 15 characters");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(stdtDtl.MiddleName) != true)
            {
                if (stdtDtl.MiddleName.Length >= 15)
                {
                    ContentResult.Add("0");
                    ContentResult.Add("Middle name must be less than 15 characters");
                    goto Exitlabel;
                }
            }
            if (string.IsNullOrEmpty(stdtDtl.LastName) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid last name");
                goto Exitlabel;
            }
            if (stdtDtl.LastName.Length >= 15)
            {
                ContentResult.Add("0");
                ContentResult.Add("last name must be less than 15 characters");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(stdtDtl.Location) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid location name");
                goto Exitlabel;
            }
            if (stdtDtl.Location.Length >= 20)
            {
                ContentResult.Add("0");
                ContentResult.Add("Location must be less than 15 characters");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(stdtDtl.Email) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid Email ID");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(stdtDtl.Contact) == true || stdtDtl.Contact.Length != 10)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid mobile no.");
                goto Exitlabel;
            }
            bool isMobile = Regex.IsMatch(stdtDtl.Contact.Trim(), @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
            if (!isMobile)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid mobile no.");
                goto Exitlabel;
            }
            var TempResult1 = cmnF.GetPrimaryKeyOnDataExist("UserMaster", "Email", stdtDtl.Email, "UserID");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Email ID already exists");
                goto Exitlabel;
            }
            var TempResult2 = cmnF.GetPrimaryKeyOnDataExist("LocationMaster", "LocationName", stdtDtl.Location, "LocationCode");
            if (string.IsNullOrEmpty(TempResult2))
            {
                stdtDtl.LocationCode = null;
            }
            else
            {
                stdtDtl.LocationCode = TempResult2;
            }
            ContentResult.Add("1");
            ContentResult.Add(stdtDtl.LocationCode);
        Exitlabel:
            return ContentResult;
        }
        public ActionResult TrainerDownloadExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "Trainer" + ".xls";
            //Bind data list from edmx
            var getKeywords = objDBentity.prcTrainerOnDemand(null, null, null).ToList();

            DataTable dt = new DataTable();
            DataTable dtChild = new DataTable();
            dt = LINQToDataTable(getKeywords);
            string oldEmail = "";
            string BatchName = "";


            sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
            sb.Append("<caption>Trainer List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
            sb.Append("<thead><tr>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>S.No</b></th>");
            sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Name</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Location</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Email</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Contact</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Batch</b></th>");
            sb.Append("</thead></tr><tbody>");

            int inc = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (oldEmail != dt.Rows[i]["Email"].ToString())
                {

                    oldEmail = dt.Rows[i]["Email"].ToString();
                    BatchName = "";
                    DataRow[] filteredRows = dt.Select("Email = '" + oldEmail + "'");

                    foreach (DataRow childItem in filteredRows)
                    {
                        BatchName = BatchName + ", " + childItem["BatchName"].ToString();
                    }
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + removdash(dt.Rows[i]["FullName"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["LocationName"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["Email"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(dt.Rows[i]["Contact"].ToString()) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(BatchName.ToString().Substring(1)) + " </td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("</tbody></table>");
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        //public ActionResult TrainerDownloadTemplateExcel()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string sFileName = "TrainerTemplate" + ".xls";

        //    sb.Append("<table>");
        //    sb.Append("<thead><tr>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>FirstName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>MiddleName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>LastName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Location</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Email</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Contact</b></th>");
        //    sb.Append("</tr></thead>");
        //    sb.Append("</table>");

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        //    return File(buffer, "application/vnd.ms-excel");
        //}
        public ActionResult TrainerDownloadTemplateCSV()
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            //csv += "Search Query" + ',';
            csv += "FirstName" + ',';
            csv += "MiddleName" + ',';
            csv += "LastName" + ',';
            csv += "Location" + ',';
            csv += "Email" + ',';
            csv += "Contact";
            //Add new line.
            csv += "\r\n";
            //Add the Data rows.
            //csv += "Online sports shoes in india".Replace(",", ";") + ',';
            csv += "aman".Replace(",", ";") + ',';
            csv += "kumar".Replace(",", ";") + ',';
            csv += "jain".Replace(",", ";") + ',';
            csv += "agra".Replace(",", ";") + ',';
            csv += "aman@gmail.com".Replace(",", ";") + ',';
            csv += "8958956895".Replace(",", ";");
            //Add new line.
            //csv += "\r\n";

            string sFileName = "TrainerTemplate" + ".csv";
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }
        #endregion

        #region Students

        public ActionResult Students()
        {

                ViewBag.BatchDetails = objDBentity.BatchMasters.Where(x => x.isActive == true).OrderBy(x => x.BatchName).ToList();
                ViewBag.Location = objDBentity.sp_Get_Location_UserMaster(0).ToList();
                GetAccountDetails();
                return View();

        }
        public JsonResult getStudentsOnDemand(string BatchCode, string LocationCode)
        {


                string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
                string TmpLocationCode = string.IsNullOrEmpty(LocationCode) ? null : LocationCode;

                GetAccountDetails();
                var getStudents = objDBentity.prcStudentOnDemand(TmpBatchCode, TmpLocationCode, null).ToArray();
                return Json(getStudents, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CreateStudent(FormCollection fc)
        {
            stdtDtl = new Student();
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(fc["txtFirstName"].Trim()) == true)
            {
                ContentResult.Add("First name is required.");//to show error calidation message
                ContentResult.Add("0");                      //to show response of stored procedure has not executed
                goto Exitlabel;
                //return Content("Batch Name is required.");

            }
            if (fc["txtFirstName"].Length >= 15)
            {
                ContentResult.Add("First name must be less than 15 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            //if (string.IsNullOrEmpty(fc["txtMiddleName"]) == true)
            //{
            //    ContentResult.Add("Middle Name is required.");
            //    ContentResult.Add("0");
            //    goto Exitlabel;
            //    //return Content("Batch Name is required.");
            //}
            if (string.IsNullOrEmpty(fc["txtLastName"].Trim()) == true)
            {
                ContentResult.Add("Last name is required.");
                ContentResult.Add("0");
                goto Exitlabel;
                //return Content("Batch Name is required.");

            }
            if (fc["txtLastName"].Length >= 15)
            {
                ContentResult.Add("Last name must be less than 15 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtLocation"].Trim()) == true)
            {
                ContentResult.Add("Location is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            int n;
            bool isNumeric = int.TryParse(fc["txtLocation"].Trim(), out n);
            if (isNumeric == true)
            {
                ContentResult.Add("location can not be numeric.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtLocation"].Length >= 20)
            {
                ContentResult.Add("Location must be less than 20 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtEmail"].Trim()) == true)
            {
                ContentResult.Add("Email ID is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtContact"].Trim()) == true)
            {

                ContentResult.Add("Contact is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtContact"].Length != 10)
            {
                ContentResult.Add("Contact must be in 10 digit.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            stdtDtl.FirstName = fc["txtFirstName"].Trim();
            stdtDtl.MiddleName = fc["txtMiddleName"].Trim();
            stdtDtl.LastName = fc["txtLastName"].Trim();
            stdtDtl.Location = fc["txtLocation"].Trim();
            stdtDtl.Email = fc["txtEmail"].Trim();
            stdtDtl.Contact = fc["txtContact"].Trim();

            var TempResult1 = cmnF.GetPrimaryKeyOnDataExist("UserMaster", "Email", fc["txtEmail"].Trim(), "UserID");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                if (fc["hdnStudentCode"].ToUpper() == TempResult1.ToUpper())
                {

                }
                else
                {
                    ContentResult.Add("This Email ID already exists. Please try with a different Email ID.");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
            }

            var TempResult2 = cmnF.GetPrimaryKeyOnDataExist("LocationMaster", "LocationName", stdtDtl.Location, "LocationCode");
            if (string.IsNullOrEmpty(TempResult2))
            {
                stdtDtl.LocationCode = null;
            }
            else
            {
                stdtDtl.LocationCode = TempResult2;
            }
            var u = fc["hdnStudentCode"].ToUpper();

            //if hidden field- fc["hdnStudentCode"] value is zero means create code will work
            //if hidden field- fc["hdnStudentCode"] value is zero means Edit code will work
            if (fc["hdnStudentCode"].ToUpper() == "0")
            {
                randomPwd = "";
                randomPwd = cmnF.RandomString(6);

                //randomPwd = "abc@123";

                objDBentity.prcCreateStudent(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, randomPwd, stdtDtl.Contact, stdtDtl.LocationCode, null, "INSERT");
                cmnF.SendPasscodeToMail(stdtDtl.Email, stdtDtl.FirstName, randomPwd, stdtDtl.Email, "whisskers login details");
                ContentResult.Add("0");//to show response of create message
                ContentResult.Add("1");//to show response of stored procedure working correctly
            }
            if (fc["hdnStudentCode"].ToUpper() != "0")
            {
                objDBentity.prcCreateStudent(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, null, stdtDtl.Contact, stdtDtl.LocationCode, fc["hdnStudentCode"], "UPDATE");
                ContentResult.Add("1");//to show response of edit message
                ContentResult.Add("1");//to show response of stored procedure working correctly
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleStudentDetails(string StudentCode)
        {
            //GetAccountDetails();
            if (StudentCode == null || StudentCode == "" || StudentCode == "undefined")
            {
                return null;
            }
            List<object> SingleStudentDetails = new List<object>();
            var Detail1 = objDBentity.prcStudentOnDemand(null, null, StudentCode).ToArray();
            //var Detail1 = objDBentity.BatchMasters.Where(x => x.BatchCode == BatchCode);
            SingleStudentDetails.Add(Detail1);
            var vg = Json(SingleStudentDetails, JsonRequestBehavior.AllowGet);
            return Json(SingleStudentDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteSingleStudent(string StudentCode)
        {
            List<string> ContentResult = new List<string>();
            if (StudentCode == null || StudentCode == "" || StudentCode == "undefined")
            {
                return null;
            }
            string TmpStudentCode = string.IsNullOrEmpty(StudentCode) ? null : StudentCode;
            var TempResult2 = (from BSM in objDBentity.BatchAndStudentMappings
                               where BSM.UserID == TmpStudentCode
                               select BSM.BatchCode).ToList();
            if (TempResult2.Count > 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult2.Count <= 0)
            {
                objDBentity.prcCreateStudent(null, null, null, null, null, null, null, null, StudentCode, "DELETE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveStudentExcel(FormCollection fc, IEnumerable<HttpPostedFileBase> excelfile)
        {
            List<string> ContentResult = new List<string>();
            //try
            //{
                var ExcelImagePath = "";
                var FileExtension = "";
                var fileName = "";
                var newPath = "";
                var path = "";
                var errDropdown = "";

                DataTable errDT = new DataTable();
                errDT.Columns.Add("Row No");
                errDT.Columns.Add("Description");
                DataSet ds = new DataSet();
                DataTable objDt1 = ds.Tables.Add("DataTable1");
                ds.Tables[0].Columns.Add("FirstName");
                ds.Tables[0].Columns.Add("MiddleName");
                ds.Tables[0].Columns.Add("LastName");
                ds.Tables[0].Columns.Add("Location");
                ds.Tables[0].Columns.Add("Email");
                ds.Tables[0].Columns.Add("Contact");
                ds.Tables[0].Columns.Add("LocationCode");
                ds.Tables[0].Columns.Add("Status");

                stdtDtl = new Student();

                HttpPostedFileBase UploadedFile = excelfile.FirstOrDefault();

                if (UploadedFile == null && fc["hdnIdenType"].ToUpper() != "STUDENT")
                {
                    ContentResult.Add("Please Upload Excel");//to show error calidation message
                    ContentResult.Add("0");                           //to show response of stored procedure has not executed
                    goto Exitlabel;
                }
                if (UploadedFile != null)
                {
                    if (UploadedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                        string[] AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".csv" };
                        if (!AllowedFileExtensions.Contains
                           (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.'))))
                        {
                            //ContentResult.Add("Choose valid file types are: " + string.Join(", ", AllowedFileExtensions));//to show error calidation message
                            ContentResult.Add("csv file is required");
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;

                        }
                        else if (UploadedFile.ContentLength > MaxContentLength)
                        {
                            ContentResult.Add("Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");//to show error calidation message
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;
                        }
                        fileName = Path.GetFileName(UploadedFile.FileName);
                        newPath = cmnF.getCurrentDateTime().ToString("yyyyMMddHHmmss") + '_' + fileName;
                        FileExtension = (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.')));
                    }
                }
                ExcelImagePath = "/ExcelFiles/" + newPath;
                if (fc["hdnIdenType"].ToUpper() == "STUDENT")
                {
                    path = Path.Combine(Server.MapPath("~/ExcelFiles/"), newPath);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    UploadedFile.SaveAs(path);
                    string excelConnectionString = string.Empty;


                var ValidCSV = cmnF.ValidateCsv(path.ToString(), 6);
                if (ValidCSV == false)
                {
                    ContentResult.Add("Invalid csv file");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
                cmnF.ConvertCsvToDatatable(path.ToString(), ds.Tables[0]);

                    //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    ////connection String for xls file format.
                    //if (FileExtension == ".xls")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////connection String for xlsx file format.
                    //else if (FileExtension == ".xlsx")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////Create Connection to Excel work book and add oledb namespace
                    //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    //excelConnection.Open();
                    //DataTable dt = new DataTable();

                    //dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //if (dt == null)
                    //{
                    //    return null;
                    //}
                    //String[] excelSheets = new String[dt.Rows.Count];
                    //int t = 0;
                    //int mailuid = 0;
                    ////excel data saves in temp file here.
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    excelSheets[t] = row["TABLE_NAME"].ToString();
                    //    t++;
                    //}
                    //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    //{
                    //    dataAdapter.Fill(ds);
                    //}

                    List<string> TempContentResult = new List<string>();
                    List<string> InCorrectRows = new List<string>();
                    int unsuccessfulCount = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        stdtDtl.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString().Trim();
                        stdtDtl.MiddleName = ds.Tables[0].Rows[i]["MiddleName"].ToString().Trim();
                        stdtDtl.LastName = ds.Tables[0].Rows[i]["LastName"].ToString().Trim();
                        stdtDtl.Location = ds.Tables[0].Rows[i]["Location"].ToString().Trim();
                        stdtDtl.Email = ds.Tables[0].Rows[i]["Email"].ToString().Trim();
                        stdtDtl.Contact = ds.Tables[0].Rows[i]["Contact"].ToString().Trim();

                        ds.Tables[0].Rows[i]["FirstName"] = ds.Tables[0].Rows[i]["FirstName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["MiddleName"] = ds.Tables[0].Rows[i]["MiddleName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["LastName"] = ds.Tables[0].Rows[i]["LastName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Location"] = ds.Tables[0].Rows[i]["Location"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Email"] = ds.Tables[0].Rows[i]["Email"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Contact"] = ds.Tables[0].Rows[i]["Contact"].ToString().Trim();

                        TempContentResult = CheckValidStudent();
                        if (TempContentResult[0] == "1")
                        {
                            ds.Tables[0].Rows[i]["LocationCode"] = stdtDtl.LocationCode;
                            ds.Tables[0].Rows[i]["Status"] = "RIGHT";
                            //objDBentity.prcCreateStudent(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, null, stdtDtl.Contact, stdtDtl.LocationCode, null, "INSERT");
                        }
                        if (TempContentResult[0] == "0")
                        {
                            DataRow dr = errDT.NewRow();

                            dr[0] = i + 1;
                            dr[1] = TempContentResult[1];
                            errDT.Rows.Add(dr);

                            InCorrectRows.Add((i + 1).ToString());
                            unsuccessfulCount++;
                        }
                    }
                    if (InCorrectRows.Count == 0 && unsuccessfulCount == 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["Status"].ToString() == "RIGHT")
                            {
                                objDBentity.prcCreateStudent(ds.Tables[0].Rows[i]["FirstName"].ToString(), ds.Tables[0].Rows[i]["MiddleName"].ToString(), ds.Tables[0].Rows[i]["LastName"].ToString(), ds.Tables[0].Rows[i]["Location"].ToString(), ds.Tables[0].Rows[i]["Email"].ToString(), null, ds.Tables[0].Rows[i]["Contact"].ToString(), ds.Tables[0].Rows[i]["LocationCode"].ToString(), null, "INSERT");
                            }
                        }
                        ContentResult.Add("1");
                    }
                    else
                    {
                        ContentResult.Add("0");
                    }
                    ContentResult.Add("Total rows " + ds.Tables[0].Rows.Count);
                    ContentResult.Add("Correct rows " + (Convert.ToInt32(ds.Tables[0].Rows.Count) - unsuccessfulCount).ToString());
                    ContentResult.Add("Incorrect rows " + InCorrectRows.Count);
                    string res = String.Join(Environment.NewLine, errDT.Rows.OfType<DataRow>().Select(x => String.Join(",", x.ItemArray)));
                    if (InCorrectRows.Count > 0)
                    {
                        ContentResult.Add("Incorrect rows index are  " + string.Join(", ", InCorrectRows));
                    }
                    else
                    {
                        ContentResult.Add("");
                    }
                    ContentResult.Add(res);
                    //excelConnection.Close();
                    // objDBentity.prcCreateAccount(accDtl.AccountName, accDtl.ImagePath, accDtl.AccountDescription, null, "INSERT");
                    // ContentResult.Add("0");//to show response of create message
                    //  ContentResult.Add("1");//to show response of stored procedure working correctly
                }
            Exitlabel:
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                errDropdown = String.Join(Environment.NewLine, (from r in errDT.AsEnumerable()
                                                                select r["Description"]).Distinct().ToList());
                ContentResult.Add(errDropdown);
                return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    ContentResult.Clear();
            //    ContentResult.Add("Invalid csv file");
            //    ContentResult.Add("0");
            //    return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
        }
        public ActionResult StudentDownloadExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "Student" + ".xls";
            //Bind data list from edmx
            var getKeywords = objDBentity.prcStudentOnDemand(null, null, null).ToArray();

            if (getKeywords != null && getKeywords.Any())
            {
                sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
                sb.Append("<caption>All Student List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>S.No</b></th>");
                sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Name</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Location</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Email</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Contact</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Batch</b></th>");
                sb.Append("</thead></tr><tbody>");
                int inc = 0;
                foreach (var result in getKeywords)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + removdash(result.FullName) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.LocationName) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.Email) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.Contact) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.BatchName) + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
            }

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        //public ActionResult StudentDownloadTemplateExcel()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string sFileName = "StudentTemplate" + ".xls";

        //    sb.Append("<table>");
        //    sb.Append("<thead><tr>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>FirstName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>MiddleName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>LastName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Location</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Email</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Contact</b></th>");
        //    sb.Append("</tr></thead>");
        //    sb.Append("</table>");

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        //    return File(buffer, "application/vnd.ms-excel");
        //}
        public ActionResult StudentDownloadTemplateCSV()
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            //csv += "Search Query" + ',';
            csv += "FirstName" + ',';
            csv += "MiddleName" + ',';
            csv += "LastName" + ',';
            csv += "Location" + ',';
            csv += "Email" + ',';
            csv += "Contact";
            //Add new line.
            csv += "\r\n";
            //Add the Data rows.
            //csv += "Online sports shoes in india".Replace(",", ";") + ',';
            csv += "aman".Replace(",", ";") + ',';
            csv += "kumar".Replace(",", ";") + ',';
            csv += "jain".Replace(",", ";") + ',';
            csv += "agra".Replace(",", ";") + ',';
            csv += "aman@gmail.com".Replace(",", ";") + ',';
            csv += "8958956895".Replace(",", ";");
            //Add new line.
            //csv += "\r\n";

            string sFileName = "StudentTemplate" + ".csv";
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }
        #endregion

        #region Keywords

        public ActionResult Keywords()
        {
            GetAccountDetails();
            return View();
        }
        public JsonResult getKeywordsOnDemand()
        {
           var getKeywords = objDBentity.KeywordMasters.Where(x => x.IsActive == true).OrderByDescending(x => x.ModifiedOn).ToList();
           return Json(getKeywords, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CreateKeyword(FormCollection fc)
        {
            KeyDtl = new Keyword();
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(fc["txtKeywordName"].Trim()) == true)
            {
                ContentResult.Add("Keyword name is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (fc["txtKeywordName"].Length >= 50)
            {
                ContentResult.Add("Keyword must be less than 50 characters.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtKeywordAvgMonthSearch"].Trim()) == true)
            {
                ContentResult.Add("Average monthly search field is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            int value1 = 0;
            if (!int.TryParse(fc["txtKeywordAvgMonthSearch"], out value1))
            {
                ContentResult.Add("Average monthly search should be numeric.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtKeywordSuggestBid"].Trim()) == true)
            {
                ContentResult.Add("Suggested bid is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            decimal value = 0;
            if (!decimal.TryParse(fc["txtKeywordSuggestBid"], out value))
            {
                ContentResult.Add("Invalid Suggested bid.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            KeyDtl.KeywordName = fc["txtKeywordName"].Trim();
            KeyDtl.KeywordAvgMonthSearch = fc["txtKeywordAvgMonthSearch"].Trim();
            KeyDtl.KeywordSuggestBid = fc["txtKeywordSuggestBid"];

            var TempResult1 = cmnF.GetIntegerPrimaryKeyOnDataExist("KeywordMaster", "KeyName", KeyDtl.KeywordName, "KeyCode");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                var t1 = fc["hdnKeywordCode"].ToUpper();
                if (fc["hdnKeywordCode"].ToUpper() == TempResult1.ToUpper())
                {

                }
                else
                {
                    ContentResult.Add("This Keyword is already exist.");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
            }
            var u = fc["hdnKeywordCode"].ToUpper();
            if (fc["hdnKeywordCode"].ToUpper() == "0")
            {
                objDBentity.prcCreateKeyword(KeyDtl.KeywordName, KeyDtl.KeywordAvgMonthSearch, cmnF.CnvrtRoundToDcimal(KeyDtl.KeywordSuggestBid, 2).ToString() , "0", null, "INSERT");
                ContentResult.Add("0");
                ContentResult.Add("1");
            }
            if (fc["hdnKeywordCode"].ToUpper() != "0")
            {
                objDBentity.prcCreateKeyword(KeyDtl.KeywordName, KeyDtl.KeywordAvgMonthSearch, cmnF.CnvrtRoundToDcimal(KeyDtl.KeywordSuggestBid, 2).ToString(), "0", fc["hdnKeywordCode"], "UPDATE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleKeywordDetails(string KeywordCode)
        {
            //GetAccountDetails();
            if (KeywordCode == null || KeywordCode == "" || KeywordCode == "undefined")
            {
                return null;
            }
            List<object> SingleKeywordDetails = new List<object>();
            Int32 FKeywordCode = Convert.ToInt32(KeywordCode);
            var Detail1 = objDBentity.KeywordMasters.Where(x => x.KeyCode == FKeywordCode && x.IsActive == true).OrderBy(x => x.KeyCode).FirstOrDefault();
            //var Detail1 = objDBentity.prcAccountOnDemand(null, null, AccountCode).ToArray();
            //var Detail1 = objDBentity.BatchMasters.Where(x => x.BatchCode == BatchCode);
            SingleKeywordDetails.Add(Detail1);
            var vg = Json(SingleKeywordDetails, JsonRequestBehavior.AllowGet);
            return Json(SingleKeywordDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteSingleKeyword(string KeywordCode)
        {
            List<string> ContentResult = new List<string>();
            if (KeywordCode == null || KeywordCode == "" || KeywordCode == "undefined")
            {
                return null;
            }
            string TmpKeywordCode = string.IsNullOrEmpty(KeywordCode) ? null : KeywordCode;
            Int32 TmpIntegerKeywordCode = Convert.ToInt32(TmpKeywordCode);
            var TempResult2 = (from MKAG in objDBentity.MapKeywordsAGs
                               where MKAG.KeyCode == TmpIntegerKeywordCode
                               select MKAG.KeyCode).ToList();
            if (TempResult2.Count > 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult2.Count <= 0)
            {
                objDBentity.prcCreateKeyword(null, null, null, null, KeywordCode, "DELETE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveKeywordExcel(FormCollection fc, IEnumerable<HttpPostedFileBase> excelfile)
        {
            List<string> ContentResult = new List<string>();
            //try
            //{
                GetAccountDetails();
                var ExcelImagePath = "";
                var FileExtension = "";
                var fileName = "";
                var newPath = "";
                var path = "";
                var errDropdown = "";

                DataTable errDT = new DataTable();
                errDT.Columns.Add("Row No");
                errDT.Columns.Add("Description");

                DataSet ds = new DataSet();
                DataTable objDt1 = ds.Tables.Add("DataTable1");
                ds.Tables[0].Columns.Add("KeywordName");
                ds.Tables[0].Columns.Add("Avg Monthely Searches");
                ds.Tables[0].Columns.Add("Suggested Bid");
                ds.Tables[0].Columns.Add("Status");
                KeyDtl = new Keyword();

                HttpPostedFileBase UploadedFile = excelfile.FirstOrDefault();

                if (UploadedFile == null && fc["hdnIdenType"].ToUpper() != "KEYWORD")
                {
                    ContentResult.Add("Please Upload Excel");//to show error calidation message
                    ContentResult.Add("0");                           //to show response of stored procedure has not executed
                    goto Exitlabel;
                }
                if (UploadedFile != null)
                {
                    if (UploadedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                        string[] AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".csv" };
                        if (!AllowedFileExtensions.Contains
                           (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.'))))
                        {
                            //ContentResult.Add("Choose valid file types are: " + string.Join(", ", AllowedFileExtensions));//to show error calidation message
                            ContentResult.Add("csv file is required");
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;

                        }
                        else if (UploadedFile.ContentLength > MaxContentLength)
                        {
                            ContentResult.Add("Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");//to show error calidation message
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;
                        }
                        fileName = Path.GetFileName(UploadedFile.FileName);
                        newPath = cmnF.getCurrentDateTime().ToString("yyyyMMddHHmmss") + '_' + fileName;
                        FileExtension = (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.')));
                    }
                }
                ExcelImagePath = "/ExcelFiles/" + newPath;
                if (fc["hdnIdenType"].ToUpper() == "KEYWORD")
                {
                    path = Path.Combine(Server.MapPath("~/ExcelFiles/"), newPath);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    UploadedFile.SaveAs(path);
                    string excelConnectionString = string.Empty;

                var ValidCSV = cmnF.ValidateCsv(path.ToString(), 3);
                if (ValidCSV == false)
                {
                    ContentResult.Add("Invalid csv file");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
                cmnF.ConvertCsvToDatatable(path.ToString(), ds.Tables[0]);

                    //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    ////connection String for xls file format.
                    //if (FileExtension == ".xls")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////connection String for xlsx file format.
                    //else if (FileExtension == ".xlsx")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////Create Connection to Excel work book and add oledb namespace
                    //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    //excelConnection.Open();
                    //DataTable dt = new DataTable();

                    //dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //if (dt == null)
                    //{
                    //    return null;
                    //}
                    //String[] excelSheets = new String[dt.Rows.Count];
                    //int t = 0;
                    //int mailuid = 0;
                    ////excel data saves in temp file here.
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    excelSheets[t] = row["TABLE_NAME"].ToString();
                    //    t++;
                    //}
                    //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    //{
                    //    dataAdapter.Fill(ds);
                    //}

                    List<string> TempContentResult = new List<string>();
                    List<string> InCorrectRows = new List<string>();
                    int unsuccessfulCount = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        KeyDtl.KeywordName = ds.Tables[0].Rows[i]["KeywordName"].ToString().Trim();
                        KeyDtl.KeywordAvgMonthSearch = ds.Tables[0].Rows[i]["Avg Monthely Searches"].ToString().Trim();
                        KeyDtl.KeywordSuggestBid = ds.Tables[0].Rows[i]["Suggested Bid"].ToString().Trim();

                        ds.Tables[0].Rows[i]["KeywordName"] = ds.Tables[0].Rows[i]["KeywordName"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Avg Monthely Searches"] = ds.Tables[0].Rows[i]["Avg Monthely Searches"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Suggested Bid"] = ds.Tables[0].Rows[i]["Suggested Bid"].ToString().Trim();



                        TempContentResult = CheckValidKeyword();
                        //, objUserInfo.UserID.ToString()
                        if (TempContentResult[0] == "1")
                        {
                            ds.Tables[0].Rows[i]["Status"] = "RIGHT";
                            //objDBentity.prcCreateKeyword(KeyDtl.KeywordName, KeyDtl.KeywordAvgMonthSearch, Convert.ToInt32(KeyDtl.KeywordSuggestBid), "0", null, "INSERT");
                        }
                        if (TempContentResult[0] == "0")
                        {
                            DataRow dr = errDT.NewRow();

                            dr[0] = i + 1;
                            dr[1] = TempContentResult[1];
                            errDT.Rows.Add(dr);

                            InCorrectRows.Add((i + 1).ToString());
                            unsuccessfulCount++;
                        }
                    }
                    if (InCorrectRows.Count == 0 && unsuccessfulCount == 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["Status"].ToString() == "RIGHT")
                            {
                                objDBentity.prcCreateKeyword(ds.Tables[0].Rows[i]["KeywordName"].ToString(), ds.Tables[0].Rows[i]["Avg Monthely Searches"].ToString(), cmnF.CnvrtRoundToDcimal(ds.Tables[0].Rows[i]["Suggested Bid"].ToString(), 2).ToString() , "0", null, "INSERT");
                            }
                        }
                        ContentResult.Add("1");
                    }
                    else
                    {
                        ContentResult.Add("0");
                    }
                    ContentResult.Add("Total rows " + ds.Tables[0].Rows.Count);
                    ContentResult.Add("Correct rows " + (Convert.ToInt32(ds.Tables[0].Rows.Count) - unsuccessfulCount).ToString());
                    ContentResult.Add("Incorrect rows " + InCorrectRows.Count);
                    string res = String.Join(Environment.NewLine, errDT.Rows.OfType<DataRow>().Select(x => String.Join(",", x.ItemArray)));
                    if (InCorrectRows.Count > 0)
                    {
                        ContentResult.Add("Incorrect rows index are  " + string.Join(", ", InCorrectRows));
                    }
                    else
                    {
                        ContentResult.Add("");
                    }
                    ContentResult.Add(res);
                    //excelConnection.Close();
                    //objDBentity.prcCreateAccount(accDtl.AccountName, accDtl.ImagePath, accDtl.AccountDescription, null, "INSERT");
                    //ContentResult.Add("0");//to show response of create message
                    //ContentResult.Add("1");//to show response of stored procedure working correctly
                }
            Exitlabel:
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                errDropdown = String.Join(Environment.NewLine, (from r in errDT.AsEnumerable()
                                                                select r["Description"]).Distinct().ToList());
                ContentResult.Add(errDropdown);
                return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    ContentResult.Clear();
            //    ContentResult.Add("Invalid csv file");
            //    ContentResult.Add("0");
            //    return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
        }
        public List<string> CheckValidKeyword()
        {
            //1 means true and zero means false
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(KeyDtl.KeywordName) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid keyword name");
                goto Exitlabel;
            }
            if (KeyDtl.KeywordName.Length >= 50)
            {
                ContentResult.Add("0");
                ContentResult.Add("Keyword name must be less than 50 characters");
                goto Exitlabel;
            }
            var TempResult1 = cmnF.GetIntegerPrimaryKeyOnDataExist("KeywordMaster", "KeyName", KeyDtl.KeywordName, "KeyCode");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Keyword name already exists");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(KeyDtl.KeywordAvgMonthSearch) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid average monthly searches");
                goto Exitlabel;
            }
            if (KeyDtl.KeywordAvgMonthSearch.Length > 5)
            {
                ContentResult.Add("0");
                ContentResult.Add("Max 5 digit value is allowed for average monthly search");
                goto Exitlabel;
            }
            int value = 0;
            if (!int.TryParse(KeyDtl.KeywordAvgMonthSearch, out value))
            {
                ContentResult.Add("0");
                ContentResult.Add("Average monthly search must be integer value");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(KeyDtl.KeywordSuggestBid) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid suggested bid");
                goto Exitlabel;
            }
            if (KeyDtl.KeywordSuggestBid.Length > 6)
            {
                ContentResult.Add("0");
                ContentResult.Add("Max 6 digit value is allowed for Suggested bid");
                goto Exitlabel;
            }
            decimal value1 = 0;
            if (!decimal.TryParse(KeyDtl.KeywordSuggestBid, out value1))
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid Suggested bid");
                goto Exitlabel;
            }
            ContentResult.Add("1");
            ContentResult.Add("1");
        Exitlabel:
            return ContentResult;
        }
        public ActionResult KeywordDownloadExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "Keyword" + ".xls";
            //Bind data list from edmx
            var getKeywords = objDBentity.KeywordMasters.Where(x => x.IsActive == true).OrderByDescending(x => x.KeyCode).ToList();

            if (getKeywords != null && getKeywords.Any())
            {
                sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
                sb.Append("<caption>All Keywords List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>S.No</b></th>");
                sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Keywords</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Avg Monthely Searches</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Suggested Bid</b></th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getKeywords)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + removdash(result.KeyName) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.AvgMonthlySrch) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.SuggestedBid.ToString()) + "</td>");

                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
            }

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        //public ActionResult KeywordDownloadTemplateExcel()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string sFileName = "KeywordTemplate" + ".xls";

        //    sb.Append("<table>");
        //    sb.Append("<thead><tr>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>KeywordName</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Avg Monthely Searches</b></th>");
        //    sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Suggested Bid</b></th>");
        //    sb.Append("</tr></thead>");
        //    sb.Append("</table>");

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        //    return File(buffer, "application/vnd.ms-excel");
        //}
        public ActionResult KeywordDownloadTemplateCSV()
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            //csv += "Search Query" + ',';
            csv += "KeywordName" + ',';
            csv += "Avg Monthely Searches" + ',';
            csv += "Suggested Bid";
            //Add new line.
            csv += "\r\n";
            //Add the Data rows.
            //csv += "Online sports shoes in india".Replace(",", ";") + ',';
            csv += "shoes online".Replace(",", ";") + ',';
            csv += "50".Replace(",", ";") + ',';
            csv += "1000".Replace(",", ";");
            //Add new line.
            //csv += "\r\n";

            string sFileName = "KeywordTemplate" + ".csv";
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }
        #endregion

        #region Phrases

        public ActionResult Phrases()
        {
            GetAccountDetails();
            return View();
        }
        [HttpPost]
        public JsonResult getPhrasesOnDemand()
        {
            //get Start (paging start index) and length (page size for paging)
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Get Sort columns value
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int totalRecords = 0;
            //var getKeywords = objDBentity.prcPhraseOnDemand().ToList();
            //var trmp = Json(getKeywords, JsonRequestBehavior.AllowGet);
            //var hg = getKeywords.OrderBy(x => x.Id);
            using (WhiskersDBEntities dc = new WhiskersDBEntities())
            {
                var v1 = (from a in dc.SearchQueryMasters orderby a.ModifiedOn ascending select a);
                var v = dc.SearchQueryMasters.AsEnumerable().Select((x, index) => new { index = index + 1, x.SearchQuery, x.Traffic, x.Id,x.ModifiedOn });
                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                totalRecords = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                //for (int i = 0; i < users.Count; i++)
                //{
                //    string user = (string)users[i];
                //    user = user.Replace("\0", "");
                //    users[i] = user;
                //}
                //for (int i = 0; i < data.Count; ++i)
                //{
                //    Int32 aa = data[i].index;
                //    data[i].index = aa;
                //}
                //var data1 = hg.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, skip = skip, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = data }, JsonRequestBehavior.AllowGet);
            }

            //try
            //{
            //    var getKeywords = objDBentity.prcPhraseOnDemand().ToList();
            //    var trmp = Json(getKeywords, JsonRequestBehavior.AllowGet);
            //    return Json(getKeywords, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }
        [HttpPost]
        public ActionResult CreatePhrase(FormCollection fc)
        {
            GetAccountDetails();
            PhraseDtl = new Phrase();
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(fc["txtPhraseName"].Trim()) == true)
            {
                ContentResult.Add("Search query is required.");
                ContentResult.Add("0");
                goto Exitlabel;

            }
            if (string.IsNullOrEmpty(fc["txtPhraseTraffic"].Trim()) == true)
            {
                ContentResult.Add("Traffic field is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            int value = 0;
            if (!int.TryParse(fc["txtPhraseTraffic"], out value))
            {
                ContentResult.Add("Traffic field should be numeric.");
                ContentResult.Add("0");
                goto Exitlabel;
            }

            PhraseDtl.PhraseName = fc["txtPhraseName"].Trim();
            PhraseDtl.Traffic = fc["txtPhraseTraffic"].Trim();
            PhraseDtl.CreatedOn = cmnF.StringToDate(cmnF.getCurrentDateTime().Date.ToString());

            var TempResult1 = cmnF.GetIntegerPrimaryKeyOnDataExist("SearchQueryMaster", "SearchQuery", PhraseDtl.PhraseName, "Id");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                var t1 = fc["hdnPhraseCode"].ToUpper();
                if (fc["hdnPhraseCode"].ToUpper() == TempResult1.ToUpper())
                {

                }
                else
                {
                    ContentResult.Add("Search query already exists. Please try with a different Search query.");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
            }
            var u = fc["hdnPhraseCode"].ToUpper();
            if (fc["hdnPhraseCode"].ToUpper() == "0")
            {
                objDBentity.prcCreatePhrase(PhraseDtl.PhraseName, PhraseDtl.CreatedOn, objUserInfo.UserID.ToString(), Convert.ToInt32(PhraseDtl.Traffic), null, null, "INSERT");
                ContentResult.Add("0");
                ContentResult.Add("1");
            }
            if (fc["hdnPhraseCode"].ToUpper() != "0")
            {
                objDBentity.prcCreatePhrase(PhraseDtl.PhraseName, null, null, Convert.ToInt32(PhraseDtl.Traffic), null, fc["hdnPhraseCode"], "UPDATE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSinglePhraseDetails(string PhraseCode)
        {
            //GetAccountDetails();
            if (PhraseCode == null || PhraseCode == "" || PhraseCode == "undefined")
            {
                return null;
            }
            List<object> SinglePhraseDetails = new List<object>();
            Int32 FPhraseCode = Convert.ToInt32(PhraseCode);
            var Detail1 = objDBentity.SearchQueryMasters.Where(x => x.Id == FPhraseCode).OrderBy(x => x.Id).FirstOrDefault();
            //var Detail1 = objDBentity.prcAccountOnDemand(null, null, AccountCode).ToArray();
            //var Detail1 = objDBentity.BatchMasters.Where(x => x.BatchCode == BatchCode);
            SinglePhraseDetails.Add(Detail1);
            var vg = Json(SinglePhraseDetails, JsonRequestBehavior.AllowGet);
            return Json(SinglePhraseDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteSinglePhrase(string PhraseCode)
        {
            List<string> ContentResult = new List<string>();
            if (PhraseCode == null || PhraseCode == "" || PhraseCode == "undefined")
            {
                return null;
            }
            string TmpPhraseCode = string.IsNullOrEmpty(PhraseCode) ? null : PhraseCode;
            Int32 TmpIntegerPhraseCode = Convert.ToInt32(TmpPhraseCode);
            //var TempResult2 = (from MKAG in objDBentity.MapKeywordsAGs
            //                   where MKAG.KeyCode == TmpIntegerPhraseCode
            //                   select MKAG.KeyCode).ToList();
            //if (TempResult2.Count > 0)
            //{
            //    ContentResult.Add("0");
            //    ContentResult.Add("0");
            //    goto Exitlabel;
            //}
            //if (TempResult2.Count <= 0)
            //{
            objDBentity.prcCreatePhrase(null, null, null, null, null, PhraseCode, "DELETE");
            ContentResult.Add("1");
            ContentResult.Add("1");
            //}
            //Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SavePhraseExcel(FormCollection fc, IEnumerable<HttpPostedFileBase> excelfile)
        {
            List<string> ContentResult = new List<string>();
            //try
            //{
                GetAccountDetails();
                var ExcelImagePath = "";
                var FileExtension = "";
                var fileName = "";
                var newPath = "";
                var path = "";
                var errDropdown = "";
                DataTable errDT = new DataTable();
                errDT.Columns.Add("Row No");
                errDT.Columns.Add("Description");

                DataSet ds = new DataSet();
                DataTable objDt1 = ds.Tables.Add("DataTable1");
                ds.Tables[0].Columns.Add("SearchQuery");
                ds.Tables[0].Columns.Add("Traffic");
                ds.Tables[0].Columns.Add("Status");

                PhraseDtl = new Phrase();
                HttpPostedFileBase UploadedFile = excelfile.FirstOrDefault();

                if (UploadedFile == null && fc["hdnIdenType"].ToUpper() != "PHRASE")
                {
                    ContentResult.Add("Please Upload csv");//to show error calidation message
                    ContentResult.Add("0");                           //to show response of stored procedure has not executed
                    goto Exitlabel;
                }
                if (UploadedFile != null)
                {
                    if (UploadedFile.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                        string[] AllowedFileExtensions = new string[] { ".csv" };
                        //string[] AllowedFileExtensions = new string[] { ".xls", ".xlsx", ".csv" };
                        if (!AllowedFileExtensions.Contains
                           (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.'))))
                        {
                            //ContentResult.Add("Choose valid file types are: " + string.Join(", ", AllowedFileExtensions));//to show error calidation message
                            //ContentResult.Add("Incorrect file type, upload valid csv file.");
                            ContentResult.Add("csv file is required");
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;

                        }
                        else if (UploadedFile.ContentLength > MaxContentLength)
                        {
                            ContentResult.Add("Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");//to show error calidation message
                            ContentResult.Add("0");                           //to show response of stored procedure has not executed
                            goto Exitlabel;
                        }
                        fileName = Path.GetFileName(UploadedFile.FileName);
                        newPath = cmnF.getCurrentDateTime().ToString("yyyyMMddHHmmss") + '_' + fileName;
                        FileExtension = (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.')));
                    }
                }
                ExcelImagePath = "/ExcelFiles/" + newPath;
                if (fc["hdnIdenType"].ToUpper() == "PHRASE")
                {
                    path = Path.Combine(Server.MapPath("~/ExcelFiles/"), newPath);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    UploadedFile.SaveAs(path);
                    string excelConnectionString = string.Empty;



                var ValidCSV = cmnF.ValidateCsv(path.ToString(), 2);
                if (ValidCSV == false)
                {
                    ContentResult.Add("Invalid csv file");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
                cmnF.ConvertCsvToDatatable(path.ToString(), ds.Tables[0]);

                    //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    ////connection String for xls file format.
                    //if (FileExtension == ".xls")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////connection String for xlsx file format.
                    //else if (FileExtension == ".xlsx")
                    //{
                    //    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //}
                    ////Create Connection to Excel work book and add oledb namespace
                    //OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    //excelConnection.Open();
                    //DataTable dt = new DataTable();

                    //dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //if (dt == null)
                    //{
                    //    return null;
                    //}
                    //String[] excelSheets = new String[dt.Rows.Count];
                    //int t = 0;
                    //int mailuid = 0;
                    ////excel data saves in temp file here.
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    excelSheets[t] = row["TABLE_NAME"].ToString();
                    //    t++;
                    //}
                    //OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                    //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    //{
                    //    dataAdapter.Fill(ds);
                    //}
                    List<string> TempContentResult = new List<string>();
                    List<string> InCorrectRows = new List<string>();
                    int unsuccessfulCount = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        PhraseDtl.PhraseName = ds.Tables[0].Rows[i]["SearchQuery"].ToString().Trim();
                        PhraseDtl.Traffic = ds.Tables[0].Rows[i]["Traffic"].ToString().Trim();



                        ds.Tables[0].Rows[i]["SearchQuery"] = ds.Tables[0].Rows[i]["SearchQuery"].ToString().Trim();
                        ds.Tables[0].Rows[i]["Traffic"] = ds.Tables[0].Rows[i]["Traffic"].ToString().Trim();

                        TempContentResult = CheckValidPhrase();
                        PhraseDtl.CreatedOn = cmnF.StringToDate(cmnF.getCurrentDateTime().Date.ToString());
                        //, objUserInfo.UserID.ToString()
                        if (TempContentResult[0] == "1")
                        {
                            ds.Tables[0].Rows[i]["Status"] = "RIGHT";
                            //objDBentity.prcCreatePhrase(PhraseDtl.PhraseName, PhraseDtl.CreatedOn, objUserInfo.UserID.ToString(), Convert.ToInt32(PhraseDtl.Traffic), null, "INSERT");
                        }
                        if (TempContentResult[0] == "0")
                        {
                            DataRow dr = errDT.NewRow();
                            dr[0] = i + 1;
                            dr[1] = TempContentResult[1];
                            errDT.Rows.Add(dr);
                            InCorrectRows.Add((i + 1).ToString());
                            unsuccessfulCount++;
                        }

                    }
                    if (InCorrectRows.Count == 0 && unsuccessfulCount == 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i]["Status"].ToString() == "RIGHT")
                            {
                                objDBentity.prcCreatePhrase(ds.Tables[0].Rows[i]["SearchQuery"].ToString(), PhraseDtl.CreatedOn, objUserInfo.UserID.ToString(), Convert.ToInt32(PhraseDtl.Traffic), PhraseDtl.Category, null, "INSERT");
                            }
                        }
                        ContentResult.Add("1");
                    }
                    else
                    {
                        ContentResult.Add("0");
                    }
                    ContentResult.Add("Total rows " + ds.Tables[0].Rows.Count);
                    ContentResult.Add("Correct rows " + (Convert.ToInt32(ds.Tables[0].Rows.Count) - unsuccessfulCount).ToString());
                    ContentResult.Add("Incorrect rows " + InCorrectRows.Count);
                    string res = String.Join(Environment.NewLine, errDT.Rows.OfType<DataRow>().Select(x => String.Join(",", x.ItemArray)));
                    if (InCorrectRows.Count > 0)
                    {
                        ContentResult.Add("Incorrect rows index are  " + string.Join(", ", InCorrectRows));
                    }
                    else
                    {
                        ContentResult.Add("");
                    }
                    ContentResult.Add(res);
                }
            Exitlabel:
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                errDropdown = String.Join(Environment.NewLine, (from r in errDT.AsEnumerable()
                                                                select r["Description"]).Distinct().ToList());
                ContentResult.Add(errDropdown);
                
                return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
            //catch
            //{
            //    ContentResult.Clear();
            //    ContentResult.Add("Invalid csv file");
            //    ContentResult.Add("0");
            //    return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
        }
        public List<string> CheckValidPhrase()
        {
            //1 means true and zero means false
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(PhraseDtl.PhraseName) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid search query");
                goto Exitlabel;
            }
            if (PhraseDtl.PhraseName.Length >= 50)
            {
                ContentResult.Add("0");
                ContentResult.Add("Search query must be less than 50 characters");
                goto Exitlabel;
            }
            //if (string.IsNullOrEmpty(PhraseDtl.Category) == true)
            //{
            //    ContentResult.Add("0");
            //    ContentResult.Add("Invalid Category");
            //    goto Exitlabel;
            //}
            //if (PhraseDtl.Category.Length >= 50)
            //{
            //    ContentResult.Add("0");
            //    ContentResult.Add("Category must be less than 50 characters");
            //    goto Exitlabel;
            //}
            if (string.IsNullOrEmpty(PhraseDtl.Traffic) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid traffic");
                goto Exitlabel;
            }
            if (PhraseDtl.Traffic.Length > 5)
            {
                ContentResult.Add("0");
                ContentResult.Add("Max 5 digit value is allowed for Traffic");
                goto Exitlabel;
            }
            int value = 0;
            if (!int.TryParse(PhraseDtl.Traffic, out value))
            {
                ContentResult.Add("0");
                ContentResult.Add("Traffic must be integer value");
                goto Exitlabel;
            }
            var PhraseExist = cmnF.GetIntegerPrimaryKeyOnDataExist("SearchQueryMaster", "SearchQuery", PhraseDtl.PhraseName, "Id");
            if (string.IsNullOrEmpty(PhraseExist) != true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Search query already exist");
                goto Exitlabel;
            }
            ContentResult.Add("1");
            ContentResult.Add("1");
        Exitlabel:
            return ContentResult;
        }
        public ActionResult PhraseDownloadExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "SearchQuery" + ".xls";
            //Bind data list from edmx
            var getKeywords = objDBentity.prcPhraseOnDemand().ToList();

            if (getKeywords != null && getKeywords.Any())
            {
                sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
                sb.Append("<caption>Search Query List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>S.No</b></th>");
                sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Search Query</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Traffic</b></th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getKeywords)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + removdash(result.SearchQuery) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + removdash(result.Traffic.ToString()) + "</td>");


                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
            }

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        public ActionResult PhraseDownloadTemplateExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "PhraseTemplate" + ".xls";
            sb.Append("<table>");
            sb.Append("<thead><tr>");
            sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Search Query</b></th>");
            sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Traffic</b></th>");
            sb.Append("</tr></thead>");
            sb.Append("</table>");

            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        public ActionResult PhraseDownloadTemplateCSV()
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            //csv += "Search Query" + ',';
            csv += "Search Query" + ',';
            csv += "Traffic";
            //Add new line.
            csv += "\r\n";
            //Add the Data rows.
            //csv += "Online sports shoes in india".Replace(",", ";") + ',';
            csv += "Online sports shoes in india".Replace(",", ";") + ',';
            csv += "500".Replace(",", ";");
            //Add new line.
            //csv += "\r\n";

            string sFileName = "SearchQueryTemplate" + ".csv";
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }
        #endregion

        #region Accounts

        public ActionResult Accounts()
        {
            GetAccountDetails();
            return View();
        }
        public JsonResult getAccountsOnDemand()
        {

                var getAccounts = objDBentity.AccountsMasters.Where(x => x.IsActive == true).OrderBy(x => x.AccName).ToList();
                return Json(getAccounts, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CreateAccount(FormCollection fc, IEnumerable<HttpPostedFileBase> file)
        {
            accDtl = new Account();
            var fileName = "";
            var newPath = "";
            HttpPostedFileBase UploadedFile = file.FirstOrDefault();
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(fc["txtAccountName"].Trim()) == true)
            {
                ContentResult.Add("Account name is required.");//to show error calidation message
                ContentResult.Add("0");                      //to show response of stored procedure has not executed
                goto Exitlabel;
                //return Content("Batch Name is required.");

            }
            if (UploadedFile == null && fc["hdnAccountCode"].ToUpper() == "0")
            {
                ContentResult.Add("Please upload account image");//to show error calidation message
                ContentResult.Add("0");                           //to show response of stored procedure has not executed
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtAccountDesrpsn"].Trim()) == true)
            {
                ContentResult.Add("Account description is required.");//to show error calidation message
                ContentResult.Add("0");                      //to show response of stored procedure has not executed
                goto Exitlabel;
                //return Content("Batch Name is required.");

            }
            if (UploadedFile != null)
            {
                if (UploadedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                    string[] AllowedFileExtensions = new string[] { ".jpg", ".JPG", ".PNG", ".png", ".jpeg", ".JPEG" };
                    if (!AllowedFileExtensions.Contains
                       (UploadedFile.FileName.Substring(UploadedFile.FileName.LastIndexOf('.'))))
                    {
                        ContentResult.Add("Choose valid file types are: " + string.Join(", ", AllowedFileExtensions));//to show error calidation message
                        ContentResult.Add("0");                           //to show response of stored procedure has not executed
                        goto Exitlabel;

                    }
                    else if (UploadedFile.ContentLength > MaxContentLength)
                    {
                        ContentResult.Add("Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");//to show error calidation message
                        ContentResult.Add("0");                           //to show response of stored procedure has not executed
                        goto Exitlabel;
                    }
                    fileName = Path.GetFileName(UploadedFile.FileName);
                    newPath = cmnF.getCurrentDateTime().ToString("yyyyMMddHHmmss") + '_' + fileName;
                }
            }
            if (string.IsNullOrEmpty(fc["txtAccountDesrpsn"]) == true)
            {
                ContentResult.Add("Account description is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            accDtl.AccountName = fc["txtAccountName"].Trim();
            accDtl.ImagePath = "/Content/images/admin/" + newPath;
            accDtl.AccountDescription = fc["txtAccountDesrpsn"].Trim();
            var TempResult1 = cmnF.GetIntegerPrimaryKeyOnDataExist("AccountsMaster", "AccName", accDtl.AccountName, "AccCode");
            if (string.IsNullOrEmpty(TempResult1) != true)
            {
                var t1 = fc["hdnAccountCode"].ToUpper();
                if (fc["hdnAccountCode"].ToUpper() == TempResult1.ToUpper())
                {

                }
                else
                {
                    ContentResult.Add("This account is already exist.");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }
            }

            //var TempResult2 = cmnF.GetPrimaryKeyOnDataExist("LocationMaster", "LocationName", stdtDtl.Location, "LocationCode");
            //if (string.IsNullOrEmpty(TempResult2))
            //{
            //    stdtDtl.LocationCode = null;
            //}
            //else
            //{
            //    stdtDtl.LocationCode = TempResult2;
            //}
            var u = fc["hdnAccountCode"].ToUpper();

            //if hidden field- fc["hdnStudentCode"] value is zero means create code will work
            //if hidden field- fc["hdnStudentCode"] value is zero means Edit code will work
            if (fc["hdnAccountCode"].ToUpper() == "0")
            {
                var path = Path.Combine(Server.MapPath("~/Content/images/admin/"), newPath);
                UploadedFile.SaveAs(path);

                objDBentity.prcCreateAccount(accDtl.AccountName, accDtl.ImagePath, accDtl.AccountDescription, null, "INSERT");
                ContentResult.Add("0");//to show response of create message
                ContentResult.Add("1");//to show response of stored procedure working correctly
            }
            if (fc["hdnAccountCode"].ToUpper() != "0")
            {
                if (UploadedFile != null)
                {
                    var TempResult2 = cmnF.GetPrimaryKeyOnDataExist("AccountsMaster", "AccCode", fc["hdnAccountCode"], "ImagePath");
                    if (!string.IsNullOrEmpty(TempResult2))
                    {
                        if (System.IO.File.Exists(Server.MapPath("~" + TempResult2)))
                        {
                            System.IO.File.Delete(Server.MapPath("~" + TempResult2));
                        }
                    }
                    var path = Path.Combine(Server.MapPath("~/Content/images/admin/"), newPath);
                    UploadedFile.SaveAs(path);
                    objDBentity.prcCreateAccount(accDtl.AccountName, accDtl.ImagePath, accDtl.AccountDescription, fc["hdnAccountCode"], "UPDATE");
                }

                if (UploadedFile == null)
                {
                    objDBentity.prcCreateAccount(accDtl.AccountName, null, accDtl.AccountDescription, fc["hdnAccountCode"], "UPDATE");
                }
                ContentResult.Add("1");//to show response of edit message
                ContentResult.Add("1");//to show response of stored procedure working correctly
            }
        Exitlabel:
           
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleAccountDetails(string AccountCode)
        {
            //GetAccountDetails();
            if (AccountCode == null || AccountCode == "" || AccountCode == "undefined")
            {
                return null;
            }
            List<object> SingleAccountDetails = new List<object>();
            Int32 FAccountCode = Convert.ToInt32(AccountCode);
            var Detail1 = objDBentity.AccountsMasters.Where(x => x.AccCode == FAccountCode && x.IsActive == true).OrderBy(x => x.AccCode).FirstOrDefault();
            //var Detail1 = objDBentity.prcAccountOnDemand(null, null, AccountCode).ToArray();
            //var Detail1 = objDBentity.BatchMasters.Where(x => x.BatchCode == BatchCode);
            SingleAccountDetails.Add(Detail1);
            var vg = Json(SingleAccountDetails, JsonRequestBehavior.AllowGet);
            return Json(SingleAccountDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteSingleAccount(string AccountCode)
        {
            List<string> ContentResult = new List<string>();
            if (AccountCode == null || AccountCode == "" || AccountCode == "undefined")
            {
                return null;
            }
            string TmpAccountCode = string.IsNullOrEmpty(AccountCode) ? null : AccountCode;
            Int32 TmpIntegerAccountCode = Convert.ToInt32(TmpAccountCode);
            var TempResult2 = (from BASM in objDBentity.BatchAndStudentMappings
                               where BASM.AccCode == TmpIntegerAccountCode
                               select BASM.AccCode).ToList();
            if (TempResult2.Count > 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult2.Count <= 0)
            {
                var TempResult3 = cmnF.GetPrimaryKeyOnDataExist("AccountsMaster", "AccCode", AccountCode, "ImagePath");
                if (!string.IsNullOrEmpty(TempResult3))
                {
                    if (System.IO.File.Exists(Server.MapPath("~" + TempResult3)))
                    {
                        System.IO.File.Delete(Server.MapPath("~" + TempResult3));
                    }
                }
                objDBentity.prcCreateAccount(null, null, null, AccountCode, "DELETE");
                ContentResult.Add("1");
                ContentResult.Add("1");
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ChangeHistory
        public ActionResult ChangeHistory()
        {
            var result = objDBentity.getChangeHistoryOnDemand().ToArray();
            ViewBag.TotalRecord = result.Count();
            ViewBag.Result = result.ToList();
            GetAccountDetails();
            return View();
        }
        #endregion

        private ActionResult GetAccountDetails()
        {
            if (Session["LoginInfo"] == null)
            {
                return RedirectToRoute("default");
            }
            else
            {
                objUserInfo = (UserLoginInfo)Session["LoginInfo"];

                if (objUserInfo.UserType == 2)
                {
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
                else
                {
                    Session["LoginInfo"] = null;
                    return RedirectToRoute("default");
                }
            }
        }
        public string removdash(string value)
        {
            if (value.Trim() == "-")
            {
                value = "";
            }
            return value.Trim();
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                //Use reflection to get property names, to create table, Only first time, others  will follow

                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = "application/json; charset=utf-8",
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
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
                //randomPwd = "abc@123";
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

            return View("~/Views/Home/Opportunities.cshtml", "~/Areas/Admin/Views/Shared/Admin_Layout.cshtml");
        }
        public ActionResult Resources()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            return View("~/Views/Home/Resources.cshtml", "~/Areas/Admin/Views/Shared/Admin_Layout.cshtml");
        }
    }
}