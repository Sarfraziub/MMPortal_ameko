using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.ViewModel
{
  public  class SiteConsumptionViewModel
    {
        public int SiteConsumptionId { get; set; }
        public string SiteConsumptionNo { get; set; }
        public string SiteConsumptionDate { get; set; }
    
        public int CompanyId { get; set; }
        public int CompanyBranchId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerBranchId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyBranchName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerBranchName { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedByUserName { get; set; }
        public string ModifiedDate { get; set; }
        public string ConsumptionStatus { get; set; }
        public int FinYearId { get; set; }
        public int ConsumeSiteSequence { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public string ConsumedByUser { get; set; }
    }
    public class SiteConsumptionProductViewModel
    {

        public long SiteConsumptionDetailId { get; set; }
        public int SequenceNo { get; set; }
        public int SiteConsumptionId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductMainGroupId { get; set; }
        public string ProductMainGroupName { get; set; }
        public string ProductCode { get; set; }
        public string ProductShortDesc { get; set; }
        public string UOMName { get; set; }

        public decimal Quantity { get; set; }
        public string message { get; set; }
        public string status { get; set; }

    }
}
