//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseStudentManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class user
    {
        public int id { get; set; }

        [DisplayName("First Name")]
        public string firstname { get; set; }

        [DisplayName("Last Name")]
        public string lastname { get; set; }

        [DisplayName("UserName")]
        public string username { get; set; }

        [DisplayName("Password")]
        public string password { get; set; }
    }
}