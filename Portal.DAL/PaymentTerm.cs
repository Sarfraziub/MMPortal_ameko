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
    
    public partial class PaymentTerm
    {
        public int PaymentTermId { get; set; }
        public string PaymentTermDesc { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
