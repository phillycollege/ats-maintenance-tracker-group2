using System; // Import basic system functionality (DateTime, etc.)
using System.Collections.Generic; // Allows use of collections like List<>
using System.Linq; // Allows LINQ queries
using System.ComponentModel.DataAnnotations; // Provides attributes like [Key]
using System.Web; // Web-related functionality
using System.ComponentModel.DataAnnotations.Schema; // Provides [ForeignKey]

namespace ats_maintenance_tracker_group2.Models
{
    // Job class represents a maintenance job in the database
    public class Job
    {
        //Primary Key
        [Key]
        public int JobID { get; set; } // primary key , unique identifier for each job
        public DateTime JobDate { get; set; }
        public string JobTime { get; set; } // Time slot of the job: Early 07:00 - 14:00, Late 14:00 - 21:00
        public string JobType { get; set; } // Service or Fault Job

        // Service checklist
        public string FaultDescription { get; set; } 
        public bool MainGeneratorServiced { get; set; }
        public bool GearboxServiced { get; set; }
        public bool YawMotorServiced { get; set; }
        public bool InternalPassengerLiftServiced { get; set; }

        public string JobCompleteStatus { get; set; } // Awaiting Engineer or Job Completed

        //Navigational Properties

        [ForeignKey("WindFarm")]  // Foreign key linking Job → WindFarm
        public string FarmID { get; set; }
        public WindFarm WindFarm { get; set; } // Navigation property to access related WindFarm


        [ForeignKey("Turbine")]  // Foreign key linking Job → Turbine
        public string TurbineID { get; set; }
        public Turbine Turbine { get; set; }

        [ForeignKey("Staff")] // Foreign key linking Job → Staff (engineer assigned)
        public string StaffID { get; set; }
        public Staff Staff { get; set; } //  if it is  null can be cause an error
    }
}