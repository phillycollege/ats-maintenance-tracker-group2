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
        public string JobID { get; set; }
        public DateTime JobDate { get; set; }
        public DateTime JobTime { get; set; }
        public string JobType { get; set; }
        public string FaultDescription { get; set; }
        public bool MainGeneratorServiced { get; set; }
        public bool GearboxServiced { get; set; }
        public bool YawMotorServiced { get; set; }
        public bool InternalPassengerLiftServiced { get; set; }
        public string JobCompleteStatus { get; set; }

        //Navigational Properties
        public string FarmID { get; set; }
        [ForeignKey(nameof(FarmID))]
        public WindFarm windfarm { get; set; }

        public string TurbineID { get; set; }
        [ForeignKey(nameof(TurbineID))]
        public Turbine turbine { get; set; }

        public string StaffID { get; set; }
        [ForeignKey(nameof(StaffID))]
        public Staff staff { get; set; }

    }
}