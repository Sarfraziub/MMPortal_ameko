﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
   public class FabricationViewModel
    {      
        public long FabricationId { get; set; }
        public string FabricationNo { get; set; }
        public string FabricationDate { get; set; }
        public long WorkOrderId { get; set; }
        public string WorkOrderNo { get; set; }       
        public int CompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public string CompanyBranchName { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedDate { get; set; }
        public int Modifiedby { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedDate { get; set; }
        public string FabricationStatus { get; set; }
        
        public string message { get; set; }
        public string status { get; set; }

    }
    public class FabricationProductViewModel
    {
        
        public long FabricationDetailId { get; set; }
        public int SequenceNo { get; set; }
        public int FabricationId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductShortDesc { get; set; }
        public string UOMName { get; set; }
        
        public decimal Quantity { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        
    }
   
}
