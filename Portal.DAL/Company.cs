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
    
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ContactPersonName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> CountryId { get; set; }
        public string ZipCode { get; set; }
        public string CompanyDesc { get; set; }
        public string PANNo { get; set; }
        public string TINNo { get; set; }
        public string TanNo { get; set; }
        public string ServiceTaxNo { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CompanyCode { get; set; }
        public Nullable<decimal> AnnualTurnover { get; set; }
    }
}
