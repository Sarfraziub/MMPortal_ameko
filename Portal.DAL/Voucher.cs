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
    
    public partial class Voucher
    {
        public long VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<int> VoucherNoSequence { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string VoucherType { get; set; }
        public string VoucherMode { get; set; }
        public Nullable<decimal> VoucherAmount { get; set; }
        public Nullable<int> PayeeSLTypeId { get; set; }
        public Nullable<int> BookId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> FinYearId { get; set; }
        public Nullable<long> ContraVoucherId { get; set; }
        public string ContraVoucherNo { get; set; }
        public Nullable<int> ContraBookId { get; set; }
        public Nullable<int> ContraCompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string VoucherStatus { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
        public string CancelReason { get; set; }
        public Nullable<long> RefInvoiceId { get; set; }
    }
}
