using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ats_maintenance_tracker_group2.Models
{
    public class Job
    {
        [Key]
        public string jobID { get; set; }
        public DateTime jobDate { get; set; }
        public DateTime jobTime { get; set; }

        //Navigational Properties
        public string farmID { get; set; }
        [ForeignKey(nameof(farmID))]
        public WindFarm windfarm { get; set; }

        public string turbineID { get; set; }
        [ForeignKey(nameof(turbineID))]
        public Turbine turbine { get; set; }

        public string staffID { get; set; }
        [ForeignKey(nameof(staffID))]
        public Staff staff { get; set; }

        public string jobType { get; set; }
        public string faultDescription { get; set; }
        public bool mainGeneratorServiced { get; set; }
        public bool gearboxServiced { get; set; }
        public bool yawMotorServiced { get; set; }
        public bool internalPassengerLiftServiced { get; set; }
        public string jobCompleteStatus { get; set; }


    }
}