using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Turbine
    {
        [Key]
        public string TurbineID { get; set; }
        public string TurbineMake { get; set; }
        public string TurbineModel { get; set; }
        public int RuntimeHours { get; set; }
        public bool IsHighWinds { get; set; }
        public string OperationalStatus { get; set; }

        // Navigational Properties
        [ForeignKey(nameof(FarmID))]
        public string FarmID { get; set; }
        public WindFarm farm { get; set; }

        //Logic
        public bool RequiresService() 
        {
            return RuntimeHours >= 2000 && OperationalStatus != "ServiceScheduled";
        }

        public void MarkServiceScheduled()
        {
            OperationalStatus = "ServiceScheduled";
        }
    }
}