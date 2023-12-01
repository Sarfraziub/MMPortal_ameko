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
    
    public partial class Vendor
    {
        public int VendorId { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string ContactPersonName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ContactNo { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string PinCode { get; set; }
        public string CSTNo { get; set; }
        public string TINNo { get; set; }
        public string PANNo { get; set; }
        public string GSTNo { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Modifiedby { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public Nullable<int> CreditDays { get; set; }
        public string ExciseNo { get; set; }
        public Nullable<bool> GST_Exempt { get; set; }
        public Nullable<decimal> AnnualTurnover { get; set; }
    }
}
