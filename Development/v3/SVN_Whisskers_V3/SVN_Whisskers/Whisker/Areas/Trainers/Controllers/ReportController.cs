﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using System.Web.Mvc.Ajax;
using Whisker.App_Start;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;

namespace Whisker.Areas.Trainers.Controllers
{
    [SessionExpireFilterAttribute]
    [ExceptionHandler]
    public class ReportController : Controller
    {
        //
        // GET: /Trainers/Report/
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();
        Whisker.CommonClass.CommonFunc cmnF = new Whisker.CommonClass.CommonFunc();

        public ActionResult Index()
        {
            //ViewBag.Active = "#li_Reports";
            return View();
        }
        public ActionResult Account()
        {
            ViewBag.Active = "#li_Reports";
            GetAccountDetails();
            return View();
        }
        public ActionResult Industry()
        {
            
            ViewBag.Active = "#li_Reports";
            GetAccountDetails();
            return View();
        }
        public ActionResult NoBatch()
        {
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
                if (objUserInfo.UserType == 1)
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
        public JsonResult GetBatchDropdown()
        {
            GetAccountDetails();
            //var BatchCode = objDBentity.BatchAndTrainerMappings.Where(x => x.UserID == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault().ToString();

            List<object> AllAccountDetail = new List<object>();
            object AllBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();

            AllAccountDetail.Add(AllBatches);

            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountDropdown(string BatchCode)
        {
            GetAccountDetails();

            List<object> AllAccountDetail = new List<object>();
            object AllAccount = objDBentity.AccountsMasters.Where(x => x.IsActive == true).Select(x => new { x.AccCode, x.AccName }).ToList();

            if (BatchCode == null || BatchCode == "null" || BatchCode == "" || BatchCode == "undefined")
            {
                return null;
            }
            string TmpBatchCode = string.IsNullOrEmpty(BatchCode) ? null : BatchCode;
            Int32 TmpIntegerBatchCode = Convert.ToInt32(TmpBatchCode);
            var AllAccount1 = (from rr in objDBentity.RFA_Result
                               join am in objDBentity.AccountsMasters on rr.AccCode equals am.AccCode
                               where rr.BatchCode == TmpIntegerBatchCode && am.IsActive == true
                               select new
                               {
                                   rr.AccCode,
                                   am.AccName,
                               }).Distinct().ToList();


            AllAccountDetail.Add(AllAccount1);

            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBatchDetails(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object BatchDetails1 = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();
            object BatchDetails = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", BatchCode).FirstOrDefault();

            AllAccountDetail.Add(BatchDetails);
            //AllAccountDetail.Add(AdDetails);

            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountReport(string BatchCode, string AccCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            if (AccCode != null && AccCode != "" && AccCode != "undefined")
            {
                object AccountDetails = objDBentity.prcGetAccountRpt(BatchCode, AccCode).ToArray();
                AllAccountDetail.Add(AccountDetails);
                var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                object AccountDetails = objDBentity.prcGetAccountRpt(BatchCode, null).ToArray();
                AllAccountDetail.Add(AccountDetails);
                var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAccountCampReport(string BatchCode, string AccCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            string TmpAccCode = string.IsNullOrEmpty(AccCode) ? null : AccCode;
            object CampaignDetails = objDBentity.prcGetCampaign(BatchCode, TmpAccCode, null).ToArray();

            AllAccountDetail.Add(CampaignDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountAdGrpReport(string BatchCode, string CampCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            string TmpCampCode = string.IsNullOrEmpty(CampCode) ? null : CampCode;
            object AdGroupDetails = objDBentity.getADGroupsOnDemand(BatchCode, null, TmpCampCode, null).ToArray();

            AllAccountDetail.Add(AdGroupDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAccountKeywrdReport(string BatchCode, string AdgroupCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            string TmpAdgroupCode = string.IsNullOrEmpty(AdgroupCode) ? null : AdgroupCode;
            object KeywrdDetails = objDBentity.getMappedkeyWordsOnDemand(BatchCode, null, null, null, TmpAdgroupCode).ToArray();
            AllAccountDetail.Add(KeywrdDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AccountDownloadExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            //Decimal Budget = 0;
            //Decimal Imps = 0;
            //Decimal Clicks = 0;
            //Decimal CTR = 0;
            //Decimal Cost = 0;
            //Decimal CPC = 0;
            //Decimal cnver = 0;
            //Decimal CostCpc = 0;
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "AccountReport" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.prcGetAccountRpt(BatchCode, null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>All Account List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>Budget</th>");
                sb.Append("<th>Impressions</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>CTR (%)</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Avg CPC</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>Cost/Conversion</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AccName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Budget, 0) + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewImpression, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewClicks, 0) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CTR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCost, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewConversions, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2) + "</td>");
                    sb.Append("</tr>");

                    //Imps += cmnF.CnvrtRoundToDcimal(result.NewImpression, 0);
                    //Clicks += cmnF.CnvrtRoundToDcimal(result.NewClicks, 0);
                    ////CTR += cmnF.CnvrtRoundToDcimal(result.CTR, 2);
                    //Cost += cmnF.CnvrtRoundToDcimal(result.NewCost, 2);
                    //CPC += cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2);
                    //cnver += cmnF.CnvrtRoundToDcimal(result.NewConversions, 2);
                    //CostCpc += cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2);
                }
                //sb.Append("<tr>");
                //sb.Append("<td align='center' style='width:200px;height:50px;'>Total</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Budget, 0) + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Imps, 0) + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Clicks, 0) + "</td>");
                ////sb.Append("<td align='center' style='width:200px;height:50px;'>" + CTR + "</td>");
                //sb.Append("<td align='center' style='width:200px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Clicks, 0) / cmnF.CnvrtRoundToDcimal(Imps, 0)) * 100), 2) + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Cost, 2) + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(cnver, 0) + "</td>");
                ////                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CostCpc + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal((cmnF.CnvrtRoundToDcimal(Cost, 2) / cmnF.CnvrtRoundToDcimal(cnver, 0)), 2) + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("</tr>");
                //sb.Append("<tr>");
                //sb.Append("<td align='center' style='width:200px;height:50px;'>Avg</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal((cmnF.CnvrtRoundToDcimal(Cost, 2) / cmnF.CnvrtRoundToDcimal(Clicks, 2)), 2) + "</td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                //sb.Append("</tr>");
                sb.Append("</tbody></table>");
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        public ActionResult CampaignDownloadExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal Budget = 0;
            Decimal Imps = 0;
            Decimal RevNewImps = 0;
            Decimal Clicks = 0;
            Decimal CTR = 0;
            Decimal Cost = 0;
            Decimal CPC = 0;
            Decimal cnver = 0;
            Decimal CostCpc = 0;
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "CampaignReport" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.prcGetCampaign(BatchCode, null, null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>All Campaign List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account Name</th>");
                sb.Append("<th>Campaign Name</th>");
                sb.Append("<th>Budget</th>");
                sb.Append("<th>Impression</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>CTR (%)</th>");
                sb.Append("<th>Average Cost per click</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>CVR(%)</th>");
                sb.Append("<th>CPA (Cost per Conversion)</th>");
                sb.Append("<th>Average Postion</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Impression Share (%)</th>");
                sb.Append("<th>Impression Share Lost (%)</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.accName + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.CampaignName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Budget, 2) + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewImpression, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewClicks, 0) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CTR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewConversions, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CVR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.AvgPos, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCost, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionLost, 2) + "</td>");
                    sb.Append("</tr>");

                    RevNewImps += cmnF.CnvrtRoundToDcimal(result.NewImpression, 0);
                    Imps += cmnF.CnvrtRoundToDcimal(result.Impressions, 0);

                    Clicks += cmnF.CnvrtRoundToDcimal(result.NewClicks, 0);
                    //CTR += cmnF.CnvrtRoundToDcimal(result.CTR, 2);
                    Cost += cmnF.CnvrtRoundToDcimal(result.NewCost, 2);
                    CPC += cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2);
                    cnver += cmnF.CnvrtRoundToDcimal(result.NewConversions, 2);
                    CostCpc += cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2);
                    Budget += cmnF.CnvrtRoundToDcimal(result.Budget, 2);
                }
                Decimal TotalCTR = 0;
                Decimal ImpressionShare = 0;
                Decimal CVR = 0;
                Decimal AvgCPC = 0;
                Decimal CostPerConv = 0;
                if (cmnF.CnvrtRoundToDcimal(RevNewImps, 0) != 0)
                {
                    TotalCTR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Clicks, 0)) / (cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) * 100), 2);
                }
                else
                {
                    TotalCTR = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(Imps, 0) != 0)
                {
                    ImpressionShare = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) / (cmnF.CnvrtRoundToDcimal(Imps, 0))), 2);
                }
                else
                {
                    ImpressionShare = 0.00M;
                }

                var ImpressionLost = cmnF.CnvrtRoundToDcimal(ImpressionShare, 2) == 0 ? cmnF.CnvrtRoundToDcimal(0, 2) : cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                //var ImpressionLost = cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                if (cmnF.CnvrtRoundToDcimal(Clicks, 0) != 0)
                {
                    CVR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(cnver, 0)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                    AvgCPC = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Cost, 2)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                }
                else
                {
                    CVR = 0.00M;
                    AvgCPC = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(cnver, 0) != 0)
                {
                    CostPerConv = cmnF.CnvrtRoundToDcimal((cmnF.CnvrtRoundToDcimal(Cost, 2) / cmnF.CnvrtRoundToDcimal(cnver, 0)), 2);
                }
                else
                {
                    CostPerConv = 0.00M;
                }
                sb.Append("<tr>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>Total</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Budget, 2) + "</td>");

                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(RevNewImps, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Clicks, 0) + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>" + TotalCTR + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(cnver, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//7
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CostPerConv + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//8
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Cost, 2) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td align='center' style='width:200px;height:50px;'>Avg</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + AvgCPC + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CVR + "</td>");//8
                sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionShare + "</td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionLost + "</td>");//7
                sb.Append("</tr>");
                sb.Append("</tbody></table>");
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        public ActionResult AdgroupDownloadExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal Imps = 0;
            Decimal RevNewImps = 0;
            Decimal Clicks = 0;
            Decimal CTR = 0;
            Decimal Cost = 0;
            Decimal CPC = 0;
            Decimal cnver = 0;
            Decimal CostCpc = 0;
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "AdgroupReport" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.getADGroupsOnDemand(BatchCode, null, null, null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>All Ad group List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>Campaign</th>");
                sb.Append("<th>Ad Group</th>");
                sb.Append("<th>Budget</th>");

                sb.Append("<th>Impression</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>CTR (%)</th>");
                sb.Append("<th>Average Cost per click</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>CVR(%)</th>");
                sb.Append("<th>CPA (Cost per Conversion)</th>");
                sb.Append("<th>Average Postion</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Impression Share (%)</th>");
                sb.Append("<th>Impression Share Lost (%)</th>");

                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.accName + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.CampaignName + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AdGroupName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Budget, 0) + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewImpression, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewClicks, 0) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CTR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewConversions, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CVR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.AvgPos, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCost, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionLost, 2) + "</td>");
                    sb.Append("</tr>");

                    RevNewImps += cmnF.CnvrtRoundToDcimal(result.NewImpression, 0);
                    Imps += cmnF.CnvrtRoundToDcimal(result.Impressions, 0);
                    Clicks += cmnF.CnvrtRoundToDcimal(result.NewClicks, 0);
                    //CTR += cmnF.CnvrtRoundToDcimal(result.CTR, 2);
                    Cost += cmnF.CnvrtRoundToDcimal(result.NewCost, 2);
                    CPC += cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2);
                    cnver += cmnF.CnvrtRoundToDcimal(result.NewConversions, 2);
                    CostCpc += cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2);
                }
                Decimal TotalCTR = 0;
                Decimal ImpressionShare = 0;
                Decimal CVR = 0;
                Decimal AvgCPC = 0;
                Decimal CostPerConv = 0;
                if (cmnF.CnvrtRoundToDcimal(RevNewImps, 0) != 0)
                {
                    TotalCTR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Clicks, 0)) / (cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) * 100), 2);
                }
                else
                {
                    TotalCTR = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(Imps, 0) != 0)
                {
                    ImpressionShare = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) / (cmnF.CnvrtRoundToDcimal(Imps, 0))), 2);
                }
                else
                {
                    ImpressionShare = 0.00M;
                }

                var ImpressionLost = cmnF.CnvrtRoundToDcimal(ImpressionShare, 2) == 0 ? cmnF.CnvrtRoundToDcimal(0, 2) : cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                //var ImpressionLost = cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                if (cmnF.CnvrtRoundToDcimal(Clicks, 0) != 0)
                {
                    CVR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(cnver, 0)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                    AvgCPC = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Cost, 2)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                }
                else
                {
                    CVR = 0.00M;
                    AvgCPC = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(cnver, 0) != 0)
                {
                    CostPerConv = cmnF.CnvrtRoundToDcimal((cmnF.CnvrtRoundToDcimal(Cost, 2) / cmnF.CnvrtRoundToDcimal(cnver, 0)), 2);
                }
                else
                {
                    CostPerConv = 0.00M;
                }

                sb.Append("<tr>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>Total</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(RevNewImps, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Clicks, 0) + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>" + TotalCTR + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(cnver, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//7
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CostPerConv + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//8
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Cost, 2) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td align='center' style='width:200px;height:50px;'>Avg</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + AvgCPC + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CVR + "</td>");//8
                sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionShare + "</td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionLost + "</td>");//7

                sb.Append("</tr>");
                sb.Append("</tbody></table>");
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        public ActionResult KeywordDownloadExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal Imps = 0;
            Decimal RevNewImps = 0;
            Decimal Clicks = 0;
            Decimal CTR = 0;
            Decimal Cost = 0;
            Decimal CPC = 0;
            Decimal cnver = 0;
            Decimal CostCpc = 0;
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "KeywordReport" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.getMappedkeyWordsOnDemand(BatchCode, null, null, null, null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>All Keyword List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Keyword Name</th>");
                sb.Append("<th>Adgroup Name</th>");
                sb.Append("<th>Campaign Name</th>");
                sb.Append("<th>Impression</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>CTR (%)</th>");
                sb.Append("<th>Average Cost per click</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>CVR(%)</th>");
                sb.Append("<th>CPA (Cost per Conversion)</th>");
                sb.Append("<th>Average Postion</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Impression Share (%)</th>");
                sb.Append("<th>Impression Share Lost (%)</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.KeyName + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AdGroupName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.CampaignName + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewImpression, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewClicks, 0) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CTR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewConversions, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CVR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.AvgPos, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCost, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionLost, 2) + "</td>");

                    sb.Append("</tr>");

                    RevNewImps += cmnF.CnvrtRoundToDcimal(result.NewImpression, 0);
                    Imps += cmnF.CnvrtRoundToDcimal(result.Impressions, 0);
                    Clicks += cmnF.CnvrtRoundToDcimal(result.NewClicks, 0);
                    //CTR += cmnF.CnvrtRoundToDcimal(result.CTR, 2);
                    Cost += cmnF.CnvrtRoundToDcimal(result.NewCost, 2);
                    CPC += cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2);
                    cnver += cmnF.CnvrtRoundToDcimal(result.NewConversions, 2);
                    CostCpc += cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2);
                }
                Decimal TotalCTR = 0;
                Decimal ImpressionShare = 0;
                Decimal CVR = 0;
                Decimal AvgCPC = 0;
                Decimal CostPerConv = 0;
                if (cmnF.CnvrtRoundToDcimal(RevNewImps, 0) != 0)
                {
                    TotalCTR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Clicks, 0)) / (cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) * 100), 2);
                }
                else
                {
                    TotalCTR = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(Imps, 0) != 0)
                {
                    ImpressionShare = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) / (cmnF.CnvrtRoundToDcimal(Imps, 0))), 2);
                }
                else
                {
                    ImpressionShare = 0.00M;
                }

                var ImpressionLost = cmnF.CnvrtRoundToDcimal(ImpressionShare, 2) == 0 ? cmnF.CnvrtRoundToDcimal(0, 2) : cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                //var ImpressionLost = cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                if (cmnF.CnvrtRoundToDcimal(Clicks, 0) != 0)
                {
                    CVR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(cnver, 0)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                    AvgCPC = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Cost, 2)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                }
                else
                {
                    CVR = 0.00M;
                    AvgCPC = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(cnver, 0) != 0)
                {
                    CostPerConv = cmnF.CnvrtRoundToDcimal((cmnF.CnvrtRoundToDcimal(Cost, 2) / cmnF.CnvrtRoundToDcimal(cnver, 0)), 2);
                }
                else
                {
                    CostPerConv = 0.00M;
                }


                sb.Append("<tr>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>Total</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(RevNewImps, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Clicks, 0) + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>" + TotalCTR + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(cnver, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//7
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CostPerConv + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//8
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Cost, 2) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");


                sb.Append("<td align='center' style='width:200px;height:50px;'>Avg</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + AvgCPC + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CVR + "</td>");//8
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionShare + "</td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionLost + "</td>");//7
                sb.Append("</tr>");
                sb.Append("</tbody></table>");
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }

        public JsonResult GetIndustryReportTopSearch(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object keywordDetails = objDBentity.GetIndustryReport_TopKey(Convert.ToInt32(BatchCode)).ToArray();

            AllAccountDetail.Add(keywordDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportTopThemes(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object keywordDetails = objDBentity.GetTop5_Themes(Convert.ToInt32(BatchCode)).ToArray();

            AllAccountDetail.Add(keywordDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportCampaignMatrix(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object CampaignDetails = objDBentity.prcGetCampaign(BatchCode, null, null).ToArray();

            AllAccountDetail.Add(CampaignDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportTopAccount(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object AccountDetails = objDBentity.GetTopAccounts(Convert.ToInt32(BatchCode)).ToArray();
            AllAccountDetail.Add(AccountDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportRfaComparsnByAcc(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object AccountDetails = objDBentity.RFA_ComparisonsByAcc(Convert.ToInt32(BatchCode), null).ToArray();
            AllAccountDetail.Add(AccountDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportRfaComparsnByRfa(string BatchCode)
        {
            GetAccountDetails();
            List<object> AllAccountDetail = new List<object>();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            }
            object AccountDetails = objDBentity.RFA_ComparisonsByRFA(Convert.ToInt32(BatchCode), null).ToArray();
            AllAccountDetail.Add(AccountDetails);
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportRfaComparsnByGraph(string BatchCode, string AccCode,string RFAID)
        {
            GetAccountDetails();
            string TmpRFAID = string.IsNullOrEmpty(RFAID) ? null : RFAID;
            var AccountDetails = objDBentity.prcGetGraphs(BatchCode, AccCode, TmpRFAID).ToArray();
            return Json(AccountDetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIndustryReportRfaHistoriesByGraph(string BatchCode, string AccCode, string RFAID)
        {
            GetAccountDetails();
            var AccountDetails = objDBentity.prcGetRfaHistoriesByGraph(BatchCode, AccCode, RFAID).ToArray();
            return Json(AccountDetails, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIndustryReportTopSearchExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "Top10Keywords" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.GetIndustryReport_TopKey(Convert.ToInt32(BatchCode)).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>Top 10 Keyword List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>Keyword</th>");
                sb.Append("<th>Impression Share</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AccName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.KeyName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Cost, 0) + "</td>");
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
        public ActionResult GetIndustryReportTopThemesExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "Top5Themes" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.GetTop5_Themes(Convert.ToInt32(BatchCode)).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>Top 5 Theme List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>Campaign</th>");
                sb.Append("<th>Ad Group</th>");
                sb.Append("<th>Impression Share</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AccName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.CampaignName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.AdGroupName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Cost, 0) + "</td>");
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
        public ActionResult GetIndustryReportCampaignMatrixExcel(string id)
        {
            string BatchCode = id;
            Decimal Budget = 0;
            Decimal Imps = 0;
            Decimal RevNewImps = 0;
            Decimal Clicks = 0;
            Decimal CTR = 0;
            Decimal Cost = 0;
            Decimal CPC = 0;
            Decimal cnver = 0;
            Decimal CostCpc = 0;
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "CampaignMatrix" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.prcGetCampaign(BatchCode, null, null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>Campaign Matrix</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account Name</th>");
                sb.Append("<th>Campaign Name</th>");
                sb.Append("<th>Budget</th>");
                sb.Append("<th>Impression</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>CTR (%)</th>");
                sb.Append("<th>Average Cost per click</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>CVR(%)</th>");
                sb.Append("<th>CPA (Cost per Conversion)</th>");
                sb.Append("<th>Average Postion</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Impression Share (%)</th>");
                sb.Append("<th>Impression Share Lost (%)</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.accName + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.CampaignName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Budget, 2) + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewImpression, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewClicks, 0) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CTR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewConversions, 0) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.CVR, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.AvgPos, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.NewCost, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionLost, 2) + "</td>");
                    sb.Append("</tr>");

                    RevNewImps += cmnF.CnvrtRoundToDcimal(result.NewImpression, 0);
                    Imps += cmnF.CnvrtRoundToDcimal(result.Impressions, 0);

                    Clicks += cmnF.CnvrtRoundToDcimal(result.NewClicks, 0);
                    //CTR += cmnF.CnvrtRoundToDcimal(result.CTR, 2);
                    Cost += cmnF.CnvrtRoundToDcimal(result.NewCost, 2);
                    CPC += cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2);
                    cnver += cmnF.CnvrtRoundToDcimal(result.NewConversions, 2);
                    CostCpc += cmnF.CnvrtRoundToDcimal(result.NewCostPerConversions, 2);
                    Budget += cmnF.CnvrtRoundToDcimal(result.Budget, 2);
                }
                Decimal TotalCTR = 0;
                Decimal ImpressionShare = 0;
                Decimal CVR = 0;
                Decimal AvgCPC = 0;
                Decimal CostPerConv = 0;
                if (cmnF.CnvrtRoundToDcimal(RevNewImps, 0) != 0)
                {
                    TotalCTR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Clicks, 0)) / (cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) * 100), 2);
                }
                else
                {
                    TotalCTR = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(Imps, 0) != 0)
                {
                    ImpressionShare = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(RevNewImps, 0)) / (cmnF.CnvrtRoundToDcimal(Imps, 0))), 2);
                }
                else
                {
                    ImpressionShare = 0.00M;
                }

                var ImpressionLost = cmnF.CnvrtRoundToDcimal(ImpressionShare, 2) == 0 ? cmnF.CnvrtRoundToDcimal(0, 2) : cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                //var ImpressionLost = cmnF.CnvrtRoundToDcimal(100 - (cmnF.CnvrtRoundToDcimal(ImpressionShare, 2)), 2);
                if (cmnF.CnvrtRoundToDcimal(Clicks, 0) != 0)
                {
                    CVR = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(cnver, 0)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                    AvgCPC = cmnF.CnvrtRoundToDcimal(((cmnF.CnvrtRoundToDcimal(Cost, 2)) / (cmnF.CnvrtRoundToDcimal(Clicks, 0))), 2);
                }
                else
                {
                    CVR = 0.00M;
                    AvgCPC = 0.00M;
                }
                if (cmnF.CnvrtRoundToDcimal(cnver, 0) != 0)
                {
                    CostPerConv = cmnF.CnvrtRoundToDcimal((cmnF.CnvrtRoundToDcimal(Cost, 2) / cmnF.CnvrtRoundToDcimal(cnver, 0)), 2);
                }
                else
                {
                    CostPerConv = 0.00M;
                }
                sb.Append("<tr>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>Total</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Budget, 2) + "</td>");

                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(RevNewImps, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Clicks, 0) + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'>" + TotalCTR + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(cnver, 0) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//7
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CostPerConv + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");//8
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(Cost, 2) + "</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("<td align='center' style='width:200px;height:50px;'>Avg</td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + AvgCPC + "</td>");
                sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + CVR + "</td>");//8
                sb.Append("<td align='center' style='width:200px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'></td>");
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionShare + "</td>");//6
                sb.Append("<td align='center' style='width:50px;height:50px;'>" + ImpressionLost + "</td>");//7
                sb.Append("</tr>");
                sb.Append("</tbody></table>");
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");
        }
        public ActionResult GetIndustryReportTopAccountExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "TopAccount" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.GetTopAccounts(Convert.ToInt32(BatchCode)).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>Top Accounts</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>Impression Share (%)</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AccName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.Cost, 0) + "</td>");
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
        public ActionResult GetIndustryReportRfaComparsnByAccExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "RFAComparisionByAcc" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.RFA_ComparisonsByAcc(Convert.ToInt32(BatchCode), null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>RFA comparision by account</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                //sb.Append("<th>S No.</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>RFA</th>");
                sb.Append("<th>Impressions</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Cost/Conversion</th>");
                sb.Append("<th>Cost/Click</th>");
                sb.Append("<th>Impression Share</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                string oldAccCode = "";
                int rowsNo = 0;
                var rowSNo = 0;

                DataTable dt = cmnF.LINQToDataTable(getCampaign);
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");



                    if (oldAccCode != result.AccCode.ToString())
                    {
                        rowSNo = 0;
                        oldAccCode = result.AccCode.ToString();
                        //rowsName = 1;
                        DataRow[] filteredRows = dt.Select("AccCode = '" + oldAccCode + "'");

                        rowsNo = 0;

                        //foreach (DataRow childItem in filteredRows)
                        //{
                        //     rowsNo = rowsNo + 1;
                        //}
                        rowsNo = (int)filteredRows.Count();








                        //var result11 = Imgtobas64(Server.MapPath(result.ImagePath));
                        byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath(result.ImagePath));
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        var result11 = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64ImageRepresentation)));



                        sb.Append("<td rowspan='" + rowsNo + "'><img src='" + "data:image/png;base64," + result11 + "' width='85px' height='70px'  alt='amazone' /></a></td>");
                        //sb.Append("<td rowspan='" + rowsNo + "'><a id='" + result.AccCode + "'><div class='logo-img-scroll col-left'><img src='" + "http://localhost:49998"+ result.ImagePath + "' width='350px' height='200px'  alt='amazone' /></div> <div class='clearfix'></div></a></td>");
                        foreach (var result1 in getCampaign)
                        {
                            if (oldAccCode == result1.AccCode.ToString())
                            {
                                rowSNo = rowSNo + 1;
                            }
                        }
                    }

                    //htmlAcccontent += '<td>' + "RFA " + rowSNo + " (" + item.RunOn + ' )</td>' +




                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + "RFA "+ rowSNo + " (" + result.RunOn + " )</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>"+ result.RevNewImpression + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.RevNewClicks + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.RevNewConversions + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.RevActualCost + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerConversions, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.RevNewCostPerClick, 2) + "</td>");

                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("</tr>");
                    rowSNo = rowSNo - 1;
                }
                sb.Append("</tbody></table>");
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel/image/png";
            //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/ms-excel");
        }
        public ActionResult GetIndustryReportRfaComparsnByRFAExcel(string id)
        {
            string BatchCode = id;
            GetAccountDetails();
            Decimal CountCamp = 0;

            StringBuilder sb = new StringBuilder();
            string sFileName = "Top10Keywords" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.RFA_ComparisonsByAcc(Convert.ToInt32(BatchCode), null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>Top 10 Keyword List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>RFA</th>");
                sb.Append("<th>Account</th>");
                sb.Append("<th>Impressions</th>");
                sb.Append("<th>Clicks</th>");
                sb.Append("<th>Conversions</th>");
                sb.Append("<th>Cost</th>");
                sb.Append("<th>Cost/Conversion</th>");
                sb.Append("<th>Cost/Click</th>");
                sb.Append("<th>Impression Share</th>");
                sb.Append("</tr></thead><tbody>");
                int inc = 0;
                foreach (var result in getCampaign)
                {
                    CountCamp = getCampaign.Count;
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.AccName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.AccCode + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.ImpressionShare, 2) + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + cmnF.CnvrtRoundToDcimal(result.AccCode, 0) + "</td>");
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
        public string Imgtobas64(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
