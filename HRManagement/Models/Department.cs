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
    
    public partial class Department
    {
        public Department()
        {
            this.Announcements = new HashSet<Announcement>();
            this.Designations = new HashSet<Designation>();
            this.Employees = new HashSet<Employee>();
        }
    
        public decimal DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
