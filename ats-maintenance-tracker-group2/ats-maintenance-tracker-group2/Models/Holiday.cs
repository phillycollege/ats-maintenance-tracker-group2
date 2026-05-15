using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Holiday
    {
        //Primary Key
        public int HolidayID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]                                  //Start and End Dates for the Holiday
        [DataType(DataType.Date)]   
        public DateTime EndDate { get; set; }

        public string Status { get; set; }          //Approved or Not Approved

        public string Reason { get; set; }
        public DateTime DateRequested { get; set; }

        //Navigational Property
        [ForeignKey("Staff")]
        public string StaffID { get; set; }
        public Staff Staff { get; set; }


    }
}