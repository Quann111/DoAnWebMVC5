//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cours
    {
        public Cours()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<int> EnrollmentCount { get; set; }
    
        public virtual ICollection<Payment> Payments { get; set; }
    }
}