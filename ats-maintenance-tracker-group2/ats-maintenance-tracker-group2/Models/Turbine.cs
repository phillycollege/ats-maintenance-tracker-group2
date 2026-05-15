
// Import basic system functionality
using System;

// Allows use of collections like List<>
using System.Collections.Generic;


// Provides validation attributes like [Key]
using System.ComponentModel.DataAnnotations;

// Provides database mapping attributes like [ForeignKey]
using System.ComponentModel.DataAnnotations.Schema;

// Allows LINQ queries
using System.Linq;

// Web-related functionality
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{

    // Turbine class represents a turbine entity in the database
    public class Turbine
    {
        [Key]
        public string TurbineID { get; set; }
        public string TurbineMake { get; set; }
        public string TurbineModel { get; set; }
        public int RuntimeHours { get; set; }// update to 0 when serviced
        public bool IsHighWinds { get; set; }// Indicates if the turbine is operating in high wind conditions
                                             // true = high winds, false = normal conditions
        public string OperationalStatus { get; set; } // Active, Needs Service, Fault
        public string Coordinates { get; set; } // stores latitude/longitude

        // Navigational Properties

        // Foreign key linking this turbine to a WindFarm
        [ForeignKey("WindFarm")]
        public string FarmID { get; set; }

        // Navigation property (relationship)
        // Allows access to the related WindFarm object
        // Example: turbine.WindFarm.FarmName
        public WindFarm WindFarm { get; set; }
    }
}