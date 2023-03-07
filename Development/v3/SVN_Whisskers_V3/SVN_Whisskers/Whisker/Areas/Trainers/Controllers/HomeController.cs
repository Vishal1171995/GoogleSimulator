﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using System.Web.Mvc.Ajax;
using Whisker.App_Start;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Whisker.CommonClass;

namespace Whisker.Areas.Trainers.Controllers
{
    [SessionExpireFilterAttribute]
    [ExceptionHandler]
    public class HomeController : Controller
    {
        //
        // GET: /Trainers/Home/
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();
        CommonFunc cmnF = new CommonFunc();

        public ActionResult Index()
        {
            ViewBag.Active = "#li_ManagBtch";
            GetAccountDetails();
            ViewBag.Batches = objDBentity.BatchAndTrainerMappings.Where(x => x.UserID == objUserInfo.UserID).OrderBy(x => x.BatchCode).FirstOrDefault();
          
            return View();

            //var Cookies = Request.Cookies["MainCookie"];
            //if (Cookies != null)
            //{
            //    GetAccountDetails();
            //    //if (Request.QueryString.HasKeys())
            //    //{
            //    //    if (Request.QueryString["Camp"] != null)
            //    //    {
            //    //        //getCampaign(objUserInfo.UserID, Request.QueryString["Camp"]);
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    //getCampaign(objUserInfo.UserID, null);
            //    //}
            //    ViewBag.Batches = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID).OrderBy(x => x.BatchCode).FirstOrDefault();
            //    return View();
            //}
            //else
            //{
            //    return RedirectToRoute("default");
            //}
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

        public JsonResult GetCurrentBatchesData(string cCode)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            var getBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, cCode, null).ToArray();
            var hgy = Json(getBatches, JsonRequestBehavior.AllowGet);
            return Json(getBatches, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetAllAccountDetails(string accCode)
        //{
        //    var allAccounts = objDBentity.AccountsMasters.ToList();
        //    return Json(allAccounts, JsonRequestBehavior.AllowGet);
        //}
        
        public JsonResult  GetAllBatches(string BatchCode)
        {
            Session["BatchCode"] = BatchCode;
            GetAccountDetails();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                BatchCode = objDBentity.BatchAndTrainerMappings.Where(x => x.UserID == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault().ToString();
                //BatchCode = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault();
            }
            List<object> AllAccountDetail = new List<object>();
            object AllBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();
            object BatchDetails = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", BatchCode).FirstOrDefault();
            object StudentDetails = objDBentity.prcSrudentsPerBatches(BatchCode, "").ToArray();
            object AccountDetails = objDBentity.prcGetRemainingAccounts("", objUserInfo.UserID, BatchCode).ToArray();
            object SavedAccountDetails = objDBentity.prcGetAccntsDetails(objUserInfo.UserID, BatchCode).ToArray().OrderBy(item => item.AccName);

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
            Int32 TempIntid = Convert.ToInt32(id);

            if (id != null)
            {
                ViewBag.ID = objDBentity.BatchAndTrainerMappings.Where(x => x.UserID == objUserInfo.UserID && x.BatchCode == TempIntid).OrderBy(x => x.BatchCode).FirstOrDefault();
                //ViewBag.ID = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID && x.BatchCode == id).FirstOrDefault();
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
                //BatchCode = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault();
            }
            List<object> AllAccountDetail = new List<object>();
            object AllBatches = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", null).ToArray();
            object BatchDetails = objDBentity.prcGetCurrentBatches(objUserInfo.UserID, "a", BatchCode).FirstOrDefault();
            object StudentDetails = objDBentity.prcSrudentsPerBatches(BatchCode, AccCode).ToArray();
            object AccountDetails = objDBentity.prcGetRemainingAccounts(AccCode, objUserInfo.UserID, BatchCode);
            object SavedAccountDetails = objDBentity.prcGetAccntsDetails(objUserInfo.UserID, BatchCode).ToArray();

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
            Int32 IntAccCode = Convert.ToInt32(AccCode);
            var qry = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                      where BatchAndStudentMapping.AccCode == IntAccCode
                      select BatchAndStudentMapping;

            foreach (BatchAndStudentMapping p in qry)
            {
                p.AccCode = null;
                p.Status = null;
            }
            objDBentity.SaveChanges();
            var SaveAccDetails = objDBentity.prcSaveAccount(StudentDetails, AccountDetails, Status, TrainerCode);
            return Json(SaveAccDetails, JsonRequestBehavior.AllowGet);
            //return null;
        }

        public JsonResult FSaveAccount(int Status, string BatchCode)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            Int32 IntBatchCode = Convert.ToInt32(BatchCode);

            var qry = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                      where BatchAndStudentMapping.BatchCode == IntBatchCode && BatchAndStudentMapping.AccCode != null && BatchAndStudentMapping.Status == 0
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
            //return null;
        }

        public JsonResult DeleteAccountDetails(string BatchCode, string AccCode)
        {
            GetAccountDetails();
            //objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            Int32 IntAccCode = Convert.ToInt32(AccCode);
            Int32 IntBatchCode = Convert.ToInt32(BatchCode);

            var qry = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                      where BatchAndStudentMapping.AccCode == IntAccCode && BatchAndStudentMapping.BatchCode == IntBatchCode
                      select BatchAndStudentMapping;

            foreach (BatchAndStudentMapping p in qry)
            {
                p.AccCode = null;
                p.Status = null;
            }
            objDBentity.SaveChanges();
            var SaveAccDetails = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
                                 where BatchAndStudentMapping.AccCode == IntAccCode && BatchAndStudentMapping.BatchCode == IntBatchCode
                                  && BatchAndStudentMapping.Status == 2
                                 select BatchAndStudentMapping;

            return Json(SaveAccDetails, JsonRequestBehavior.AllowGet);
            // return null;
        }

        public JsonResult RFA_bak(string BatchCode, string AccCode)
        {

            //   //GetAccountDetails();
            //   objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            //   String TrainerCode = objUserInfo.UserID;
            //   DataTable dt = new DataTable();
            //var qry = (from SearchQueryMaster in objDBentity.SearchQueryMasters.AsEnumerable()
            //                                  //where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
            //                                  select SearchQueryMaster).ToList();

            //   dt = LINQToDataTable(qry);

            //   DataTable dt1 = new DataTable();
            //  var qry1 = (from bsm in objDBentity.BatchAndStudentMappings
            //                                join mka in objDBentity.MapKeywordsAGs on bsm.UserID equals mka.CreatedBy
            //                                join km in objDBentity.KeywordMasters on mka.KeyCode equals km.KeyCode
            //                                join ag in objDBentity.AdGroups on mka.CampaignCode equals ag.CampaignCode
            //                                //orderby od.OrderID
            //                                where  bsm.BatchCode == "B001" && mka.KeyCode == "KC00000006"  //|| mka.KeyCode == "KC00000007" || mka.KeyCode == "KC00000008"
            //              select new
            //                                {
            //                                    mka.CampaignCode,
            //                                    //mka.AdGroupCode,
            //                                    mka.KeyCode,
            //                                    ag.AdGroupCode,
            //                                    ag.Parent,
            //                                    ag.AdGroupName,
            //                                    ag.ADText,
            //                                    ag.DescLine1,
            //                                    ag.DescLine2,
            //                                    ag.DispUrl,
            //                                    ag.HeadLine,
            //                                    km.KeyName
            //                                });
            //   dt1 = LINQToDataTable(qry1);
            //   //var searchstring = "key";
            //   //DataRow[] filteredRows = dt1.Select("KeyName LIKE '%" + searchstring + "%'");

            //   DataTable result = new DataTable();
            //   //var count = 0;
            //   foreach (DataRow dRow in dt.Rows)
            //   {

            //       var filtered = dt1.AsEnumerable().Where(r => r.Field<String>("HeadLine").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1 || r.Field<String>("DescLine1").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1 || r.Field<String>("DescLine2").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1);

            //       var filteredKey = dt1.AsEnumerable().Where(r => r.Field<String>("KeyName").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1 );

            //       var res = (from x in dt1.AsEnumerable().Where(r => r.Field<String>("KeyName").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1)
            //                  group x by (string)x["KeyName"] into y
            //                  select new { Key = y.Key, Count = y.Count() }).ToArray();

            //       var res1 = (from x in dt1.AsEnumerable().Where(r => r.Field<String>("KeyName").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1)
            //                  group x by (string)x["KeyName"] into y
            //                  select Tuple.Create(y.Key, y.Count())).ToList();

            //       var filteredUrl = dt1.AsEnumerable().Where(r => r.Field<String>("DispUrl").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1 );

            //       var filteredAd = dt1.AsEnumerable().Where(r => r.Field<String>("AdGroupName").IndexOf(dRow["SearchQuery"].ToString(), StringComparison.OrdinalIgnoreCase) != -1);

            //       //.Contains(dRow["SearchQuery"].ToString(), StringComparer.CurrentCultureIgnoreCase).ToString());
            //       //count++;
            //       //if(count == dt.Rows.Count)
            //       //{
            //       //  result = LINQToDataTable(filtered);
            //       //}
            //   }

            //   objDBentity.SaveChanges();
            //   var SaveAccDetails = from BatchAndStudentMapping in objDBentity.BatchAndStudentMappings
            //                        where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
            //                         && BatchAndStudentMapping.Status == 2
            //                        select BatchAndStudentMapping;

            //   return Json(SaveAccDetails, JsonRequestBehavior.AllowGet);
            return null;

        }

        public JsonResult RFA(string CurrentBatchCode)
        {
            //string BatchCode = Convert.ToString(Session["BatchCode"]);
            double perMatch = 0.0;
            double perMatchHeadLine = 0.0;
            double perMatchDescline1 = 0.0;
            double perMatchDescline2 = 0.0;
            double perMatchDispUrl = 0.0;
            double perMatchAdGroupName = 0.0;
            double perMatchtoAdgrps = 0.0;

            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            String TrainerCode = objUserInfo.UserID;
            DataTable dt = new DataTable();


            //Get all search query
            var qry = (from SearchQueryMaster in objDBentity.SearchQueryMasters.AsEnumerable()
                           //where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
                       select SearchQueryMaster).ToList();

            dt = LINQToDataTable(qry);
            //All Batch
            //var allbatchcode = (from c in objDBentity.RFAMasters
            //                     group c by c.BatchCode into grp
            //                     where grp.Count() == 4
            //                     select grp.Key).ToList();


            //Single Batch (Current Batch)
            Int32 IntegerBatchCode = Convert.ToInt32(CurrentBatchCode);
            var allbatchcode = (from c in objDBentity.RFAMasters
                                where c.BatchCode == IntegerBatchCode
                                group c by c.BatchCode into grp
                                select grp.Key).ToList();


            DataTable dtbatch = new DataTable();
            dtbatch = LINQToDataTable(allbatchcode);

            //for (int l = 0; l < allbatchcode.Count; l++)
            //{
            //var bctcode = int.Parse(allbatchcode[l].ToString());
            foreach (int all in allbatchcode)
            {
                RFAHistory rf = new RFAHistory();
                rf.BatchCode = all;
                rf.Status = 0;
                rf.CreatedOn = cmnF.getCurrentDateTime();
                rf.UserID = TrainerCode;

                objDBentity.RFAHistories.Add(rf);
                objDBentity.SaveChanges();
                int rfaid = rf.RFAId;


                var rfa = (from r in objDBentity.RFAHistories
                           where r.Status == 0 && r.BatchCode == all
                           select new
                           {
                               r.RFAId
                           });



                DataTable dt1 = new DataTable();

                var qry1 = (from mka in objDBentity.MapKeywordsAGs
                            join ag in objDBentity.AdGroups on mka.AdGroupCode equals ag.AdGroupCode
                            join cm in objDBentity.CampaignMasters on ag.CampaignCode equals cm.CampaignCode
                            join km in objDBentity.KeywordMasters on mka.KeyCode equals km.KeyCode
                            join ads in objDBentity.tblAds on ag.AdGroupCode equals ads.AdGroupCode
                            //orderby od.OrderID
                            where cm.BatchCode == all && cm.isActive == true//&& mka.KeyCode == "KC00000006"  //|| mka.KeyCode == "KC00000007" || mka.KeyCode == "KC00000008"

                            select new
                            {
                                cm.CampaignCode,
                                mka.KeyCode,
                                ads.ADCode,
                                ag.AdGroupCode,
                                ag.AdGroupName,
                                ads.ADText,
                                ads.DescLine1,
                                ads.DescLine2,
                                ads.DispUrl,
                                ads.HeadLine,
                                km.KeyName,
                                cm.BatchCode,
                                mka.NewBid,
                                cm.AccCode,
                                cm.Budget
                            });

                dt1 = LINQToDataTable(qry1);

                //var searchstring = "key";
                //DataRow[] filteredRows = dt1.Select("KeyName LIKE '%" + searchstring + "%'");
                DataTable dt2 = new DataTable();
                var query = (from keyrank in objDBentity.keyWordRanks
                             select new
                             {
                                 keyrank.Heading,
                                 keyrank.description1,
                                 keyrank.description2,
                                 keyrank.DisplayUrl,
                                 keyrank.AdgroupName
                             });

                dt2 = LINQToDataTable(query);


                //DataRow[] acc;
                //acc = dt2.Select("AccCode=DISTINCT(AccCode)");

                DataTable uniqueCols = new DataTable();

                var acc = (from c in objDBentity.RFAMasters
                           where c.BatchCode == all
                           select c
                            );
                uniqueCols = LINQToDataTable(acc);

                DataTable result = new DataTable();
                result.Clear();
                result.Columns.Add("BatchCode");
                result.Columns.Add("AccCode");
                result.Columns.Add("KeyCode");
                result.Columns.Add("AdgroupCode");
                result.Columns.Add("AdCode");
                result.Columns.Add("CampaignCode");
                result.Columns.Add("MatchCountKTSQ");
                result.Columns.Add("MatchCountKTADs");
                result.Columns.Add("AdRank");
                //result.Columns.Add("ParentAdGroupCode");
                result.Columns.Add("HeadlineMatchCount");
                result.Columns.Add("Descline1MatchCount");
                result.Columns.Add("Descline2MatchCount");
                result.Columns.Add("DispUrlMatchCount");
                result.Columns.Add("AdGroupNamePercentCount");
                result.Columns.Add("SearchQuery");
                result.Columns.Add("Impressions");
                result.Columns.Add("CPC");
                result.Columns.Add("RFA_Id");
                result.Columns.Add("Budget");
                //var count = 0;
                //perMatch = PercentMatchNew("handbags in india", " hand bags in india");
                //perMatch = PercentMatchNew("handbag in india", "bags in india");
                //perMatch = PercentMatchNew("handbag in india bag", "bags in india");

                //dt table contains all search query result
                //dt1 table contains all keyword result
                DataRow dr;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if((dt1.Rows[j]["KeyName"].ToString().ToLower()== "shirt online".ToLower()) && (dt.Rows[i]["SearchQuery"].ToString().ToLower()== "Jackets sale in india".ToLower()))
                        {
                            var bgh = "";
                        }
                        perMatch = PercentMatchNew(dt1.Rows[j]["KeyName"].ToString(), dt.Rows[i]["SearchQuery"].ToString());
                        if (perMatch > 0)
                        {
                        dr = result.NewRow();
                        perMatchHeadLine = (PercentMatchKtoAdd(dt1.Rows[j]["HeadLine"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["Heading"]);
                        perMatchDescline1 = (PercentMatchKtoAdd(dt1.Rows[j]["DescLine1"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["description1"]);
                        perMatchDescline2 = (PercentMatchKtoAdd(dt1.Rows[j]["DescLine2"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["description2"]);
                        perMatchDispUrl = (PercentMatchKtoAdd(dt1.Rows[j]["DispUrl"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["DisplayUrl"]);
                        perMatchAdGroupName = (PercentMatchKtoAdd(dt1.Rows[j]["AdGroupName"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["AdgroupName"]);
                        perMatchtoAdgrps = perMatchHeadLine + perMatchDescline1 + perMatchDescline2 + perMatchDispUrl + perMatchAdGroupName;
                        //perMatch= CalculateSimilarity(dt.Rows[i]["SearchQuery"].ToString(), dt1.Rows[i]["KeyName"].ToString());
                        //if (perMatch>0)
                        //{
                            //dr = result.NewRow();
                            dr["BatchCode"] = dt1.Rows[j]["BatchCode"];
                            dr["AccCode"] = dt1.Rows[j]["AccCode"];
                            dr["KeyCode"] = dt1.Rows[j]["KeyCode"];
                            dr["AdCode"] = dt1.Rows[j]["ADCode"];
                            dr["AdGroupCode"] = dt1.Rows[j]["AdGroupCode"];
                            dr["CampaignCode"] = dt1.Rows[j]["CampaignCode"];
                            dr["Budget"] = dt1.Rows[j]["Budget"];
                            dr["MatchCountKTSQ"] = perMatch;
                            dr["HeadlineMatchCount"] = perMatchHeadLine;
                            dr["Descline1MatchCount"] = perMatchDescline1;
                            dr["Descline2MatchCount"] = perMatchDescline2;
                            dr["DispUrlMatchCount"] = perMatchDispUrl;
                            dr["AdGroupNamePercentCount"] = perMatchAdGroupName;
                            dr["MatchCountKTADs"] = perMatchtoAdgrps;
                            dr["AdRank"] = ((perMatchtoAdgrps + perMatch) * Convert.ToDouble("0" + Convert.ToString(dt1.Rows[j]["NewBid"])));
                            dr["SearchQuery"] = dt.Rows[i]["SearchQuery"];
                            dr["Impressions"] = dt.Rows[i]["Traffic"];
                            dr["CPC"] = dt1.Rows[j]["NewBid"];
                            dr["RFA_Id"] = rfaid;

                            result.Rows.Add(dr);
                        }
                    }
                }

                //batchcode,acccode,searchquery,CampaignCode,adGroupcode
                //May be used in future ----//

                //result.DefaultView.Sort = "BatchCode, AccCode, SearchQuery, CampaignCode, AdGroupCode, AdRank DESC";
                DataTable dbtable = result.Clone();
                //var distinctRows = (from DataRow dRow in result.Rows
                //                    select dRow["BatchCode"]).Distinct();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < uniqueCols.Rows.Count; k++)
                    {
                        try {
                            var rw = result.Select("SearchQuery ='" + dt.Rows[i]["SearchQuery"].ToString() + "' and BatchCode='" + all + "'" + " and AccCode='" + uniqueCols.Rows[k]["AccountCode"].ToString() + "'", "AdRank ASC").Last();
                            dbtable.ImportRow(rw);
                        }
                        catch
                        {

                        }
                    }
                }
                //------------------------//
                //for(int i =0,i< dbtable.Rows.Count;i++)
                //DataView dv = result.DefaultView;
                //dv.Sort = "BatchCode, AdRank DESC";
                //dbtable = dv.ToTable();

                //var checkRFA = (from chckRFA in objDBentity.RFA_Result select chckRFA).Count();
                //if (checkRFA > 0)
                //{
                //var cust = (from c in objDBentity.RFA_Result
                //where c.BatchCode == "B001"
                //select c);
                //IEnumerable<RFA_Result> list = cust.ToList();
                //// Use Remove Range function to delete all records at once
                //objDBentity.RFA_Result.RemoveRange(list);
                //// Save changes
                //db.SaveChanges();

                //}


                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Con_whiskers"].ConnectionString;
                System.Data.SqlClient.SqlBulkCopy bc = new System.Data.SqlClient.SqlBulkCopy(con, SqlBulkCopyOptions.TableLock, null);
                con.Open();
                bc.DestinationTableName = "RFA_Result";
                bc.ColumnMappings.Add("KeyCode", "KeyCode");
                bc.ColumnMappings.Add("AdGroupCode", "AdGroupCode");
                bc.ColumnMappings.Add("AdCode", "AdCode");
                bc.ColumnMappings.Add("BatchCode", "BatchCode");
                bc.ColumnMappings.Add("AccCode", "AccCode");
                //bc.ColumnMappings.Add("AccCode", "AccCode");
                //bc.ColumnMappings.Add("Class_Id", "Class_Id");
                bc.ColumnMappings.Add("CampaignCode", "CampaignCode");
                bc.ColumnMappings.Add("Budget", "Budget");
                bc.ColumnMappings.Add("MatchCountKTSQ", "MatchCountKTSQ");
                //bc.ColumnMappings.Add("MatchCountKTADs", "MatchCountKTADs");
                bc.ColumnMappings.Add("HeadlineMatchCount", "HeadlineMatchCount");
                bc.ColumnMappings.Add("Descline1MatchCount", "Descline1MatchCount");
                bc.ColumnMappings.Add("Descline2MatchCount", "Descline2MatchCount");
                bc.ColumnMappings.Add("DispUrlMatchCount", "DispUrlMatchCount");
                bc.ColumnMappings.Add("AdGroupNamePercentCount", "AdGroupNamePercentCount");
                bc.ColumnMappings.Add("MatchCountKTADs", "MatchCountKTADs");
                bc.ColumnMappings.Add("AdRank", "AdRank");
                bc.ColumnMappings.Add("SearchQuery", "SearchQuery");
                bc.ColumnMappings.Add("Impressions", "Impressions");
                bc.ColumnMappings.Add("CPC", "CPC");
                bc.ColumnMappings.Add("RFA_Id", "RFA_Id");
                bc.WriteToServer(dbtable);
                con.Close();

                //Procedure for calculating all cost,clicks, impression etc.
                var SaveAccDetails = objDBentity.RFA_Calculation(all);

                RFAHistory rfu = new RFAHistory();

                rfu = (from r in objDBentity.RFAHistories
                       where r.RFAId == rfaid
                       select r).FirstOrDefault();

                rfu.Status = 2;
                objDBentity.Entry(rfu).State = EntityState.Modified;
                //objDBentity.RFAHistories.Attach(rfu);
                //objDBentity.ObjectStateManager.ChangeObjectState(rfa, System.Data.EntityState.Modified);
                objDBentity.SaveChanges();
            }
            return Json("RFA has been completed Successfully", JsonRequestBehavior.AllowGet);
        }
        //public JsonResult RFA_12_07_2016()
        //{

        //    string BatchCode = Convert.ToString(Session["BatchCode"]);
        //    double perMatch = 0.0;
        //    double perMatchHeadLine = 0.0;
        //    double perMatchDescline1 = 0.0;
        //    double perMatchDescline2 = 0.0;
        //    double perMatchDispUrl = 0.0;
        //    double perMatchAdGroupName = 0.0;
        //    double perMatchtoAdgrps = 0.0;

        //    objUserInfo = (UserLoginInfo)Session["LoginInfo"];
        //    String TrainerCode = objUserInfo.UserID;
        //    DataTable dt = new DataTable();

        //    var qry = (from SearchQueryMaster in objDBentity.SearchQueryMasters.AsEnumerable()
        //                   //where BatchAndStudentMapping.AccCode == AccCode && BatchAndStudentMapping.TrainerCode == TrainerCode
        //               select SearchQueryMaster).ToList();

        //    dt = LINQToDataTable(qry);

        //    var allbatchcode = (from c in objDBentity.RFAMasters
        //                        group c by c.BatchCode into grp
        //                       // where grp.Count() == 4
        //                        select grp.Key).ToList();


        //    DataTable dtbatch = new DataTable();
        //    dtbatch = LINQToDataTable(allbatchcode);

        //    //for (int l = 0; l < allbatchcode.Count; l++)
        //    //{
        //    //var bctcode = int.Parse(allbatchcode[l].ToString());
        //    foreach (int all in allbatchcode)
        //    {
        //        RFAHistory rf = new RFAHistory();
        //        rf.BatchCode = all;
        //        rf.Status = 0;
        //        rf.CreatedOn = DateTime.Now;
        //        rf.UserID = TrainerCode;

        //        objDBentity.RFAHistories.Add(rf);
        //        objDBentity.SaveChanges();
        //        int rfaid = rf.RFAId;




        //        var rfa = (from r in objDBentity.RFAHistories
        //                   where r.Status == 0 && r.BatchCode == all
        //                   select new
        //                   {
        //                       r.RFAId
        //                   });



        //        DataTable dt1 = new DataTable();
        //        var qry1 = (from bsm in objDBentity.BatchAndStudentMappings
        //                    join mka in objDBentity.MapKeywordsAGs on bsm.UserID equals mka.CreatedBy
        //                    join km in objDBentity.KeywordMasters on mka.KeyCode equals km.KeyCode
        //                    join ag in objDBentity.AdGroups on mka.CampaignCode equals ag.CampaignCode
        //                    join ads in objDBentity.tblAds on ag.AdGroupCode equals ads.AdGroupCode
        //                    //orderby od.OrderID
        //                    where bsm.BatchCode == all //&& mka.KeyCode == "KC00000006"  //|| mka.KeyCode == "KC00000007" || mka.KeyCode == "KC00000008"
        //                    select new
        //                    {
        //                        mka.CampaignCode,
        //                        mka.KeyCode,
        //                        ads.ADCode,
        //                        ag.AdGroupCode,
        //                        ag.AdGroupName,
        //                        ads.ADText,
        //                        ads.DescLine1,
        //                        ads.DescLine2,
        //                        ads.DispUrl,
        //                        ads.HeadLine,
        //                        km.KeyName,
        //                        bsm.BatchCode,
        //                        mka.NewBid,
        //                        bsm.AccCode
        //                    });

        //        dt1 = LINQToDataTable(qry1);

        //        //var searchstring = "key";
        //        //DataRow[] filteredRows = dt1.Select("KeyName LIKE '%" + searchstring + "%'");
        //        DataTable dt2 = new DataTable();
        //        var query = (from keyrank in objDBentity.keyWordRanks
        //                     select new
        //                     {
        //                         keyrank.Heading,
        //                         keyrank.description1,
        //                         keyrank.description2,
        //                         keyrank.DisplayUrl,
        //                         keyrank.AdgroupName
        //                     });

        //        dt2 = LINQToDataTable(query);


        //        //DataRow[] acc;
        //        //acc = dt2.Select("AccCode=DISTINCT(AccCode)");

        //        DataTable uniqueCols = new DataTable();

        //      var acc =  (from c in objDBentity.RFAMasters
        //                  where c.BatchCode == all
        //                  select c
        //                  );
        //        uniqueCols = LINQToDataTable(acc);

        //        DataTable result = new DataTable();
        //        result.Clear();
        //        result.Columns.Add("BatchCode");
        //        result.Columns.Add("AccCode");
        //        result.Columns.Add("KeyCode");
        //        result.Columns.Add("AdgroupCode");
        //        result.Columns.Add("AdCode");
        //        result.Columns.Add("CampaignCode");
        //        result.Columns.Add("MatchCountKTSQ");
        //        result.Columns.Add("MatchCountKTADs");
        //        result.Columns.Add("AdRank");
        //        //result.Columns.Add("ParentAdGroupCode");
        //        result.Columns.Add("HeadlineMatchCount");
        //        result.Columns.Add("Descline1MatchCount");
        //        result.Columns.Add("Descline2MatchCount");
        //        result.Columns.Add("DispUrlMatchCount");
        //        result.Columns.Add("AdGroupNamePercentCount");
        //        result.Columns.Add("SearchQuery");
        //        result.Columns.Add("Impressions");
        //        result.Columns.Add("CPC");
        //        result.Columns.Add("RFA_Id");

        //        //var count = 0;
        //        //perMatch = PercentMatchNew("handbags in india", " hand bags in india");
        //        //perMatch = PercentMatchNew("handbag in india", "bags in india");
        //        //perMatch = PercentMatchNew("handbag in india bag", "bags in india");

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dt1.Rows.Count; j++)
        //            {
        //                DataRow dr = result.NewRow();

        //                perMatch = PercentMatchNew(dt1.Rows[j]["KeyName"].ToString(), dt.Rows[i]["SearchQuery"].ToString());
        //                perMatchHeadLine = (PercentMatchKtoAdd(dt1.Rows[j]["HeadLine"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["Heading"]);
        //                perMatchDescline1 = (PercentMatchKtoAdd(dt1.Rows[j]["DescLine1"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["description1"]);
        //                perMatchDescline2 = (PercentMatchKtoAdd(dt1.Rows[j]["DescLine2"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["description2"]);
        //                perMatchDispUrl = (PercentMatchKtoAdd(dt1.Rows[j]["DispUrl"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["DisplayUrl"]);
        //                perMatchAdGroupName = (PercentMatchKtoAdd(dt1.Rows[j]["AdGroupName"].ToString(), dt1.Rows[j]["KeyName"].ToString())) * Convert.ToDouble(dt2.Rows[0]["AdgroupName"]);
        //                perMatchtoAdgrps = perMatchHeadLine + perMatchDescline1 + perMatchDescline2 + perMatchDispUrl + perMatchAdGroupName;
        //                //perMatch= CalculateSimilarity(dt.Rows[i]["SearchQuery"].ToString(), dt1.Rows[i]["KeyName"].ToString());
        //                dr["BatchCode"] = dt1.Rows[j]["BatchCode"];
        //                dr["AccCode"] = dt1.Rows[j]["AccCode"];
        //                dr["KeyCode"] = dt1.Rows[j]["KeyCode"];
        //                dr["AdCode"] = dt1.Rows[j]["ADCode"];
        //                dr["AdGroupCode"] = dt1.Rows[j]["AdGroupCode"];
        //                dr["CampaignCode"] = dt1.Rows[j]["CampaignCode"];
        //                dr["MatchCountKTSQ"] = perMatch;
        //                dr["HeadlineMatchCount"] = perMatchHeadLine;
        //                dr["Descline1MatchCount"] = perMatchDescline1;
        //                dr["Descline2MatchCount"] = perMatchDescline2;
        //                dr["DispUrlMatchCount"] = perMatchDispUrl;
        //                dr["AdGroupNamePercentCount"] = perMatchAdGroupName;
        //                dr["MatchCountKTADs"] = perMatchtoAdgrps;
        //                dr["AdRank"] = ((perMatchtoAdgrps + perMatch) * Convert.ToDouble("0" + Convert.ToString(dt1.Rows[j]["NewBid"])));
        //                dr["SearchQuery"] = dt.Rows[i]["SearchQuery"];
        //                dr["Impressions"] = dt.Rows[i]["Traffic"];
        //                dr["CPC"] = dt1.Rows[j]["NewBid"];
        //                dr["RFA_Id"] = rfaid;

        //                result.Rows.Add(dr);
        //            }
        //        }

        //        //batchcode,acccode,searchquery,CampaignCode,adGroupcode
        //        //May be used in future ----//

        //        //result.DefaultView.Sort = "BatchCode, AccCode, SearchQuery, CampaignCode, AdGroupCode, AdRank DESC";
        //        DataTable dbtable = result.Clone();
        //        //var distinctRows = (from DataRow dRow in result.Rows
        //        //                    select dRow["BatchCode"]).Distinct();

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            for (int k = 0; k < uniqueCols.Rows.Count; k++)
        //            {

        //                var rw = result.Select("SearchQuery ='" + dt.Rows[i]["SearchQuery"].ToString() + "' and BatchCode='" + all + "'" + " and AccCode='" + uniqueCols.Rows[k]["AccountCode"].ToString() + "'", "AdRank ASC").Last();
        //                dbtable.ImportRow(rw);

        //            }
        //        }
        //        //------------------------//
        //        //for(int i =0,i< dbtable.Rows.Count;i++)
        //        //DataView dv = result.DefaultView;
        //        //dv.Sort = "BatchCode, AdRank DESC";
        //        //dbtable = dv.ToTable();

        //        //var checkRFA = (from chckRFA in objDBentity.RFA_Result select chckRFA).Count();
        //        //if (checkRFA > 0)
        //        //{
        //        //var cust = (from c in objDBentity.RFA_Result
        //        //where c.BatchCode == "B001"
        //        //select c);
        //        //IEnumerable<RFA_Result> list = cust.ToList();
        //        //// Use Remove Range function to delete all records at once
        //        //objDBentity.RFA_Result.RemoveRange(list);
        //        //// Save changes
        //        //db.SaveChanges();

        //        //}


        //        SqlConnection con = new SqlConnection();
        //        con.ConnectionString = ConfigurationManager.ConnectionStrings["Con_whiskers"].ConnectionString;
        //        System.Data.SqlClient.SqlBulkCopy bc = new System.Data.SqlClient.SqlBulkCopy(con, SqlBulkCopyOptions.TableLock, null);
        //        con.Open();
        //        bc.DestinationTableName = "RFA_Result";
        //        bc.ColumnMappings.Add("KeyCode", "KeyCode");
        //        bc.ColumnMappings.Add("AdGroupCode", "AdGroupCode");
        //        bc.ColumnMappings.Add("AdCode", "AdCode");
        //        bc.ColumnMappings.Add("BatchCode", "BatchCode");
        //        bc.ColumnMappings.Add("AccCode", "AccCode");
        //        //bc.ColumnMappings.Add("AccCode", "AccCode");
        //        //bc.ColumnMappings.Add("Class_Id", "Class_Id");
        //        bc.ColumnMappings.Add("CampaignCode", "CampaignCode");
        //        bc.ColumnMappings.Add("MatchCountKTSQ", "MatchCountKTSQ");
        //        //bc.ColumnMappings.Add("MatchCountKTADs", "MatchCountKTADs");
        //        bc.ColumnMappings.Add("HeadlineMatchCount", "HeadlineMatchCount");
        //        bc.ColumnMappings.Add("Descline1MatchCount", "Descline1MatchCount");
        //        bc.ColumnMappings.Add("Descline2MatchCount", "Descline2MatchCount");
        //        bc.ColumnMappings.Add("DispUrlMatchCount", "DispUrlMatchCount");
        //        bc.ColumnMappings.Add("AdGroupNamePercentCount", "AdGroupNamePercentCount");
        //        bc.ColumnMappings.Add("MatchCountKTADs", "MatchCountKTADs");
        //        bc.ColumnMappings.Add("AdRank", "AdRank");
        //        bc.ColumnMappings.Add("SearchQuery", "SearchQuery");
        //        bc.ColumnMappings.Add("Impressions", "Impressions");
        //        bc.ColumnMappings.Add("CPC", "CPC");
        //        bc.ColumnMappings.Add("RFA_Id", "RFA_Id");
        //        bc.WriteToServer(dbtable);
        //        con.Close();

        //        //Procedure for calculating all cost,clicks, impression etc.
        //        var SaveAccDetails = objDBentity.RFA_Calculation();

        //        RFAHistory rfu = new RFAHistory();

        //        rfu = (from r in objDBentity.RFAHistories
        //               where r.RFAId == rfaid
        //               select r).FirstOrDefault();

        //        rfu.Status = 2;
        //        objDBentity.Entry(rfu).State = EntityState.Modified;
        //        //objDBentity.RFAHistories.Attach(rfu);
        //        //objDBentity.ObjectStateManager.ChangeObjectState(rfa, System.Data.EntityState.Modified);
        //        objDBentity.SaveChanges();
        //    }
        //    return Json("RFA has been completed Successfully", JsonRequestBehavior.AllowGet);
        //}


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
                tempCount = 0.0;
               
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
                return (extraWtg + (matchcount / (double)sourceArr.Length));
            }
            else
            {
                return (extraWtg + (matchcount / (double)targetArr.Length));
            }


        }

        private double PercentMatchKtoAdd(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            double matchcount = 0.0;
            double tempCount = 0.0;
            string[] sourceArr = source.Split(' ');
            string[] targetArr = target.Split(' ');
            for (int i = 0; i < targetArr.Length; i++)
            {
                tempCount = 0.0;
                
             
                    if (source.ToLower().IndexOf(targetArr[i].ToLower()) != -1 & targetArr[i].Length > 2)
                    {

                        //double r = ((double)targetArr[i].Length / (double)source.Length);
                        //tempCount = tempCount + r;


                        for (int j = 0; j < sourceArr.Length; j++)
                        {
                            if (sourceArr[j].ToLower().IndexOf(targetArr[i].ToLower()) != -1)
                            {
                                double r = ((double)targetArr[i].Length / (double)sourceArr[j].Length);
                                tempCount = tempCount + r;
                            }
                        }
                    }
                

                else
                {

                    for (int j = 0; j < sourceArr.Length; j++)
                    {

                        if (targetArr[i].ToLower().IndexOf(sourceArr[j].ToLower()) != -1 & sourceArr[j].Length>2)
                        {
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



            if (sourceArr.Length > targetArr.Length)
            {
                return matchcount / (double)targetArr.Length;
            }
            else
            {
                return matchcount / (double)targetArr.Length;
            }
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

        //private double PercentMatchNew(string source, string target)
        //{

        //    if ((source == null) || (target == null)) return 0.0;
        //    if ((source.Length == 0) || (target.Length == 0)) return 0.0;
        //    if (source == target) return 1.0;

        //    double matchcount = 0.0;
        //    double tempCount = 0.0;
        //    string[] sourceArr = source.Split(' ');
        //    string[] targetArr = target.Split(' ');

        //    for (int i = 0; i < targetArr.Length; i++)
        //    {
        //        tempCount = 0.0;
        //        if (source.ToLower().IndexOf(targetArr[i].ToLower()) != -1)
        //        {

        //            tempCount++;

        //        }
        //        else
        //        {

        //            for (int j = 0; j < sourceArr.Length; j++)
        //            {
        //                if (targetArr[i].ToLower().IndexOf(sourceArr[j].ToLower()) != -1)
        //                {
        //                    double r = ((double)sourceArr[j].Length / (double)targetArr[i].Length);
        //                    tempCount = tempCount + r;
        //                }
        //            }
        //            //for (int j = 0; j < targetArr.Length; j++)

        //            //{
        //            //    if (targetArr[j].Length < sourceArr[i].Length)
        //            //    {
        //            //        if (targetArr[j].StartsWith(sourceArr[i].Substring(0, targetArr[j].Length)))
        //            //        {
        //            //            //str = str + "-" + sourceArr[i];
        //            //            double r = (sourceArr[i].Length / targetArr[j].Length);
        //            //            tempCount = tempCount + r;

        //            //        }

        //            //    }
        //            //    else if (targetArr[j].Length > sourceArr[i].Length)
        //            //    {
        //            //        if (targetArr[j].Substring(0, sourceArr[i].Length).StartsWith(sourceArr[i]))
        //            //        {
        //            //            //str = str + "-" + sourceArr[i];
        //            //            double r = (targetArr[j].Length / sourceArr[i].Length);
        //            //            tempCount = tempCount + r;
        //            //        }
        //            //    }
        //            //    //else
        //            //    //{
        //            //    //    if (targetArr[j].StartsWith(sourceArr[i]))
        //            //    //    {
        //            //    //        //str = str + "-" + sourceArr[i];
        //            //    //        tempCount++;
        //            //    //    }
        //            //    //}



        //            //}
        //        }

        //        if (tempCount > 0)
        //        {
        //            matchcount = matchcount + tempCount;
        //        }
        //    }

        //    if (sourceArr.Length > targetArr.Length)
        //    {
        //        return matchcount / (double)sourceArr.Length;
        //    }
        //    else
        //    {
        //        return matchcount / (double)targetArr.Length;
        //    }


        //}

        /// <summary>
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = ComputeLevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }

        public JsonResult CancelAuction(string rfaId, string BatchCode, string AccCode)
        {
            objDBentity.CancelAuction(rfaId, BatchCode, AccCode);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckRfaApplicable(string BatchCode)
        {
            GetAccountDetails();
            if (BatchCode == null || BatchCode == "" || BatchCode == "undefined")
            {
                BatchCode = objDBentity.BatchAndTrainerMappings.Where(x => x.UserID == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault().ToString();
                //BatchCode = objDBentity.BatchMasters.Where(x => x.TrainerCode == objUserInfo.UserID).OrderBy(x => x.BatchCode).Select(x => x.BatchCode).FirstOrDefault();
            }
            List<object> AllAccountDetail = new List<object>();

            var AllAccCount = objDBentity.prcGetRFAAccCount(objUserInfo.UserID, BatchCode, "AccCount").ToList();
            var RfaCount = objDBentity.prcGetRFAAccCount(objUserInfo.UserID, BatchCode, "RfaCount").ToList();

            var aa = AllAccCount.Count;
            var aa1 = RfaCount.Count;

            if(AllAccCount.Count == RfaCount.Count && AllAccCount.Count != 0)
            {
                AllAccountDetail.Add("TRUE");
                AllAccountDetail.Add("");
            }
            else
            {
                AllAccountDetail.Add("FALSE");
                AllAccountDetail.Add("");
            }
            var vg = Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
            return Json(AllAccountDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Opportunities()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            
            return View("~/Views/Home/Opportunities.cshtml", "~/Areas/Trainers/Views/Shared/Trainer_Layout.cshtml");
        }
        public ActionResult Resources()
        {
            objUserInfo = (UserLoginInfo)Session["LoginInfo"];
            GetAccountDetails();
            return View("~/Views/Home/Resources.cshtml", "~/Areas/Trainers/Views/Shared/Trainer_Layout.cshtml");
        }
    }
}
