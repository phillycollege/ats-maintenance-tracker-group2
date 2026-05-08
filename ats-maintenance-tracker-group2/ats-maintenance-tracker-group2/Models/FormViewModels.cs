using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models {
    public class UpdateJobStatusViewModel {
        public int JobID { get; set; }
        public string FarmName { get; set; }
        public string JobType { get; set; }
        public string TurbineId { get; set; }
        [Required]
        public string JobCompleteStatus { get; set; } // Awaiting Engineer, Complete
    }
}