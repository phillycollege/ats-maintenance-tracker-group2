using System; // Import basic system functionality
using System.Collections.Generic; // Allows use of collections like List<>
using System.ComponentModel.DataAnnotations; // Provides validation attributes such as [Key]
using System.Linq; // Allows LINQ queries
using System.Web; // Web-related functionality

namespace ats_maintenance_tracker_group2.Models
{
    public class WindFarm{ // WindFarm class represents a wind farm entity in the database
        [Key]
        public string FarmID { get; set; } //primary key for WindFarm table
        public string FarmName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; } // Second line of the farm's address (optional)
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Region { get; set; } // Scotland, North Region

        // Navigational Properties E


        // A WindFarm can have many Turbines (one-to-many relationship)
        // This allows to access all turbines belonging to a wind farm

        //public ICollection<Turbine> Turbines { get; set; }

    }
}