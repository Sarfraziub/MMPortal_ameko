//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portal.DAL.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class HR_EmployeeTravelApplication
    {
        public long ApplicationId { get; set; }
        public string ApplicationNo { get; set; }
        public Nullable<System.DateTime> ApplicationDate { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<long> EmployeeId { get; set; }
        public Nullable<int> TravelTypeId { get; set; }
        public string TravelDestination { get; set; }
        public Nullable<System.DateTime> TravelStartDate { get; set; }
        public Nullable<System.DateTime> TravelEndDate { get; set; }
        public string TravelReason { get; set; }
        public string TravelStatus { get; set; }
        public Nullable<int> ApproveBy { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public Nullable<int> RejectBy { get; set; }
        public Nullable<System.DateTime> RejectDate { get; set; }
        public string RejectReason { get; set; }
    }
}
