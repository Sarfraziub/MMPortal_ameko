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
    
    public partial class HR_JobOpening
    {
        public long JobOpeningId { get; set; }
        public string JobOpeningNo { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<System.DateTime> JobOpeningDate { get; set; }
        public string JobTitle { get; set; }
        public string JobPortalRefNo { get; set; }
        public Nullable<int> NoOfOpening { get; set; }
        public Nullable<int> MinExp { get; set; }
        public Nullable<int> MaxExp { get; set; }
        public Nullable<decimal> MinSalary { get; set; }
        public Nullable<decimal> MaxSalary { get; set; }
        public string KeySkills { get; set; }
        public string JobDescription { get; set; }
        public Nullable<System.DateTime> JobStartDate { get; set; }
        public Nullable<System.DateTime> JobExpiryDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string JobStatus { get; set; }
        public Nullable<int> RequisitionId { get; set; }
        public Nullable<int> EducationId { get; set; }
        public string OtherQualification { get; set; }
        public string CurrencyCode { get; set; }
        public string Remarks { get; set; }
    }
}
