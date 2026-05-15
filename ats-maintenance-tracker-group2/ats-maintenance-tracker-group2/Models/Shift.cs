
using System; // Import basic system functionality
using System.Collections.Generic; // Allows use of collections like List<>
using System.ComponentModel.DataAnnotations; // Provides attributes like [Key]
using System.ComponentModel.DataAnnotations.Schema; // Provides [ForeignKey]
using System.Linq; // Allows LINQ queries
using System.Web; // Web-related functionality


namespace ats_maintenance_tracker_group2.Models
{
    // Shift class represents a weekly repeating work schedule for a staff member
    public class Shift
    {
        // this applies for every week in the calendar year (repeated schedule)
        [Key]
        public int ShiftRecordID { get; set; }// primary key, Unique identifier for each shift record
        public string ShiftType { get; set; } //Early: 07:00 - 14:00, Late: 14:00-21:00

        // Boolean values representing which days this shift applies
        // true = staff works that day, false = does not work
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }

        //Navigational Property

        [ForeignKey("Staff")] // Foreign key linking Shift → Staff (who this schedule belongs to)
        public string Id { get; set; } // we named id instead StaffId to avoid mistakes

        // Navigation property to access the related Staff member, can be null if not loaded or not assigned
        public Staff Staff{get; set;}
    }
}