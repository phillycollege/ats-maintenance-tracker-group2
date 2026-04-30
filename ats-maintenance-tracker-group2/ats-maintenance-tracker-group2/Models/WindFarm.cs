using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class WindFarm
    {
        [Key]
        public string FarmID { get; set; }
        public string FarmName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Region { get; set; }
        public double Coords { get; set; }
    }
}