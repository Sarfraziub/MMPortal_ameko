//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portal.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteConsumption
    {
        public long SiteConsumptionId { get; set; }
        public string SiteConsumptionNo { get; set; }
        public Nullable<System.DateTime> SiteConsumptionDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> CustomerBranchId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CompanyBranchId { get; set; }
        public string ConsumedByUser { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ConsumptionStatus { get; set; }
        public Nullable<int> FinYearId { get; set; }
        public Nullable<int> ConsumeSiteSequence { get; set; }
    }
}
