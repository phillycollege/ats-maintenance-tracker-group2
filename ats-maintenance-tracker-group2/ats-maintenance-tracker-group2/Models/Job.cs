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
        public int JobID { get; set; }
        public DateTime JobDate { get; set; } // this is to match up with what day the engineer's shift (mon - sun could be any)
        public string JobTime { get; set; } // Early or Late


        public string JobType { get; set; }
        public string FaultDescription { get; set; }
        public bool MainGeneratorServiced { get; set; }
        public bool GearboxServiced { get; set; }
        public bool YawMotorServiced { get; set; }
        public bool InternalPassengerLiftServiced { get; set; }
        public string JobCompleteStatus { get; set; }


        //Navigational Properties
        [ForeignKey("WindFarm")]
        public string FarmID { get; set; }
        public WindFarm WindFarm { get; set; }

        [ForeignKey("Turbine")]
        public string TurbineID { get; set; }
        public Turbine Turbine { get; set; }

        [ForeignKey("Staff")]
        public string StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}