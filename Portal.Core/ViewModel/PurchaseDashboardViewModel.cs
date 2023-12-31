﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
 public   class PurchaseDashboardViewModel
    {
        public List<POCountViewModel> bookBalanceList { get; set; }
    }
    public class POCountViewModel
    {       
        public string POCount_Head { get; set; }            
        public string POTotalCount { get; set; }
        public int POTodayCount { get; set; }
        public string TodayPOSumAmount { get; set; }
        public string ApprovalStatus { get; set; }
    }
    public class PICountViewModel
    {
        public string PICount_Head { get; set; }
        public string PITotalCount { get; set; }
        public int PITodayCount { get; set; }
        public string TodayPISumAmount { get; set; }
    }



}
