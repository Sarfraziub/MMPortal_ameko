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
    
    public partial class UserInterface
    {
        public int InterfaceId { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceDescription { get; set; }
        public Nullable<int> ParentInterfaceId { get; set; }
        public string InterfaceType { get; set; }
        public string InterfaceSubType { get; set; }
        public string InterfaceURL { get; set; }
        public Nullable<int> SequenceNo { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}