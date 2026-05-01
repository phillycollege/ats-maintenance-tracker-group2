using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Staff
    {
        [Key]
        public string StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkMobileNumber { get; set; }
        public string HomeMobileNumber { get; set; }
        public string WorkEmailAddress { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public decimal Salary { get; set; }
        public string EmploymentRole { get; set; }
    }
}