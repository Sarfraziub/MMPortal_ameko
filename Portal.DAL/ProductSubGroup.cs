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
    
    public partial class ProductSubGroup
    {
        public int ProductSubGroupId { get; set; }
        public string ProductSubGroupName { get; set; }
        public string ProductSubGroupCode { get; set; }
        public Nullable<int> ProductMainGroupId { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
