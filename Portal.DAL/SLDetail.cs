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
    
    public partial class SLDetail
    {
        public long SLDetailId { get; set; }
        public Nullable<int> GLId { get; set; }
        public Nullable<long> SLId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> FinYearId { get; set; }
        public Nullable<decimal> OpeningBalanceDebit { get; set; }
        public Nullable<decimal> OpeningBalanceCredit { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
