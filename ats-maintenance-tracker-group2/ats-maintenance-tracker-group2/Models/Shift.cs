using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Shift
    {
        // this applies for every week in the calendar year (repeated schedule)
        [Key]
        public int ShiftRecordID { get; set; }
        public string ShiftType { get; set; } // Early or Late
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }

        //Navigational Property
        [ForeignKey("Staff")]
        public string StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}