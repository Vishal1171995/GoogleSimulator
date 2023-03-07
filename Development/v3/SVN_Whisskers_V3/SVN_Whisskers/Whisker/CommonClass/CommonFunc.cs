//**************************************************************
//  Creator Name    : Aman Jain  
//  Creation Date   : 21-04-2016
//  Reason          : To create common fuction that can be used throughout the project.
//  Updated By      : Aman Jain
//  Update Date     : 21-04-2016
//**************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Whisker.Models;
using System.Web.Mvc.Ajax;
using Whisker.App_Start;
using Whisker;
using System.Globalization;
//using System.Data.Linq;
using System.Collections;
using Whisker.Areas.Admin.Models;
using System.Net.Mail;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Web.Script.Serialization;
/// <summary>
/// common fuction that can be used throughout the project.
/// </summary>
namespace Whisker.CommonClass
{
    public class CommonFunc
    {
        WhiskersDBEntities objDBentity = new WhiskersDBEntities();
        UserLoginInfo objUserInfo = new UserLoginInfo();

        public DataTable ConvertCsvToDatatable(string csvPath, DataTable dt)
        {
            string csvData = File.ReadAllText(csvPath);
            int j = 0;
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    int i = 0;

                    if (j > 0)
                    {
                        dt.Rows.Add();
                        foreach (string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                            i++;
                        }
                    }
                    j++;
                }
            }
            return dt;
        }
        public string GetPrimaryKeyOnDataExist(string TableName, string ColumnName, string ColumnValue, string SelectedColumn)
        {
            string resultvalue = null;
            ColumnValue = ColumnValue.Trim();
            string qry = "select " + SelectedColumn + " from " + TableName + " where isActive = 1 and " + ColumnName + "={0}";
            var result = objDBentity.Database.SqlQuery<string>(qry, ColumnValue);
            foreach (var r in result)
            {
                resultvalue = r.ToString();
            }
            return resultvalue;
        }
        public string GetIntegerPrimaryKeyOnDataExist(string TableName, string ColumnName, string ColumnValue, string SelectedColumn)
        {
            string resultvalue = null;
            ColumnValue = ColumnValue.Trim();
            string qry = "select " + SelectedColumn + " from " + TableName + " where isActive = 1 and " + ColumnName + "={0}";
            var result = objDBentity.Database.SqlQuery<Int32>(qry, ColumnValue);
            foreach (var r in result)
            {
                resultvalue = r.ToString();
            }
            return resultvalue;
        }
        public string GetPrimaryKeyOnDataExistInMappedKeywordsAg(Int32 CampaignCode, Int32 AdGroupCode, Int32 KeyCode)
        {

            string resultvalue = null;
            resultvalue = Convert.ToString(objDBentity.MapKeywordsAGs.Where(x => x.AdGroupCode == AdGroupCode && x.KeyCode == KeyCode).Select(x => x.KeyCode).FirstOrDefault());
            return resultvalue;
        }
        public DateTime StringToDate(string InputDate)
        {
            DateTime dateTime = Convert.ToDateTime(InputDate.Trim()).Date;
            return dateTime;
        }
        public Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void SendPasscodeToMail(string toAddress, string FirstName, string passCode, string user_name, string Subject)
        {
            try {
                var email = toAddress;
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.To.Add("amanjain203@gmail.com");
                //mail.From = new MailAddress("aman.jain@convergenttec.com");
                mail.From = new MailAddress("pv@buttermilktraining.com");


                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "mail.convergenttec.com"; //Or Your SMTP Server Address For Convergenttec
                //smtp.Host = "mail.convergenttec.com"; //Or Your SMTP Server Address For Convergenttec
                smtp.Host = "mail.buttermilktraining.com"; //Or Your SMTP Server Address For buttermiltraining

                smtp.Credentials = new System.Net.NetworkCredential("pv@buttermilktraining.com", "qY@NBC@6"); // ***use valid credentials***
                //smtp.Port = 587;
                //Or your Smtp Email ID and Password
                //smtp.EnableSsl = false;// For Convergenttec
                //smtp.EnableSsl = true;// For gmail
                mail.Body = "<html xmlns='http://www.w3.org/1999/xhtml'>";
                mail.Body += "<head>";
                mail.Body += "<meta name='viewport' content='width=device-width'>";
                mail.Body += "</head>";
                mail.Body += "<body>";
                mail.Body += "<div >";
                mail.Body += "<div >";
                mail.Body += "<div >";
                mail.Body += " <p>";
                mail.Body += "<table style=' background: #eee;' align='left' border='0' cellpadding='0' cellspacing='0'>";
                mail.Body += "<tbody>";
                mail.Body += "<tr>";
                mail.Body += " <td>";
                mail.Body += "<table style='font-family: Arial, Helvetica, sans-serif; background: #fff;' align='center' border='0' cellpadding='0' cellspacing='0'>";
                mail.Body += "<tbody>";
                mail.Body += " <tr>";
                mail.Body += " <td >";
                mail.Body += "<div style=''>";
                mail.Body += "<img alt='whisskers Logo' src='http://whisskers.com/wp-content/themes/whisskers/images/whisskers_logo.jpg'/></div><br/>";
                mail.Body += "</td>";
                mail.Body += "</tr>";
                mail.Body += "<tr>";
                mail.Body += "<td >";//Dear Parent<br />";
                mail.Body += "<br />";
                mail.Body += "Dear" + " " + FirstName + "," + "<br/><br/>" + "Your login credentials are: " + "<br/><br/>";
                mail.Body += "UserName: " + "<b>" + user_name + "</b>" + "." + "<br/><br/>";
                mail.Body += "Password:" + "<b>" + " " + passCode + "</b>" + "." + "<br/><br/>";
                mail.Body += " <br />";
                mail.Body += " Team whisskers.";
                mail.Body += "<br />";
                mail.Body += "<br />";
                mail.Body += " </td>";
                mail.Body += " </tr>";
                mail.Body += " <tr>";
                mail.Body += " <td style='font-size: 11px; color: #888; border-top: 1px solid #eee; '>Please do not reply to this message.It is a service email related to your Whisskers account.</td>";
                mail.Body += "</tr>";
                mail.Body += "</tbody>";
                mail.Body += "</table>";
                mail.Body += " </td>";
                mail.Body += " </tr>";
                mail.Body += "</tbody></table></p></div></div></div></body></html>";
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }
            catch
            {
                objDBentity.prcCreateErrLog("MailError", "MailError", "MailError", "Mail not sent", "MailErrorr", false);
            }
            
        }
        public void SendErrorMail(string toAddress, string FirstName, string Controller, string Action, string Exception, string Stacktrace, string Subject)
        {
            try
            {
                var email = toAddress;
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.To.Add("amanjain203@gmail.com");
                mail.From = new MailAddress("pv@buttermilktraining.com");
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.buttermilktraining.com"; //Or Your SMTP Server Address For buttermiltraining
                smtp.Credentials = new System.Net.NetworkCredential("pv@buttermilktraining.com", "qY@NBC@6"); // ***use valid credentials***
                //smtp.Port = 587;
                //Or your Smtp Email ID and Password
                smtp.EnableSsl = false;// For Convergenttec
                //smtp.EnableSsl = true;// For gmail
                mail.Body = "<html xmlns='http://www.w3.org/1999/xhtml'>";
                mail.Body += "<head>";
                mail.Body += "<meta name='viewport' content='width=device-width'>";
                mail.Body += "</head>";
                mail.Body += "<body>";
                mail.Body += "<div >";
                mail.Body += "<div >";
                mail.Body += "<div >";
                mail.Body += " <p>";
                mail.Body += "<table style=' background: #eee;' align='left' border='0' cellpadding='0' cellspacing='0'>";
                mail.Body += "<tbody>";
                mail.Body += "<tr>";
                mail.Body += " <td>";
                mail.Body += "<table style='font-family: Arial, Helvetica, sans-serif; background: #fff;' align='center' border='0' cellpadding='0' cellspacing='0'>";
                mail.Body += "<tbody>";
                mail.Body += " <tr>";
                mail.Body += " <td >";
                mail.Body += "<div style=''>";
                mail.Body += "<img alt='whisskers Logo' src='http://whisskers.com/wp-content/themes/whisskers/images/whisskers_logo.jpg'/></div><br/>";
                mail.Body += "</td>";
                mail.Body += "</tr>";
                mail.Body += "<tr>";
                mail.Body += "<td >";//Dear Parent<br />";
                mail.Body += "<br />";
                mail.Body += "Dear" + " " + FirstName + "," + "<br/><br/>" + "Error founds " + "<br/><br/>";
                mail.Body += "Controller: " + "<b>" + Controller + "</b>" + "." + "<br/><br/>";
                mail.Body += "Action:" + "<b>" + " " + Action + "</b>" + "." + "<br/><br/>";
                mail.Body += "Exception:" + "<b>" + " " + Exception + "</b>" + "." + "<br/><br/>";
                mail.Body += "Stacktrace:" + "<b>" + " " + Stacktrace + "</b>" + "." + "<br/><br/>";
                mail.Body += "<br />";
                mail.Body += " Team whisskers.";
                mail.Body += "<br />";
                mail.Body += "<br />";
                mail.Body += " </td>";
                mail.Body += " </tr>";
                mail.Body += " <tr>";
                mail.Body += " <td style='font-size: 11px; color: #888; border-top: 1px solid #eee; '>Please do not reply to this message.It is a service email related to your Whisskers account.</td>";
                mail.Body += "</tr>";
                mail.Body += "</tbody>";
                mail.Body += "</table>";
                mail.Body += " </td>";
                mail.Body += " </tr>";
                mail.Body += "</tbody></table></p></div></div></div></body></html>";
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }
            catch
            {
                objDBentity.prcCreateErrLog("ErrorInErrorMail", "ErrorInErrorMail", "ErrorInErrorMail", "ErrorInErrorMail", "ErrorInErrorMail", false);
            }
        }

        /*splitWithNewLine Function split string with '/r/n'(new line) and remove all space(leading,trailing and multiple space b/w two character)
        for each splitted cell.*/
        public List<string> splitWithNewLine(string StringToBeSplitted)
        {
            if (!String.IsNullOrWhiteSpace(StringToBeSplitted))
            {
                List<string> lines1 = Regex.Split(StringToBeSplitted, "\n").Select(p => Regex.Replace(p, @"\s+", " ").Trim().ToUpper()).Where(x => !string.IsNullOrEmpty(x)).ToArray().ToList();
                return lines1;
            }
            else
            {
                return null;
            }
        }
        public static object DataTableToJSON(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = (Convert.ToString(row[col]));
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(list);
        }

        //CnvrtRoundToDcimal function is used to covert into decimal and round result 
        public Decimal CnvrtRoundToDcimal(Object data, Int32 roundValue)
        {
            var kmkl = Math.Round(55.45, 2);
            Decimal tempdata = Convert.ToDecimal(data);
            if (tempdata == 0)
            {
                tempdata = 0.00M;
            }
            var roundeddata = Math.Round(tempdata, roundValue);
            return roundeddata;
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
        //public void ValidateCsv(string fileContents)
        //{
        //    var fileLines = fileContents.Split(
        //          new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        //    if (fileLines.Count < 2)
        //        fail - no data row.

        //        ValidateColumnHeader(fileLines[0]);

        //    ValidateRows(fileLines.Skip(1));
        //}

        //public bool ValidateColumnHeaders(string header)
        //{
        //    return header.Trim().Replace(' ', '').ToLower() ==
        //       "name,address,age,gender";
        //}

        //public bool ValidateRows(IEnumerable<string> rows)
        //{
        //    foreach (row in rows)
        //    {
        //        var cells = row.Split(',');

        //        check if the number of cells is correct
        //        if (!cells.Length == 4)
        //            return false;

        //        ensure gender is correct
        //        if (cells[3] != "M" && cells[3] != "F")
        //            return false;

        //        perform any additional row checks relevant to your domain
        //    }
        //}

        public bool ValidateCsv(string csvPath, int CellCount)
        {
            bool status = true;

            string fileContents = File.ReadAllText(csvPath);
            var fileLines = fileContents.Split(
                  new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            if (fileLines.Count() < 2)
            {
                status = false;
                return status;
            }
            status = ValidateRows(fileLines.Skip(1), CellCount);
            return status;
        }
        public bool ValidateRows(IEnumerable<string> rows, int CellCount)
        {
            bool status = true;
            foreach (var row in rows)
            {
                var cells = row.Split(',');

                //check if the number of cells is correct
                if (cells.Length != CellCount && cells[0] != "")
                {
                    status = false;
                }

                //if (cells[3] != "M" && cells[3] != "F")
                //    return false;
            }
            return status;
        }

        public DateTime getCurrentDateTime()
        {
            return DateTime.UtcNow.AddHours(5.5);
        }
    }
}
