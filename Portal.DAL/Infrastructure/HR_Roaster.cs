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
    
    public partial class HR_Roaster
    {
        public int RoasterId { get; set; }
        public string RoasterName { get; set; }
        public string RoasterDesc { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<System.DateTime> RoasterStartDate { get; set; }
        public string RoasterType { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> NoOfWeeks { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
