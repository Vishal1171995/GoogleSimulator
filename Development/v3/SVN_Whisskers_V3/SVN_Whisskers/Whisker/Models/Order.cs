using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Whisker.Models
{
    [Serializable()]
    public class Order
    {
        public string UserID;
        public string tid;
        public string merchant_id = "165356";
        public string order_id;
        public string currency;
        public string redirect_url = ConfigurationManager.AppSettings["redirect_url"].ToString();
        public string cancel_url = ConfigurationManager.AppSettings["cancel_url"].ToString();
        public decimal amount;
        public Order()
        {
         //redirect_url = ConfigurationManager.AppSettings["redirect_url"].ToString();
         //cancel_url = ConfigurationManager.AppSettings["redirect_url"].ToString();
         }
    }
}