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
    
    public partial class PurchaseIndentProductDetail
    {
        public long IndentProductDetailId { get; set; }
        public Nullable<long> IndentId { get; set; }
        public Nullable<long> ProductId { get; set; }
        public string ProductShortDesc { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> ReceivedQuantity { get; set; }
        public Nullable<decimal> POQuantity { get; set; }
    }
}
