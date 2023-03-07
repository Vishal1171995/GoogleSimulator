using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisker.Models
{
    [Serializable()]
    public class UserLoginInfo
    {
        public string UserID;
        public string FirstName;
        public string LastName;
        public string EMAILID;
        public Int32 AccCode;
        public string AccName;
        public Int16 UserType;
        public string Status;
        public string BatchCode;
        public string BatchName;
        public string Password;
        public string Organization;
        public string MobileNo;
        public string MiddleName;
        public string Location;
    }
}
