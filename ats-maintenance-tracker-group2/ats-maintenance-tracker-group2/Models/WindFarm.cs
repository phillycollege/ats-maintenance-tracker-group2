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
        public string farmID { get; set; }
        public string farmName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
    }
}