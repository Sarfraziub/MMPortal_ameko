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
    
    public partial class POSetting
    {
        public int POSettingId { get; set; }
        public Nullable<bool> NormalApprovalRequired { get; set; }
        public Nullable<int> NormalApprovalByUserId { get; set; }
        public Nullable<bool> RevisedApprovalRequired { get; set; }
        public Nullable<int> RevisedApprovalByUserId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
