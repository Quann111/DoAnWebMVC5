//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payments
    {
        public int PaymentID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public decimal PaymentAmount { get; set; }
        public System.DateTime PaymentDate { get; set; }
    
        public virtual Courses Courses { get; set; }
        public virtual Users Users { get; set; }
    }
}