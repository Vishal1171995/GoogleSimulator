using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using Whisker.Areas.Students.Models;
using Whisker.App_Start;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
using Whisker.CommonClass;
using System.Data.SqlClient;
using System.Configuration;

namespace Whisker.Areas.Students.Controllers
{
    [SessionExpireFilterAttribute]
    [ExceptionHandler]
    public class HomeController : Controller
    {
        //
        // GET: /Students/Home/
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();
        DataTable dtKeyword = null;
        Whisker.CommonClass.CommonFunc cmnF = new Whisker.CommonClass.CommonFunc();
        Whisker.Areas.Admin.Models.Keyword KeyDtl;

        public ActionResult Index()
        {

            Session["dtKeyword"] = null;
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            //if (Request.QueryString.HasKeys())
            //{
            //    if (Request.QueryString["Camp"] != null)
            //    {
            //        getCampaign(objUserInfo.UserID, Request.QueryString["Camp"]);
            //    }
            //}
            //else
            //{
            //    getCampaign(objUserInfo.UserID, null);
            //}
            return View();
        }
        //private void getCampaign(string userID, string cCode)
        //{
        //    // var unicornName = TempData["email"].ToString();// Request.QueryString["email"];
        //    // var unicornName = Request.QueryString["email"].ToString();
        //    var userCampaign = objDBentity.prcGetCampaign(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(),null);
        //    //* var userType = objDBentity.prcGetUserDetails("student@gmail.com");
        //    /* foreach (var item in userCampaign)
        //     {
        //        // item.CampaignName
        //        // item.Budget

        //     }*/
        //    ViewBag.uCampaign = userCampaign;

        //    //ViewBag.data = "khgij";
        //    /*ViewBag.FName = userInfo.FirstName;
        //    ViewBag.AccName = userInfo.AccName;
        //    ViewBag.ImagePath = userInfo.ImagePath;
        //    ViewBag.Description = userInfo.Description;
        //    ViewBag.AccFullName = userInfo.AccFullName;*/
        //}


        //private void getAccountDetails(string unicornName)
        //{
        //    //var unicornName = TempData["email"].ToString();// Request.QueryString["email"];
        //    //var unicornName = Request.QueryString["email"].ToString();
        //    var userInfo = objDBentity.prcGetUserDetails(unicornName).FirstOrDefault();
        //    /* var userType = objDBentity.prcGetUserDetails("student@gmail.com");
        //     foreach (var item in userType)
        //     { 
        //        item.
        //     }*/

        //    // ViewBag.data = "khgij";
        //    ViewBag.FName = userInfo.FirstName;
        //    ViewBag.AccCode = userInfo.AccCode;
        //    ViewBag.AccName = userInfo.AccName;
        //    ViewBag.ImagePath = userInfo.ImagePath;
        //    ViewBag.Description = userInfo.Description;
        //    ViewBag.AccFullName = userInfo.AccFullName;
        //    ViewBag.UserID = userInfo.UserID;

        //}

        //public JsonResult GetADGroupsData(string cCode)
        //{
        //    if (cCode == "ALL")
        //        cCode = null;

        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var userAdGroup = objDBentity.prcGetAdGroups(cCode, objUserInfo.UserID);

        //    return Json(userAdGroup, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetADsData(string cCode)
        //{
        //    if (cCode == "ALL")
        //        cCode = null;


        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var userAdGroup = objDBentity.prcGetCampaignAds(objUserInfo.UserID, cCode);

        //   // var userAewrddGroup = objDBentity.prcGetCampaignAds(objUserInfo.UserID, cCode).ToList();
        //    var ndfegjbje = Json(userAdGroup, JsonRequestBehavior.AllowGet);

        //    return Json(userAdGroup, JsonRequestBehavior.AllowGet);


        //}

        //public JsonResult GetKeywordsData(string cCode)
        //{
        //    if (cCode == "ALL")
        //        cCode = null;
        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var userAdGroup = objDBentity.GetMappedkeyWords(objUserInfo.UserID, cCode).ToList();
        //    return Json(userAdGroup, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetKeywords(string Keywordtxt)
        //{

        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var keywords = objDBentity.prcGetKeywords(Keywordtxt).ToList();
        //    return Json(keywords, JsonRequestBehavior.AllowGet);
        //}



        //public ActionResult GetKeywordsAllData(string CampaignCode, string AdGroupCode, string Keywordtxt)
        //{
        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var keywords = objDBentity.prcGetKeywords(Keywordtxt, 1).ToList();
        //    if (keywords.Count > 0)
        //    {
        //        var Temp = objDBentity.prcCheckKeywordFromMapKeywordsAG(keywords[0].KeyCode, CampaignCode, AdGroupCode, Convert.ToString(objUserInfo.UserID)).ToList();
        //        if (Temp.Count > 0)
        //        {
        //            return Json("Already exist", JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(keywords, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(keywords, JsonRequestBehavior.AllowGet);

        //    }
        //}
        //[HttpPost]
        //public ActionResult MapKeywordsAD(string empdata)
        //{
        //    var serializeData = JsonConvert.DeserializeObject<List<KeywordMapping>>(empdata);
        //    bool status = false;
        //    foreach (var data in serializeData)
        //    {
        //        if (data.keyBid == "N/A")
        //        {
        //            data.keyBid = "0";
        //        }
        //        objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //        objDBentity.prcMappedKeyword(data.KeyCode, data.CampaignCode, data.AdGroupCode, null, Convert.ToString(objUserInfo.UserID), 0);
        //        status = true;
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetKeywordsByCampId(string campcode, string adgroupcode)
        //{

        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var keywords = objDBentity.prcGetKeywordsbyCampCode(campcode, adgroupcode);

        //    return Json(keywords, JsonRequestBehavior.AllowGet);

        //}

        //public JsonResult DeleteKey(string cCode, string adgroupCode, string keyCode)
        //{
        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];

        //    var keywords = objDBentity.prcMappedKeyword(keyCode, cCode, adgroupCode, null, Convert.ToString(objUserInfo.UserID), 1);
        //    return Json(keywords, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult GetValidateemailvolunteer(string query)
        //{
        //    // string uid = "";

        //    string[] str = new string[4];
        //    str = query.Split(',');
        //    string url = WebApiURL + "PreRegistration/GetEmailValidate?query=" + str[0] + "&type=1";
        //    List<dynamic> lstuserInfo = (List<dynamic>)Newtonsoft.Json.JsonConvert.DeserializeObject((client.DownloadString(url)), typeof(List<dynamic>));

        //    //url = WebApiURL + "PreRegistration/GetValidateemailvol_prereg?query=" + str[0];
        //    //List<dynamic> lstuserInfo1 = (List<dynamic>)Newtonsoft.Json.JsonConvert.DeserializeObject((client.DownloadString(url)), typeof(List<dynamic>));

        //    List<string> lst = new List<string>();
        //    if (lstuserInfo.Count <= 0)
        //    {
        //        lst.Add(string.Format("failure"));
        //    }
        //    else
        //    {

        //        if (lstuserInfo.Count > 0)
        //        {
        //            if (lstuserInfo[0].email.Value != "")
        //                lst.Add(string.Format(lstuserInfo[0].email.Value));
        //        }

        //    }

        //    return Json(lst, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Edit Functionality
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult UpdateCompaign(FormCollection fc)
        //{
        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var CampaignName = fc["txtCampaignNameEdit"];
        //    var NetwordType = fc["ddlNetwordTypeEdit"];
        //    var Location = fc["txtLocationEdit"];
        //    var Budget = fc["txtBudgetEdit"];
        //    var CampaignCode = fc["hdnCampId"];

        //    objDBentity.prcUpdateCampaign(Convert.ToString(objUserInfo.UserID), CampaignCode, CampaignName, Location, Budget,NetwordType);
        //    //objDBentity.prcCreateCampaign(CampaignName, Convert.ToInt16(NetwordType), Location, Convert.ToDecimal(Budget), objUserInfo.AccCode, objUserInfo.UserID);

        //    return RedirectToAction("Index");
        //    // return JavaScript("window.location = 'Students/Home/Index'");
        //}

        //[HttpPost]
        //public ActionResult UpdateAdGroup(FormCollection fc)
        //{
        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var CampaignCode = fc["ddlCampaignADGMEdit"];
        //    var ADGroupName = fc["txtADGroupNameEdit"];
        //    var ADGroupText = fc["txtADGroupTextEdit"];
        //    var ADGroupHeadline = fc["txtADGroupHeadlineEdit"];
        //    var ADGroupDesc1 = fc["txtADGroupDesc1Edit"];
        //    var ADGroupDesc2 = fc["txtADGroupDesc2Edit"];
        //    var ADGroupDespUrl = fc["txtADGroupDespUrlEdit"];
        //    var ADGroupFinalUrl = fc["txtADGroupFinalUrlEdit"];
        //    var AdGroupId = fc["hdnAdGroupId"];

        //    objDBentity.prcUpdateAdGroup(Convert.ToString(objUserInfo.UserID), CampaignCode, AdGroupId, ADGroupName, ADGroupText, ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl);

        //    return new RedirectResult(Url.Action("Index") + "#adgroups");
        //    // return JavaScript("window.location = 'Students/Home/Index'");
        //}


        //[HttpPost]
        //public ActionResult UpdateAD(FormCollection fc)
        //{
        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    var CampaignCode = fc["ddlCampaignADEdit"];
        //    var ParentADGroup = fc["ddlADName"];
        //    var ADGroupText = fc["txtADTextEdit"];
        //    var ADGroupHeadline = fc["txtADHeadlineEdit"];
        //    var ADGroupDesc1 = fc["txtADDesc1Edit"];
        //    var ADGroupDesc2 = fc["txtADDesc2Edit"];
        //    var ADGroupDespUrl = fc["txtADDespUrlEdit"];
        //    var ADGroupFinalUrl = fc["txtADFinalUrlEdit"];
        //    var AdGroupId = fc["hdnAdGrpId"];

        //    //objDBentity.prcUpdateAdGroup(Convert.ToString(objUserInfo.UserID), CampaignCode, AdGroupId, ADGroupName, ADGroupText, ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl);
        //    objDBentity.prcUpdateAds(Convert.ToString(objUserInfo.UserID), CampaignCode, ParentADGroup, "", ADGroupText, ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl, AdGroupId);
        //    return new RedirectResult(Url.Action("Index") + "#ads");
        //}

        #region Campaign
        //For select campaigns
        public JsonResult getCampaignsOnDemand(string CampaignCode)
        {
            if (CampaignCode == "ALL")
                CampaignCode = null;
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userCampaign = objDBentity.prcGetCampaign(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), CampaignCode).ToList();
            return Json(userCampaign, JsonRequestBehavior.AllowGet);
        }
        //For create campaigns
        [HttpPost]
        public ActionResult CreateCompaign(FormCollection fc)
        {
            List<string> ContentResult = new List<string>();
            var CampaignStatus = true;
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var CampaignName = fc["txtCampaignName"].Trim();
            var NetwordType = fc["ddlNetwordType"];
            var Location = fc["txtLocation"].Trim();
            var Budget = fc["txtBudget"];
            var CampaignCode = fc["hdnCampId"];
            var TempCampaignStatus = fc["someSwitchOption001"];
            Int32 IntBatchCode = Convert.ToInt32(objUserInfo.BatchCode);
            var TempResult1 = from CM in objDBentity.CampaignMasters
                              where CM.isActive == true
                              && CM.BatchCode == IntBatchCode && CM.AccCode == objUserInfo.AccCode
                              && CM.CampaignName == CampaignName
                              orderby CM.CampaignName ascending
                              select new
                              {
                                  CampaignCode = CM.CampaignCode
                              };
            if (string.IsNullOrEmpty(fc["txtCampaignName"].Trim()) == true)
            {
                ContentResult.Add("Campaign is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtLocation"].Trim()) == true)
            {
                ContentResult.Add("Location is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtBudget"].Trim()) == true)
            {
                ContentResult.Add("Budget is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult1.ToList().Count > 0 && TempResult1.ToList()[0].CampaignCode != Convert.ToInt32(CampaignCode))
            {
                ContentResult.Add("Campaign name already exists.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtBudget"].Trim()) == true)
            {
                ContentResult.Add("Budget is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            int value = 0;
            if (!int.TryParse(fc["txtBudget"].Trim(), out value))
            {
                ContentResult.Add("Budget should be a numeric value.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempCampaignStatus == "on")
            {
                CampaignStatus = true;
            }
            else
            {
                CampaignStatus = false;
            }
            if (CampaignCode == "0")
            {
                objDBentity.prcCreateCampaign(CampaignName, Convert.ToInt32(CampaignCode), Convert.ToInt16(NetwordType), Location, Convert.ToDecimal(Budget), objUserInfo.AccCode, objUserInfo.UserID, Convert.ToString(objUserInfo.BatchCode), CampaignStatus);
                objDBentity.prcChangeHistory("CreateCompaign","Campaign is created", "Campaign Name is: "+CampaignName, objUserInfo.UserID);
                ContentResult.Add("0");
                ContentResult.Add("1");
                goto Exitlabel;
            }
            if (CampaignCode != "0")
            {
                objDBentity.prcCreateCampaign(CampaignName, Convert.ToInt32(CampaignCode), Convert.ToInt16(NetwordType), Location, Convert.ToDecimal(Budget), objUserInfo.AccCode, objUserInfo.UserID, Convert.ToString(objUserInfo.BatchCode), CampaignStatus);

                if (CampaignStatus == true)
                {
                    objDBentity.prcChangeHistory("CreateCompaign", "Campaign is updated", "Campaign Name is: " + CampaignName, objUserInfo.UserID);
                }
                if (CampaignStatus == false)
                {
                    objDBentity.prcChangeHistory("CreateCompaign", "Campaign is deleted", "Campaign Name is: " + CampaignName, objUserInfo.UserID);
                }
                ContentResult.Add("1");
                ContentResult.Add("1");
                goto Exitlabel;
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        //For Edit campaigns
        public JsonResult GetCampaignData(string cCode)
        {
            if (cCode == "ALL")
                cCode = null;

            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userCampaign = objDBentity.prcGetCampaign(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), cCode).ToList();
            var userCampaig1n = objDBentity.prcGetCampaign(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), cCode).ToList();
            return Json(userCampaign, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CampaignDownloadExcel()
        {
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
            string sFileName = "Campaign" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.prcGetCampaign(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), null).ToList();
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
        #endregion

        #region AdGroup
        //For select AdGroups
        public JsonResult getADGroupsOnDemand(string CampaignCode, string AdGroupCode)
        {
            if (CampaignCode == "ALL")
            {
                CampaignCode = null;
            }
            if (AdGroupCode == "ALL")
            {
                AdGroupCode = null;
            }
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userAdGroup = objDBentity.getADGroupsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), CampaignCode, AdGroupCode);
            var TempuserAdGroup = objDBentity.getADGroupsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), CampaignCode, AdGroupCode).ToList();

            return Json(userAdGroup, JsonRequestBehavior.AllowGet);
        }
        //For create AdGroups
        [HttpPost]
        public ActionResult CreateADGroup(FormCollection fc)
        {
            List<string> ContentResult = new List<string>();
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var CampaignCode = fc["ddlCampaignADGM"];
            var ADGroupName = fc["txtADGroupName"].Trim();
            var ADGroupHeadline = fc["txtADGroupHeadline"];
            var ADGroupDesc1 = fc["txtADGroupDesc1"];
            var ADGroupDesc2 = fc["txtADGroupDesc2"];
            var ADGroupDespUrl = fc["txtADGroupDespUrl"];
            var BHJB = fc["txtADGroupDesc1"];
            var ADGroupFinalUrl = fc["txtADGroupFinalUrl"];
            var AdGroupId = fc["hdnAdGroupId"];
            Int32 IntBatchCode = Convert.ToInt32(objUserInfo.BatchCode);
            Int32 IntCampaignCode = Convert.ToInt32(CampaignCode);

            var TempResult1 = from CM in objDBentity.CampaignMasters
                              join ADG in objDBentity.AdGroups
                                 on CM.CampaignCode equals ADG.CampaignCode into Info
                              from temp in Info.DefaultIfEmpty()
                              where CM.isActive == true
                              && CM.BatchCode == IntBatchCode && CM.AccCode == objUserInfo.AccCode
                              && CM.CampaignCode == IntCampaignCode && temp.AdGroupName == ADGroupName
                              orderby CM.CampaignName ascending
                              select new
                              {
                                  AdGroupCode = temp.AdGroupCode
                              };
            if (string.IsNullOrEmpty(fc["txtADGroupName"].Trim()) == true)
            {
                ContentResult.Add("Ad group is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADGroupHeadline"].Trim()) == true)
            {
                ContentResult.Add("Headline is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADGroupDesc1"].Trim()) == true)
            {
                ContentResult.Add("Description line 1 is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADGroupDesc2"].Trim()) == true)
            {
                ContentResult.Add("Description line 2 is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADGroupDespUrl"].Trim()) == true)
            {
                ContentResult.Add("Display url is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADGroupFinalUrl"].Trim()) == true)
            {
                ContentResult.Add("Final url is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (TempResult1.ToList().Count > 0 && TempResult1.ToList()[0].AdGroupCode != Convert.ToInt32(AdGroupId))
            {
                ContentResult.Add("Ad Group name already exists.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (AdGroupId == "0")
            {
                objDBentity.prcCreateADGroup(CampaignCode, ADGroupName, Convert.ToString(objUserInfo.AccCode), ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl, objUserInfo.UserID, AdGroupId);
                objDBentity.prcChangeHistory("CreateADGroup", "ADGroup is created", "ADGroup is: " + ADGroupName, objUserInfo.UserID);
                ContentResult.Add("0");
                ContentResult.Add("1");
                goto Exitlabel;
            }
            if (AdGroupId != "0")
            {
                objDBentity.prcCreateADGroup(CampaignCode, ADGroupName, Convert.ToString(objUserInfo.AccCode), ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl, objUserInfo.UserID, AdGroupId);
                objDBentity.prcChangeHistory("CreateADGroup", "ADGroup is updated", "ADGroup is: " + ADGroupName, objUserInfo.UserID);
                ContentResult.Add("1");
                ContentResult.Add("1");
                goto Exitlabel;
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        //For edit AdGroups
        public JsonResult GetADGroupsDataByID(string cCode, string AdgroupCode)
        {
            if (cCode == "ALL")
                cCode = null;

            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            //var userAdGroup = objDBentity.prcGetAdGroups(cCode, objUserInfo.UserID);
            var userAdGroup = objDBentity.prcGetAdGroupsByID(cCode, AdgroupCode).ToList();
            return Json(userAdGroup, JsonRequestBehavior.AllowGet);


        }
        public ActionResult AdgroupDownloadExcel()
        {
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
            string sFileName = "AdGroup" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.getADGroupsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), null, null).ToList();
            if (getCampaign != null && getCampaign.Any())
            {
                var HeaderCSS = ".download-tbl{width: 100 %; border: 0px;}" +
                    ".download-tbl th{background:#e4e4e4;color:#000; padding: 6px 12px;text-align:center; border:0px;}" +
                    ".download-tbl td{ border:0px;padding: 6px 12px;}" +
                    ".download-tbl tr:nth-child(even){background:#f0f1f1;color:#000;}";
                sb.Append("<head><style>" + HeaderCSS + "</style></head>");
                sb.Append("<table border='1' class='download-tbl' style='border:0px; width:100%;'>");
                sb.Append("<caption>All Adgroup List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th>S No.</th>");
                sb.Append("<th>Campaigns</th>");
                sb.Append("<th>Adgroup</th>");
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

                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.CampaignName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.AdGroupName + "</td>");

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
        #endregion

        #region Ads
        //For select Ads
        public JsonResult getCampaignAdsOnDemand(string CampaignCode, string AdGroupCode, string AdCode)
        {
            if (CampaignCode == "ALL")
            {
                CampaignCode = null;
            }
            if (AdGroupCode == "ALL")
            {
                AdGroupCode = null;
            }
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userAdGroup = objDBentity.getCampaignAdsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), objUserInfo.UserID, CampaignCode, AdGroupCode, AdCode);
            var tempuserAdGroup = objDBentity.getCampaignAdsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), objUserInfo.UserID, CampaignCode, AdGroupCode, AdCode).ToList();
            return Json(userAdGroup, JsonRequestBehavior.AllowGet);
        }
        //For create Ads
        [HttpPost]
        public ActionResult CreateAD(FormCollection fc)
        {
            List<string> ContentResult = new List<string>();
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var CampaignCode = fc["ddlCampaignAD"];
            var ADGroupCode = fc["ddlADGroupName"];
            var ADGroupName = fc["hidADGName"];
            var ADCode = fc["hdnAdId"];// hidadCode
            var ADGroupHeadline = fc["txtADHeadline"];
            var ADGroupDesc1 = fc["txtADDesc1"];
            var ADGroupDesc2 = fc["txtADDesc2"];
            var ADGroupDespUrl = fc["txtADDespUrl"];
            var ADGroupFinalUrl = fc["txtADFinalUrl"];

            var AdGroupId = fc["hdnAdGrpId"];
            if (string.IsNullOrEmpty(fc["txtADHeadline"].Trim()) == true)
            {
                ContentResult.Add("Headline is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADDesc1"].Trim()) == true)
            {
                ContentResult.Add("Description line 1 is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADDesc2"].Trim()) == true)
            {
                ContentResult.Add("Description line 2 is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADDespUrl"].Trim()) == true)
            {
                ContentResult.Add("Display url is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(fc["txtADFinalUrl"].Trim()) == true)
            {
                ContentResult.Add("Final url is required.");
                ContentResult.Add("0");
                goto Exitlabel;
            }
            if (ADCode == "0")
            {
                objDBentity.prcCreateAD(ADGroupCode, ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl, objUserInfo.UserID, ADCode);
                objDBentity.prcChangeHistory("CreateAD", "AD is created", "AD is: " + ADGroupHeadline, objUserInfo.UserID);
                ContentResult.Add("0");
                ContentResult.Add("1");
                goto Exitlabel;
            }
            else
            {
                objDBentity.prcCreateAD(ADGroupCode, ADGroupHeadline, ADGroupDesc1, ADGroupDesc2, ADGroupDespUrl, ADGroupFinalUrl, objUserInfo.UserID, ADCode);
                objDBentity.prcChangeHistory("CreateAD", "AD is updated", "AD is: " + ADGroupHeadline, objUserInfo.UserID);
                ContentResult.Add("1");
                ContentResult.Add("1");
                goto Exitlabel;
            }
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Keyword
        //For autocomplete extender
        public JsonResult ClearKeywordSession()
        {
            Session["dtKeyword"] = null;
            return Json("1", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKeywords(string Keywordtxt)
        {

            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var keywords = objDBentity.prcGetKeywords(Keywordtxt, 0).ToList();

            List<string> lst = new List<string>();

            if (keywords.Count <= 0)
            {
                //lst.Add(string.Format("failure"));
            }
            else
            {
                if (keywords.Count > 0)
                {

                    for (int i = 0; i < keywords.Count(); i++)
                    {
                        if (keywords[i].KeyName != "")
                            lst.Add(string.Format(keywords[i].KeyName));
                        //lst.Add(Convert.ToString(keywords[i].SuggestedBid));
                    }
                }
            }

            //lst2.Add(lst);

            return Json(lst, JsonRequestBehavior.AllowGet);


        }

        /// <summary>
        /// Check existance of keyword in datable as well as in mapping table of database.
        /// </summary>
        /// <param name="CampaignCode"></param>
        /// <param name="AdGroupCode"></param>
        /// <param name="Keywordtxt"></param>
        /// <param name="alldata"></param>
        /// <returns></returns>
        public ActionResult CheckKeywordsToKeywordDataTable(string CampaignCode, string AdGroupCode, string Keywordtxt, string alldata)
        {
            Keywordtxt = Regex.Replace(Keywordtxt, @"\s+", " ").Trim();
            if (Session["dtKeyword"] == null)
            {
                CreateKeywordDatatable();
            }
            else
            {
                dtKeyword = (DataTable)Session["dtKeyword"];
            }
            UpdateBidValue(alldata);
            var rowcount = dtKeyword.Rows.Count;
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var keywords = objDBentity.prcGetKeywords(Keywordtxt, 1).ToList();
            var TempKeywordName = "";
            var TempKeywordCode = "0";

            if (dtKeyword.Rows.Count > 0)
            {
                if (keywords.Count > 0)
                {
                    TempKeywordCode = Convert.ToString(keywords[0].KeyCode);
                    var temp1 = isKeywordExistInDT(TempKeywordName, TempKeywordCode, Keywordtxt);
                    if (!temp1)
                    {
                        return Json("Already exist", JsonRequestBehavior.AllowGet);
                    }
                }
                if (keywords.Count <= 0)
                {
                    TempKeywordName = Keywordtxt;
                    var temp1 = isKeywordExistInDT(TempKeywordName, TempKeywordCode, Keywordtxt);
                    if (!temp1)
                    {
                        return Json("Already exist", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            if (keywords.Count > 0)
            {
                objUserInfo = (UserLoginInfo)Session["LoginInfo"];
                var Temp = objDBentity.prcCheckKeywordFromMapKeywordsAG(keywords[0].KeyCode, AdGroupCode).ToList();
                if (Temp.Count > 0)
                {
                    //foreach (DataRow row in dtKeyword.Rows)
                    //{
                    //    if (row["KeywordCode"].ToString() == keywords[0].KeyCode && row["KeywordStatus"].ToString() == "DELETE")
                    //    {
                    //        dtKeyword.Rows.Add(rowcount + 1, keywords[0].KeyCode, Keywordtxt, 0, "INSERT", "1");
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        return Json("Already exist", JsonRequestBehavior.AllowGet);
                    //    }
                    //}
                    return Json("Already exist", JsonRequestBehavior.AllowGet);
                    /*Important Note -- Uncomment Code to show functionality on edit and comment line just above one*/
                }
                else
                {
                    dtKeyword.Rows.Add(rowcount + 1, keywords[0].KeyCode, Keywordtxt, keywords[0].SuggestedBid, keywords[0].traffic, "INSERT", "1");
                    // return Json(keywords, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                dtKeyword.Rows.Add(rowcount + 1, "0", Keywordtxt, 0, 0, "INSERT", "0");
                //return Json(keywords, JsonRequestBehavior.AllowGet);
            }
            Session["dtKeyword"] = dtKeyword;
            string a = JsonConvert.SerializeObject(dtKeyword);
            return Json(a, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getMappedkeyWordsOnDemand(string CampaignCode, string AdGroupCode)
        {
            if (CampaignCode == "ALL")
            {
                CampaignCode = null;
            }
            if (AdGroupCode == "ALL")
            {
                AdGroupCode = null;
            }
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userAdGroup = objDBentity.getMappedkeyWordsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), objUserInfo.UserID, CampaignCode, AdGroupCode);
            return Json(userAdGroup, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// this function is used to create data table.
        /// </summary>
        public void CreateKeywordDatatable()
        {
            if (dtKeyword == null)
            {
                dtKeyword = new DataTable();
                dtKeyword.Clear();
                DataColumn dc = dtKeyword.Columns.Add("KeywordRowId", typeof(int));
                dc.AutoIncrement = true;
                dc.AutoIncrementSeed = 1;
                dc.AutoIncrementStep = 1;
                dtKeyword.Columns.Add("KeywordCode", typeof(String));
                dtKeyword.Columns.Add("KeywordName", typeof(String));
                dtKeyword.Columns.Add("KeywordBid", typeof(float));
                dtKeyword.Columns.Add("Traffic", typeof(Int32));
                dtKeyword.Columns.Add("KeywordStatus", typeof(String));
                dtKeyword.Columns.Add("KeywordFromMaster", typeof(String));
            }
        }

        /// <summary>
        /// Check keyword exist in data table or not
        /// </summary>
        /// <param name="TempKeywordName"></param>
        /// <param name="TempKeywordCode"></param>
        /// <param name="userKeywordText"></param>
        /// <returns></returns>
        public bool isKeywordExistInDT(string TempKeywordName, string TempKeywordCode, string userKeywordText)
        {
            var temp = "";
            var status = true;
            foreach (DataRow row in dtKeyword.Rows)
            {
                if (TempKeywordName == "" && TempKeywordCode != "0" && userKeywordText != "")
                {
                    temp = row["KeywordCode"].ToString();
                    if (temp == TempKeywordCode && row["KeywordStatus"].ToString() != "DELETE")
                    {
                        status = false;
                        break;
                    }
                }
                if (TempKeywordName != "" && TempKeywordCode == "0" && userKeywordText != "")
                {
                    temp = row["KeywordName"].ToString();
                    if ((temp.Trim().ToUpper() == Regex.Replace(TempKeywordName, @"\s+", " ").Trim().ToUpper()) && row["KeywordStatus"].ToString() != "DELETE")
                    {
                        status = false;
                        break;
                    }
                }
            }
            return status;
        }

        /// <summary>
        /// this function is used to delete row from data table.
        /// </summary>
        /// <param name="KeywordRowId"></param>
        /// <param name="alldata"></param>
        /// <returns></returns>
        public ActionResult deleteKeywordRow(string KeywordRowId, string alldata)
        {
            UpdateBidValue(alldata);
            if (Session["dtKeyword"] != null)
            {
                dtKeyword = (DataTable)Session["dtKeyword"];
                DataRow dr = dtKeyword.Select("KeywordRowId=' " + KeywordRowId + " ' ").FirstOrDefault();
                if (dr != null)
                {
                    dr["KeywordStatus"] = "DELETE";
                }
                dtKeyword.AcceptChanges();
                Session["dtKeyword"] = dtKeyword;
                string JsonResult = JsonConvert.SerializeObject(dtKeyword);
                return Json(JsonResult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// this function is used to update the bid value.
        /// </summary>
        /// <param name="alldata"></param>
        public void UpdateBidValue(string alldata)
        {
            var serializeData = JsonConvert.DeserializeObject<List<KeywordMapping>>(alldata);
            if (Session["dtKeyword"] != null && serializeData.Count > 0)
            {
                dtKeyword = (DataTable)Session["dtKeyword"];
                for (int i = 0; i < serializeData.Count; i++)
                {
                    DataRow dr = dtKeyword.Select("KeywordRowId=' " + serializeData[i].RowId + " ' ").FirstOrDefault();
                    float BidValueInteger = 0;
                    if (float.TryParse(serializeData[i].BidValue, out BidValueInteger) && dr != null)
                    {
                        dr["KeywordBid"] = serializeData[i].BidValue;
                    }
                    else
                    {
                        dr["KeywordBid"] = "0";
                    }
                }
                dtKeyword.AcceptChanges();
                Session["dtKeyword"] = dtKeyword;
            }
        }
        [HttpPost]
        public ActionResult SaveMapKeywords(string CampaignCode, string AdGroupCode, string alldata, string hdnKeywordFlag)
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            string InCorrectRows = "";
            UpdateBidValue(alldata);
            List<string> ContentResult = new List<string>();
            if (Session["dtKeyword"] != null)
            {
                dtKeyword = (DataTable)Session["dtKeyword"];
                DataRow[] resultKeywrd = dtKeyword.Select("KeywordBid = '0' and KeywordStatus <> 'DELETE'");
                foreach (DataRow row in resultKeywrd)
                {
                    InCorrectRows += row["KeywordRowId"] + ",";
                }
                if (!string.IsNullOrEmpty(InCorrectRows))
                {
                    ContentResult.Add("FALSE");
                    //ContentResult.Add(InCorrectRows.TrimEnd(','));
                    ContentResult.Add("Invalid bid value.");
                    goto Exitlabel;
                }
            }
            if (hdnKeywordFlag == "CREATE")
            {
                ContentResult.Add("TRUE");
                ContentResult.Add("CREATE");
                foreach (DataRow row in dtKeyword.Rows)
                {
                    if (row["KeywordStatus"].ToString() != "DELETE")
                    {
                        var KeywordName = row["KeywordName"].ToString();
                        var KeywordCode = row["KeywordCode"].ToString();
                        var KeywordBid = row["KeywordBid"].ToString();
                        var KeywordFromMaster = row["KeywordFromMaster"].ToString();
                        objDBentity.prcCreateMappedKeywords(KeywordName, KeywordCode, AdGroupCode, KeywordBid, Convert.ToString(objUserInfo.UserID), Convert.ToInt32(KeywordFromMaster));
                        objDBentity.prcChangeHistory("SaveMapKeywords", "Keyword is created", "KeywordName is: "+ KeywordName, objUserInfo.UserID);
                    }
                }

            }
            if (hdnKeywordFlag == "EDIT")
            {
                foreach (DataRow row in dtKeyword.Rows)
                {
                    if (row["KeywordStatus"].ToString() == "DELETE")
                    {
                        var KeywordName = row["KeywordName"].ToString();
                        var KeywordCode = row["KeywordCode"].ToString();
                        var KeywordBid = row["KeywordBid"].ToString();
                        var KeywordFromMaster = row["KeywordFromMaster"].ToString();
                        var KeywordStatus = row["KeywordStatus"].ToString();
                        objDBentity.prcUpdateMappedKeywords(KeywordName, KeywordCode, AdGroupCode, KeywordBid, Convert.ToString(objUserInfo.UserID), Convert.ToInt32(KeywordFromMaster), KeywordStatus);
                        objDBentity.prcChangeHistory("SaveMapKeywords", "Keyword is deleted", "Keyword Name is: " + KeywordName, objUserInfo.UserID);
                    }
                }
                foreach (DataRow row in dtKeyword.Rows)
                {
                    if (row["KeywordStatus"].ToString() != "DELETE")
                    {
                        var KeywordName = row["KeywordName"].ToString();
                        var KeywordCode = row["KeywordCode"].ToString();
                        var KeywordBid = row["KeywordBid"].ToString();
                        var KeywordFromMaster = row["KeywordFromMaster"].ToString();
                        var KeywordStatus = row["KeywordStatus"].ToString();
                        objDBentity.prcUpdateMappedKeywords(KeywordName, KeywordCode, AdGroupCode, KeywordBid, Convert.ToString(objUserInfo.UserID), Convert.ToInt32(KeywordFromMaster), KeywordStatus);
                        objDBentity.prcChangeHistory("SaveMapKeywords", "Keyword is updated", "Keyword Name is: " + KeywordName, objUserInfo.UserID);
                    }
                }
            }
            ContentResult.Add("TRUE");
            ContentResult.Add("EDIT");
            ClearKeywordSession();
        Exitlabel:
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSingleKeywordDetails(string CampaignCode, string AdGroupCode)
        {
            ClearKeywordSession();
            CreateKeywordDatatable();
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var rowcount = dtKeyword.Rows.Count;
            var keywords = objDBentity.getSingleKeywordDetails(AdGroupCode).ToList();
            for (int i = 0; i < keywords.Count; i++)
            {
                if (keywords[i].KeywordFromMaster == true)
                {
                    dtKeyword.Rows.Add((i + 1), keywords[i].KeywordCode, keywords[i].KeywordName, keywords[i].KeywordBid, keywords[i].traffic, "UPDATE", "1");
                }
                if (keywords[i].KeywordFromMaster == false)
                {
                    dtKeyword.Rows.Add((i + 1), keywords[i].KeywordCode, keywords[i].KeywordName, keywords[i].KeywordBid, keywords[i].traffic, "UPDATE", "0");
                }
            }
            dtKeyword.AcceptChanges();
            Session["dtKeyword"] = dtKeyword;
            string JsonResult = JsonConvert.SerializeObject(dtKeyword);
            return Json(JsonResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KeywordDownloadExcel()
        {
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
            string sFileName = "Keyword" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getCampaign = objDBentity.getMappedkeyWordsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), null, null, null).ToList();
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
        #endregion

        #region KeywordPlanner
        public ActionResult Tools()
        {
            GetAccountDetails();
            return View();
        }


        public ActionResult DownloadKeyword()
        {
            GetAccountDetails();
            return View();
        }
        //public ActionResult KeyPlanDownloadTemplateExcel()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string sFileName = "KeywordPlannerDownloadTemplate" + ".xls";

        //    sb.Append("<table>");
        //    sb.Append("<thead><tr>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Keyword Name</b></th>");
        //    sb.Append("</tr></thead>");
        //    sb.Append("</table>");

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        //    return File(buffer, "application/vnd.ms-excel");
        //}
        public ActionResult KeyPlanDownloadTemplateCSV()
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            //csv += "Keyword Name" + ',';
            csv += "Keyword Name";
            //Add new line.
            csv += "\r\n";
            //Add the Data rows.
            //csv += "Sports Shoes".Replace(",", ";") + ',';
            csv += "Sports Shoes".Replace(",", ";");
            //Add new line.
            csv += "\r\n";


            string sFileName = "KeywordPlannerDownloadTemplate" + ".csv";



            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }
        [HttpPost]
        public ActionResult GetKeyPlanExcel(string KeywordIdeas, string excludeKeyword)
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];

            //dt is a data table which contains user uploaded file
            DataTable dt = new DataTable();
            dt.Columns.Add("Keyword Name");

            KeyDtl = new Whisker.Areas.Admin.Models.Keyword();

            List<string> ContentResult = new List<string>();

            List<string> KeywordIdeasArray = cmnF.splitWithNewLine(KeywordIdeas);
            List<string> excludeKeywordArray = cmnF.splitWithNewLine(excludeKeyword);

            if (string.IsNullOrEmpty(KeywordIdeas) == true)
            {
                ContentResult.Add("Please enter keyword ideas");//to show error validation message
                ContentResult.Add("0");                   //to show response of stored procedure has not executed
                goto Exitlabel;
            }
            if (KeywordIdeasArray.Count <= 0)
            {
                ContentResult.Add("Please enter valid keyword ideas list");//to show error validation message
                ContentResult.Add("0");                   //to show response of stored procedure has not executed
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(excludeKeyword) != true)
            {
                if (excludeKeywordArray != null)
                {
                    if (excludeKeywordArray.Count <= 0)
                    {
                        ContentResult.Add("Please enter valid excluded keyword");//to show error calidation message
                        ContentResult.Add("0");                   //to show response of stored procedure has not executed
                        goto Exitlabel;
                    }
                }
            }
            //get data from User uploaded file to dt data table
            //List<CommonFunc> cm = new List<CommonFunc>();
            for (int i = 0; i < KeywordIdeasArray.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = KeywordIdeasArray[i]; // or you could generate some random string.
                dt.Rows.Add(dr);
            }
            //dt = CommonFunc.ToDataTable(KeywordIdeasArray);
            //dt = cmnF.ConvertCsvToDatatable(path.ToString(), dt);


            List<string> TempContentResult = new List<string>();
            List<string> InCorrectRows = new List<string>();

            //dt1 is a data table which contains all keyword list
            DataTable dt1 = new DataTable();
            var getKeywords = objDBentity.KeywordMasters.Where(x => x.IsActive == true).OrderByDescending(x => x.KeyCode).ToList();

            // Create a table from the query.
            dt1.Columns.Add("KeyCode");
            dt1.Columns.Add("Keyword Name");
            dt1.Columns.Add("SuggestedBid");
            dt1.Columns.Add("AvgMonthlySrch");
            foreach (var item in getKeywords)
            {
                var row = dt1.NewRow();
                row["KeyCode"] = item.KeyCode;
                row["Keyword Name"] = item.KeyName;
                row["SuggestedBid"] = item.SuggestedBid;
                row["AvgMonthlySrch"] = item.AvgMonthlySrch;
                dt1.Rows.Add(row);
            }

            DataTable resultTable = new DataTable();
            resultTable.Clear();
            resultTable.Columns.Add("UploadedkeywordIdea"); //coming from excel by the user
            resultTable.Columns.Add("KeyCode");
            resultTable.Columns.Add("DBKeyName");// from database
            resultTable.Columns.Add("SuggestedBid");
            resultTable.Columns.Add("AvgMonthlySrch");
            resultTable.Columns.Add("MatchCount");

            double perMatch = 0.0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    perMatch = PercentMatchNew(dt1.Rows[j]["Keyword Name"].ToString(), dt.Rows[i]["Keyword Name"].ToString());
                    if (perMatch > 0)
                    {
                        //excludeKeywordArray = cmnF.splitWithNewLine(excludeKeyword);
                        if (excludeKeywordArray != null)
                        {
                            for (int k = 0; k < excludeKeywordArray.Count; k++)
                            {
                                var kjdkjf = dt1.Rows[j]["Keyword Name"].ToString().ToUpper();
                                var KeywordExist = dt1.Rows[j]["Keyword Name"].ToString().ToUpper().Contains(excludeKeywordArray[k]);
                                if (KeywordExist)
                                {
                                    //if (dt1.Rows[j]["Keyword Name"].ToString().ToUpper().Contains(excludeKeywordArray[0]))
                                    //{
                                    break;
                                }
                                else if (k + 1 == excludeKeywordArray.Count)
                                {
                                    DataRow dr = resultTable.NewRow();
                                    dr["UploadedkeywordIdea"] = dt.Rows[i]["Keyword Name"];
                                    dr["KeyCode"] = dt1.Rows[j]["KeyCode"];
                                    dr["DBKeyName"] = dt1.Rows[j]["Keyword Name"];
                                    dr["SuggestedBid"] = dt1.Rows[j]["SuggestedBid"];
                                    dr["AvgMonthlySrch"] = dt1.Rows[j]["AvgMonthlySrch"];
                                    dr["MatchCount"] = perMatch;
                                    resultTable.Rows.Add(dr);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            DataRow dr = resultTable.NewRow();
                            dr["UploadedkeywordIdea"] = dt.Rows[i]["Keyword Name"];
                            dr["KeyCode"] = dt1.Rows[j]["KeyCode"];
                            dr["DBKeyName"] = dt1.Rows[j]["Keyword Name"];
                            dr["SuggestedBid"] = dt1.Rows[j]["SuggestedBid"];
                            dr["AvgMonthlySrch"] = dt1.Rows[j]["AvgMonthlySrch"];
                            dr["MatchCount"] = perMatch;
                            resultTable.Rows.Add(dr);
                        }
                    }
                }
            }
            Session["DownloadGetKeyPlanExcel"] = resultTable as DataTable;
            ContentResult.Add("Request has been processed successfully");
            ContentResult.Add("");
        Exitlabel:

            return Json(ContentResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ShowGetKeyPlanExcel()
        {
            DataTable resultTable = (DataTable)Session["DownloadGetKeyPlanExcel"];
            List<DataRow> rows = resultTable.Rows.Cast<DataRow>().ToList();
            var ghvhj = Json(rows, JsonRequestBehavior.AllowGet);
            var j1 = CommonFunc.DataTableToJSON(resultTable);
            return Json(j1, JsonRequestBehavior.AllowGet);
            // return null;
        }
        //public ActionResult DownloadGetKeyPlanExcel()
        //{
        //    DataTable resultTable = (DataTable)Session["DownloadGetKeyPlanExcel"];
        //    StringBuilder sb = new StringBuilder();
        //    string sFileName = "DownloadGetKeyPlanExcel" + ".xls";
        //    //Bind data list from edmx
        //    var getKeywords = objDBentity.prcBatchesOnDemand(0, null, null, null).ToArray();

        //    if (getKeywords != null && getKeywords.Any())
        //    {
        //        sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
        //        sb.Append("<caption>Keyword List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
        //        sb.Append("<thead><tr>");
        //        sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Index</b></th>");
        //        sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>UploadedKeyName</b></th>");
        //        sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>DBKeyName</b></th>");
        //        sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>SuggestedBid</b></th>");
        //        sb.Append("</thead></tr><tbody>");
        //        int inc = 0;
        //        for (int i = 0; i < resultTable.Rows.Count; i++)
        //        {
        //            sb.Append("<tr>");
        //            sb.Append("<td align='center' style='width:50px;height:50px;'>" + (i + 1) + "</td>");
        //            sb.Append("<td align='center' style='width:200px;height:50px;'>" + resultTable.Rows[i]["UploadedKeyName"].ToString() + "</td>");
        //            sb.Append("<td align='center' style='width:50px;height:50px;'>" + resultTable.Rows[i]["DBKeyName"].ToString() + "</td>");
        //            sb.Append("<td align='center' style='width:50px;height:50px;'>" + resultTable.Rows[i]["SuggestedBid"].ToString() + "</td>");
        //            sb.Append("</tr>");
        //        }
        //        sb.Append("</tbody></table>");
        //    }

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Session["DownloadGetKeyPlanExcel"] = null;
        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        //    return File(buffer, "application/vnd.ms-excel");
        //}
        public ActionResult DownloadGetKeyPlanCSV()
        {
            string csv = string.Empty;
            DataTable resultTable = (DataTable)Session["DownloadGetKeyPlanExcel"];
            StringBuilder sb = new StringBuilder();
            string sFileName = "DownloadGetKeyPlanExcel" + ".csv";
            //Bind data list from edmx
            var getKeywords = objDBentity.prcBatchesOnDemand(0, null, null, null).ToArray();

            if (getKeywords != null && getKeywords.Any())
            {
                //Build the CSV file data as a Comma separated string.

                csv += "Index" + ',';
                csv += "Uploaded Keyword Idea" + ',';
                csv += "Search Query" + ',';
                csv += "Suggested Bid" + ',';
                csv += "Traffic";
                //Add new line.
                csv += "\r\n";
                for (int i = 0; i < resultTable.Rows.Count; i++)
                {

                    //Add the Data rows.
                    csv += (i + 1).ToString().Replace(",", ";") + ',';
                    csv += resultTable.Rows[i]["UploadedkeywordIdea"].ToString().Replace(",", ";") + ',';
                    csv += resultTable.Rows[i]["DBKeyName"].ToString().Replace(",", ";") + ',';
                    csv += resultTable.Rows[i]["SuggestedBid"].ToString().Replace(",", ";") + ',';
                    csv += resultTable.Rows[i]["AvgMonthlySrch"].ToString().Replace(",", ";");
                    //Add new line.
                    csv += "\r\n";
                }
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";
            Session["DownloadGetKeyPlanExcel"] = null;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }


        //public ActionResult KeyPlanUploadTemplateExcel()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string sFileName = "KeywordPlannerUploadTemplate" + ".xls";

        //    sb.Append("<table>");
        //    sb.Append("<thead><tr>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Campaign Name</b></th>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Adgroup Name</b></th>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Keyword Name</b></th>");
        //    sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Bid</b></th>");
        //    sb.Append("</tr></thead>");
        //    sb.Append("</table>");

        //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    //this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
        //    return File(buffer, "application/vnd.ms-excel");
        //}
        public ActionResult KeyPlanUploadTemplateCSV()
        {
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;
            //csv += "Keyword Name" + ',';
            csv += "Campaign Name" + ',';
            csv += "Adgroup Name" + ',';
            csv += "Keyword Name" + ',';
            csv += "Bid";



            //Add new line.
            csv += "\r\n";
            //Add the Data rows.
            //csv += "Sports Shoes".Replace(",", ";") + ',';
            csv += "Footwear".Replace(",", ";") + ',';
            csv += "Sports Shoes".Replace(",", ";") + ',';
            csv += "Casual Shoes".Replace(",", ";") + ',';
            csv += "100".Replace(",", ";");
            //Add new line.
            //csv += "\r\n";


            string sFileName = "KeywordPlannerUploadTemplate" + ".csv";
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/text";

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csv);
            return File(buffer, "application/text");
        }
        [HttpPost]
        public ActionResult SaveKeyPlanExcel(FormCollection fc, IEnumerable<HttpPostedFileBase> excelfile, string UpdateBidStatus)
        {
            List<string> ContentResult = new List<string>();
            var path = "";
            //try
            //{
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            Int32 IntBatchCode = Convert.ToInt32(objUserInfo.BatchCode);
            Int32 IntAccCode = objUserInfo.AccCode;

            //dt is a data
            DataSet ds = new DataSet();
            DataTable objDt1 = ds.Tables.Add("DataTable1");
            ds.Tables[0].Columns.Add("Campaign Name");
            ds.Tables[0].Columns.Add("Adgroup Name");
            ds.Tables[0].Columns.Add("Keyword Name");
            ds.Tables[0].Columns.Add("Bid");
            ds.Tables[0].Columns.Add("BatchCode");
            ds.Tables[0].Columns["BatchCode"].DefaultValue = objUserInfo.BatchCode.ToString();
            ds.Tables[0].Columns.Add("AccCode");
            ds.Tables[0].Columns["AccCode"].DefaultValue = objUserInfo.AccCode.ToString();
            ds.Tables[0].Columns.Add("UserID");
            ds.Tables[0].Columns["UserID"].DefaultValue = objUserInfo.UserID.ToString();

            ds.Tables[0].Columns.Add("RowNo", typeof(int));
            //Set AutoIncrement True for the First Column.
            ds.Tables[0].Columns["RowNo"].AutoIncrement = true;

            //Set the Starting or Seed value.
            ds.Tables[0].Columns["RowNo"].AutoIncrementSeed = 1;

            //Set the Increment value.
            ds.Tables[0].Columns["RowNo"].AutoIncrementStep = 1;

            DataTable ErrorDataTable = ds.Tables.Add("DataTable2");
            ds.Tables[1].Columns.Add("Index");
            ds.Tables[1].Columns.Add("ErrorMessage");


            var ExcelImagePath = "";
            var FileExtension = "";
            var fileName = "";
            var newPath = "";

            KeyDtl = new Whisker.Areas.Admin.Models.Keyword();
            HttpPostedFileBase UploadedFile = excelfile.FirstOrDefault();

            if (UploadedFile == null || fc["hdnIdenType"].ToUpper() != "KEYPLAN")
            {
                ContentResult.Add("csv file is required");//to show error calidation message
                ContentResult.Add("0");                     //to show response of stored procedure has not executed
                goto Exitlabel;
            }
            if (UploadedFile != null)
            {
                if (UploadedFile.ContentLength > 0)
                {
                    int MaxContentLength = 1024 * 1024 * 4; //Size = 4 MB
                    string[] AllowedFileExtensions = new string[] { ".csv" };
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
            if (fc["hdnIdenType"].ToUpper() == "KEYPLAN")
            {
                path = Path.Combine(Server.MapPath("~/ExcelFiles/"), newPath);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                UploadedFile.SaveAs(path);
                string excelConnectionString = string.Empty;

                //objDBentity.DeleteObject((from y in objDBentity.TempKeywordMappings
                //                 where y.BatchCode == IntBatchCode && y.AccCode == IntAccCode
                //                 select y).ToList());

                //var x1 = (from y in objDBentity.TempKeywordMappings
                //         where y.BatchCode == IntBatchCode && y.AccCode == IntAccCode
                //         select y);

                //IEnumerable<RFA_Result> list = x1.ToList();
                ////// Use Remove Range function to delete all records at once
                //objDBentity.TempKeywordMappings.RemoveRange(list);
                //// Save changes
                //db.SaveChanges();

                //objDBentity.TempKeywordMappings.Remove(x1);
                //objDBentity.SaveChanges();


                //objDBentity.TempKeywordMappings.Where(x => x.BatchCode == IntBatchCode && x.AccCode == IntAccCode).ToList().ForEach(objDBentity.TempKeywordMappings.DeleteObject);
                //objDBentity.SaveChanges();

                //var customers = objDBentity.TempKeywordMappings.Where(c => c.BatchCode == IntBatchCode);

                //objDBentity.TempKeywordMappings.DeleteAllOnSubmit(customers);
                //objDBentity.SubmitChanges();

                // Select all the records to be deleted
                //IEnumerable<objDBentity.TempKeywordMappings> list = objDBentity.TempKeywordMappings.where(x => x.BatchCode == IntBatchCode).toList();
                //// Use Remove Range function to delete all records at once
                //objDBentity.TempKeywordMappings.RemoveRange(list);
                //// Save changes
                //objDBentity.SaveChanges();

                //objDBentity.TempKeywordMappings.RemoveAll(r => r.property == "propertyEntered");
                //x1.Delete();
                var ValidCSV = cmnF.ValidateCsv(path.ToString(), 4);
                if (ValidCSV == false)
                {
                    ContentResult.Add("Invalid csv file");
                    ContentResult.Add("0");
                    goto Exitlabel;
                }

                cmnF.ConvertCsvToDatatable(path.ToString(), ds.Tables[0]);


                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con_whiskers"].ConnectionString;
                System.Data.SqlClient.SqlBulkCopy bc = new System.Data.SqlClient.SqlBulkCopy(con, SqlBulkCopyOptions.TableLock, null);
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From TempKeywordMapping where BatchCode='" + objUserInfo.BatchCode + "' and AccCode='" + objUserInfo.AccCode + "'", con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                bc.DestinationTableName = "TempKeywordMapping";
                bc.ColumnMappings.Add("RowNo", "RowNo");
                bc.ColumnMappings.Add("Campaign Name", "CampaignName");
                bc.ColumnMappings.Add("Adgroup Name", "AdgroupName");
                bc.ColumnMappings.Add("Keyword Name", "KeywordName");
                bc.ColumnMappings.Add("Bid", "Bid");
                bc.ColumnMappings.Add("BatchCode", "BatchCode");
                bc.ColumnMappings.Add("AccCode", "AccCode");
                bc.ColumnMappings.Add("UserID", "UserID");

                bc.WriteToServer(ds.Tables[0]);
                con.Close();


                var ErrorSummary = objDBentity.validateMappingKeword(objUserInfo.BatchCode, objUserInfo.AccCode.ToString()).ToList();
                ContentResult.Add("1");
                ContentResult.Add("Invalid Campaign Name or Blank #" + ErrorSummary[0].cmpCount);
                ContentResult.Add("Invalid AdGroup Name or Blank #" + ErrorSummary[0].AdgroupCount);
                ContentResult.Add("Invalid Bid value (6 digit) or Blank #" + ErrorSummary[0].BidCount);
                ContentResult.Add("Invalid Keyword (Blank or length exceed) #" + ErrorSummary[0].KeywordCount);
                ContentResult.Add("Duplicate rows #" + ErrorSummary[0].DuplicateRowsCount);
                ContentResult.Add("Invalid Campaign AdGroup Mapping #" + ErrorSummary[0].cmpAdgrpMapngCount);

                if (Convert.ToBoolean(UpdateBidStatus) == true)
                {
                    UpdateBidStatus = "1";
                    ContentResult.Add("Keyword Already Exists (Update bid value) #" + "0");
                    ContentResult.Add("Total Error #" + (ErrorSummary[0].TotalCount - ErrorSummary[0].MapngAlreadyExistCount));
                }
                else
                {
                    UpdateBidStatus = "0";
                    ContentResult.Add("Keyword Already Exists #" + ErrorSummary[0].MapngAlreadyExistCount);
                    ContentResult.Add("Total Error #" + ErrorSummary[0].TotalCount);
                }

                if (ErrorSummary[0].TotalCount == 0)
                {
                    objDBentity.saveImportMappingKeyword(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), 0);
                }
                else if ((ErrorSummary[0].TotalCount - ErrorSummary[0].MapngAlreadyExistCount) == 0)
                {
                    if (UpdateBidStatus == "1")
                    {
                        objDBentity.saveImportMappingKeyword(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), 1);
                    }
                }
                //List<string> TempContentResult = new List<string>();
                //List<string> InCorrectRows = new List<string>();
                //int unsuccessfulCount = 0;
                //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //{
                //    KeyDtl.RelatedKeywordCampaign = ds.Tables[0].Rows[i]["Campaign Name"].ToString().Trim();
                //    KeyDtl.RelatedKeywordAdgroup = ds.Tables[0].Rows[i]["Adgroup Name"].ToString().Trim();
                //    KeyDtl.KeywordName = ds.Tables[0].Rows[i]["Keyword Name"].ToString().Trim();
                //    KeyDtl.KeywordSuggestBid = ds.Tables[0].Rows[i]["Bid"].ToString();
                //    TempContentResult = CheckValidKeyword();
                //    if (TempContentResult[0] == "0")
                //    {
                //        InCorrectRows.Add((i + 1).ToString());
                //        unsuccessfulCount++;
                //        ds.Tables[0].Rows[i]["Status"] = "WRONG";

                //        DataRow row = ds.Tables[1].NewRow();
                //        row["Index"] = i + 1;
                //        row["ErrorMessage"] = TempContentResult[1];
                //        ds.Tables[1].Rows.Add(row);

                //        //ds.Tables[1].Rows[i]["Index"] = i+1;
                //        //ds.Tables[1].Rows[i]["ErrorMessage"] = TempContentResult[1];
                //    }
                //    if (TempContentResult[0] == "2")
                //    {
                //        InCorrectRows.Add((i + 1).ToString());
                //        unsuccessfulCount++;
                //        ds.Tables[0].Rows[i]["Keyword Name"] = "";
                //        ds.Tables[0].Rows[i]["Campaign Name"] = "";
                //        ds.Tables[0].Rows[i]["Adgroup Name"] = "";
                //        ds.Tables[0].Rows[i]["Bid"] = "";
                //        ds.Tables[0].Rows[i]["Status"] = "EXIST";

                //        DataRow row = ds.Tables[1].NewRow();
                //        row["Index"] = i + 1;
                //        row["ErrorMessage"] = TempContentResult[1];
                //        ds.Tables[1].Rows.Add(row);
                //        //ds.Tables[1].Rows[i]["Index"] = i + 1;
                //        //ds.Tables[1].Rows[i]["ErrorMessage"] = TempContentResult[1];
                //    }
                //    if (TempContentResult[0] == "1")
                //    {
                //        ds.Tables[0].Rows[i]["Keyword Code"] = string.IsNullOrEmpty(TempContentResult[1]) ? "0" : TempContentResult[1];
                //        ds.Tables[0].Rows[i]["Campaign Name"] = TempContentResult[2];
                //        ds.Tables[0].Rows[i]["Adgroup Name"] = TempContentResult[3];
                //        ds.Tables[0].Rows[i]["Status"] = "RIGHT";
                //    }
                //    //if (TempContentResult[0] == "1")
                //    //{
                //    //    var ghg = TempContentResult[1];
                //    //    var ghg1 = TempContentResult[2];
                //    //    if (TempContentResult[2] == "0")
                //    //    {
                //    //        //Insert New Row into MappedKeywordsAg Table
                //    //        // objDBentity.prcCreateMappedKeywords("", TempContentResult[1], fc["hdnexcelCampaignValue"].ToString(), fc["hdnexcelAdGroupValue"].ToString(), KeyDtl.KeywordSuggestBid, Convert.ToString(objUserInfo.UserID), 1);
                //    //    }
                //    //    if (TempContentResult[2] != "0")
                //    //    {
                //    //        //Update  Row into MappedKeywordsAg Table
                //    //        // objDBentity.prcUpdateMappedKeywords("", TempContentResult[1], fc["hdnexcelCampaignValue"].ToString(), fc["hdnexcelAdGroupValue"], KeyDtl.KeywordSuggestBid, Convert.ToString(objUserInfo.UserID), 1, "UPDATEKEYPLAN");
                //    //    }
                //    //    //objDBentity.prcCreateTrainer(stdtDtl.FirstName, stdtDtl.MiddleName, stdtDtl.LastName, stdtDtl.Location, stdtDtl.Email, stdtDtl.Contact, stdtDtl.LocationCode, null, "INSERT");
                //    //}

                //}
                //if (InCorrectRows.Count == 0)
                //{
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        if (ds.Tables[0].Rows[i]["Status"].ToString() == "RIGHT")
                //        {
                //            if (ds.Tables[0].Rows[i]["Keyword Code"].ToString() != "0")
                //            {
                //                objDBentity.prcCreateMappedKeywords(ds.Tables[0].Rows[i]["Keyword Name"].ToString(), ds.Tables[0].Rows[i]["Keyword Code"].ToString(), ds.Tables[0].Rows[i]["Adgroup Name"].ToString(), ds.Tables[0].Rows[i]["Bid"].ToString(), null, 1);
                //            }
                //            if (ds.Tables[0].Rows[i]["Keyword Code"].ToString() == "0")
                //            {
                //                objDBentity.prcCreateMappedKeywords(ds.Tables[0].Rows[i]["Keyword Name"].ToString(), ds.Tables[0].Rows[i]["Keyword Code"].ToString(), ds.Tables[0].Rows[i]["Adgroup Name"].ToString(), ds.Tables[0].Rows[i]["Bid"].ToString(), null, 0);
                //            }

                //        }

                //    }
                //}
                //ContentResult.Add("1");
                //ContentResult.Add("Total rows " + ds.Tables[0].Rows.Count);
                //ContentResult.Add("Successful rows " + (Convert.ToInt32(ds.Tables[0].Rows.Count) - unsuccessfulCount).ToString());
                //ContentResult.Add("Unsuccessful rows " + InCorrectRows.Count);
                //if (InCorrectRows.Count > 0)
                //{
                //    ContentResult.Add("Incorrect rows index are  " + string.Join(", ", InCorrectRows));
                //}
                //else
                //{
                //    ContentResult.Add("");
                //}
                //var j1 = CommonFunc.DataTableToJSON(ErrorDataTable);
                //var j2 = j1.ToString();
                //var jj = Json(j1, JsonRequestBehavior.AllowGet).ToString();
                //ContentResult.Add(j1.ToString());
                ////excelConnection.Close();
                //// objDBentity.prcCreateAccount(accDtl.AccountName, accDtl.ImagePath, accDtl.AccountDescription, null, "INSERT");
                //// ContentResult.Add("0");//to show response of create message
                ////  ContentResult.Add("1");//to show response of stored procedure working correctly

            }
        Exitlabel:
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception ex)
            //{
            //    if (System.IO.File.Exists(path))
            //    {
            //        System.IO.File.Delete(path);
            //    }
            //    ContentResult.Clear();
            //    ContentResult.Add("Invalid csv file");
            //    ContentResult.Add("0");
            //    return Json(ContentResult, JsonRequestBehavior.AllowGet);
            //}
        }

        private double PercentMatchNew(string source, string target)
        {
            //source=KeyName  target=SQ
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            double matchcount = 0.0;
            double tempCount = 0.0;
            double extraWtg = 0.0;

            ArrayList TargetSequence = new ArrayList();
            int a, b;

            string[] sourceArr = source.Split(' ');
            string[] targetArr = target.Split(' ');
            for (int i = 0; i < targetArr.Length; i++)
            {
                
                
                    if (source.ToLower().IndexOf(targetArr[i].ToLower()) != -1 && targetArr[i].Length > 2)
                    {

                        //double r = ((double)targetArr[i].Length / (double)source.Length);
                        //tempCount = tempCount + r;

                        tempCount = 0.0;
                        for (int j = 0; j < sourceArr.Length; j++)
                        {
                            if (sourceArr[j].ToLower().IndexOf(targetArr[i].ToLower()) != -1)
                            {
                                TargetSequence.Add(j);
                                double r = ((double)targetArr[i].Length / (double)sourceArr[j].Length);
                                tempCount = tempCount + r;
                            }
                        }
                    }

                    else
                    {

                        for (int j = 0; j < sourceArr.Length; j++)
                        {

                            
                            

                                if (targetArr[i].ToLower().IndexOf(sourceArr[j].ToLower()) != -1 && sourceArr[j].Length > 2)
                                {
                                    TargetSequence.Add(j);
                                    double r = ((double)sourceArr[j].Length / (double)targetArr[i].Length);
                                    tempCount = tempCount + r;
                                }
                            


                        }
                    }
                    if (tempCount > 0)
                    {
                        matchcount = matchcount + tempCount;
                    }
                
            }
            extraWtg = 0.0;
            for (int i = 0; i < TargetSequence.Count; i++)
            {
                if (TargetSequence.Count > 1)
                {
                    a = (int)TargetSequence[i];
                    if (i < TargetSequence.Count - 1)
                    {
                        b = (int)TargetSequence[i + 1];
                    }
                    else
                    {
                        b = 0;
                        extraWtg += 0.1;
                    }
                    if (b > a)
                    {
                        extraWtg += 0.1;

                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (sourceArr.Length > targetArr.Length)
            {
                return (extraWtg + matchcount) / (double)sourceArr.Length;
            }
            else
            {
                return (extraWtg + matchcount) / (double)targetArr.Length;
            }


        }
        public List<string> CheckValidKeyword()
        {
            //1 means true and zero means false
            List<string> ContentResult = new List<string>();
            if (string.IsNullOrEmpty(KeyDtl.RelatedKeywordCampaign) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Campaign name is required.");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(KeyDtl.RelatedKeywordAdgroup) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Ad group is required");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(KeyDtl.KeywordName) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Keyword name is required");
                goto Exitlabel;
            }
            if (string.IsNullOrEmpty(KeyDtl.KeywordSuggestBid) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Suggested bid is required");
                goto Exitlabel;
            }
            int value = 0;
            if (!int.TryParse(KeyDtl.KeywordSuggestBid, out value))
            {
                ContentResult.Add("0");
                ContentResult.Add("Suggested bid should be integer");
                goto Exitlabel;
            }
            var CampaignCode = objDBentity.CampaignMasters.Where(x => x.CampaignName == KeyDtl.RelatedKeywordCampaign && x.AccCode == objUserInfo.AccCode).Select(x => x.CampaignCode).FirstOrDefault().ToString();
            if (string.IsNullOrEmpty(CampaignCode) == true)
            {
                ContentResult.Add("Campaign does not exist");
                ContentResult.Add("");
                goto Exitlabel;
            }
            Int32 IntCampaignCode = Convert.ToInt32(CampaignCode);
            var AdGroupCode = objDBentity.AdGroups.Where(x => x.CampaignCode == IntCampaignCode && x.AdGroupName == KeyDtl.RelatedKeywordAdgroup && x.AccCode == objUserInfo.AccCode).Select(x => x.AdGroupCode).FirstOrDefault().ToString();
            //cmnF.GetIntegerPrimaryKeyOnDataExist("AdGroups", "AdGroupName", KeyDtl.RelatedKeywordAdgroup, "AdGroupCode");
            if (string.IsNullOrEmpty(AdGroupCode) == true)
            {
                ContentResult.Add("0");
                ContentResult.Add("Ad group does not exist");
                goto Exitlabel;
            }
            Int32 IntAdGroupCode = Convert.ToInt32(AdGroupCode);
            var CampaignAdGroupExist = objDBentity.AdGroups.Where(x => x.CampaignCode == IntCampaignCode && x.AdGroupCode == IntAdGroupCode).ToList();
            if (CampaignAdGroupExist.Count == 0)
            {
                ContentResult.Add("0");
                ContentResult.Add("Invalid Campaign AdGroup Mapping");
                goto Exitlabel;
            }
            if (CampaignAdGroupExist.Count > 0)
            {
                var KeywordCode = cmnF.GetIntegerPrimaryKeyOnDataExist("KeywordMaster", "KeyName", KeyDtl.KeywordName, "KeyCode");
                KeyDtl.KeywordCode = string.IsNullOrEmpty(KeywordCode) ? "0" : KeywordCode;
                if (string.IsNullOrEmpty(KeywordCode) != true)
                {
                    KeyDtl.KeywordCode = KeywordCode;
                    var existMappedKeyword = cmnF.GetPrimaryKeyOnDataExistInMappedKeywordsAg(IntCampaignCode, IntAdGroupCode, Convert.ToInt32(KeyDtl.KeywordCode)).ToString();
                    //KeyDtl.FromMappedKeywordAg = existMappedKeyword;
                    if (string.IsNullOrEmpty(existMappedKeyword) == true || existMappedKeyword == "0")
                    {
                        ContentResult.Add("1");
                        ContentResult.Add(KeyDtl.KeywordCode);
                        ContentResult.Add(IntCampaignCode.ToString());
                        ContentResult.Add(IntAdGroupCode.ToString());
                        goto Exitlabel;

                    }
                    else
                    {
                        ContentResult.Add("2"); //Already Exist (Exclude it)
                        ContentResult.Add("Keyword Already Exist");
                        goto Exitlabel;
                    }
                }
                else
                {
                    ContentResult.Add("1");
                    ContentResult.Add(KeyDtl.KeywordCode);
                    ContentResult.Add(IntCampaignCode.ToString());
                    ContentResult.Add(IntAdGroupCode.ToString());
                    goto Exitlabel;
                }
            }
        Exitlabel:
            return ContentResult;
        }

        public ActionResult KeyPlannerFinalExcel()
        {
            StringBuilder sb = new StringBuilder();
            string sFileName = "KeyPlannerTemplate" + ".xls";
            //Bind data list from edmx
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getKeywords = objDBentity.getMappedkeyWordsOnDemand(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), objUserInfo.UserID, null, null).ToList();

            if (getKeywords != null && getKeywords.Any())
            {

                sb.Append("<table border='1' style='border:4px solid black; font-size:12px;'>");
                sb.Append("<caption>KeyPlanner List</caption><colgroup align='left'></colgroup><colgroup span='5' align='left'></colgroup>");
                sb.Append("<thead><tr>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Index</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Campaign Name</b></th>");
                sb.Append("<th align='center' style='width:200px;background-color:#fc9003;'><b>Adgroup Name</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Keyword Name</b></th>");
                sb.Append("<th align='center' style='width:50px;background-color:#fc9003;'><b>Bid</b></th>");
                sb.Append("</thead></tr><tbody>");
                int inc = 0;
                foreach (var result in getKeywords)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + (++inc) + "</td>");
                    sb.Append("<td align='center' style='width:200px;height:50px;'>" + result.CampaignName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.AdGroupName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.KeyName + "</td>");
                    sb.Append("<td align='center' style='width:50px;height:50px;'>" + result.NewBid + "</td>");
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
        public JsonResult getKeywordPlannerImportCount(string Status)
        {
            if (Status == "" && Status == null)
                Status = null;
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userCampaign = objDBentity.getKeywordPlannerImportCount(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), Convert.ToInt32(Status)).ToList();
            return Json(userCampaign, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region RFA
        [HttpPost]
        public ActionResult GoForAuction(string Status)
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            int status = 0;
            //status-- zero means not eligible for rfa
            //status-- one means auction request sent
            //status-- two means already request sent
            //status-- three means auction not sent
            var RFAStatus = objDBentity.CheckRFAStatus(Convert.ToString(objUserInfo.AccCode), Convert.ToString(objUserInfo.UserID)).ToList();
            if (RFAStatus.Count <= 0)
            {
                status = 3;
                if (Status == "INSERT")
                {
                    var elgibilityForRfa = objDBentity.prcEligibleForRFA(Convert.ToString(objUserInfo.BatchCode), Convert.ToString(objUserInfo.AccCode)).ToList();
                    if (Convert.ToInt32(elgibilityForRfa[0].ToString()) > 0)
                    {
                        var temp = objDBentity.RFAIniateByStudent(Convert.ToString(objUserInfo.AccCode), Convert.ToString(objUserInfo.UserID));
                        status = 1;
                        objUserInfo.Status = "YSRFA"; //YSRFA means Yes Student RFA (Go For Auction has been done)
                        Session["LoginInfo"] = objUserInfo;
                    }
                    else if (Convert.ToInt32(elgibilityForRfa[0].ToString()) <= 0)
                    {
                        status = 0;
                    }
                }
            }
            if (RFAStatus.Count > 0)
            {
                status = 2;
                objUserInfo.Status = "YSRFA"; //YSRFA means Yes Student RFA (Go For Auction has been done)
                Session["LoginInfo"] = objUserInfo;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Others
        private ActionResult GetAccountDetails()
        {
            if (Session["LoginInfo"] == null)
            {
                return RedirectToRoute("default");
            }
            else
            {
                objUserInfo = (UserLoginInfo)Session["LoginInfo"];

                if (objUserInfo.UserType == 0)
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
        public JsonResult GetCampaigns()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userCampaign = objDBentity.prcGetCampaign(objUserInfo.BatchCode, objUserInfo.AccCode.ToString(), null);
            return Json(userCampaign, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetADGroupsDataDistinct(string cCode)
        {
            if (cCode == "ALL")
                cCode = null;

            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var userAdGroup = objDBentity.prcGetDistAdGroups(cCode, objUserInfo.UserID);

            return Json(userAdGroup, JsonRequestBehavior.AllowGet);

        }
        #endregion

        public ActionResult Opportunities()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            
            return View("~/Views/Home/Opportunities.cshtml", "~/Areas/Students/Views/Shared/Student_Layout.cshtml");
        }
        public ActionResult Resources()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            ViewBag.isStudent = 1;
            //return View("~/Areas/Students/Views/Shared/Student_Layout.cshtml");
            return View("~/Views/Home/Resources.cshtml", "~/Areas/Students/Views/Shared/Student_Layout.cshtml");
        }
    }
}
