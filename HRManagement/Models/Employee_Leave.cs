//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee_Leave
    {
        public decimal LeaveId { get; set; }
        public string LeaveMessage { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string UserName { get; set; }
        public string LeaveStatus { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
