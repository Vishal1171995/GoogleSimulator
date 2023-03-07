﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisker.Areas.Admin.Models
{
    public class Batch
    {
        public string BatchName;
        public string Location;
        public string LocationCode;
        public string Address;
        public string StringStartDate;
        public string StringEndDate;
        public DateTime StartDate;
        public DateTime EndDate;
        public TimeSpan Time;
    }
}