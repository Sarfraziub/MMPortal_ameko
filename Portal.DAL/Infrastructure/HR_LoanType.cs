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
    
    public partial class HR_LoanType
    {
        public int LoanTypeId { get; set; }
        public string LoanTypeName { get; set; }
        public Nullable<decimal> LoanInterestRate { get; set; }
        public string InterestCalcOn { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
