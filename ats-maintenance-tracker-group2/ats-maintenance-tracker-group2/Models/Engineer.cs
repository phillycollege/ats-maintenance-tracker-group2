using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Engineer: Staff
    {
        // add job parameter
        public void CompleteJob()
        {
            Console.WriteLine($"Completing job: ");
        }

        public string ShiftID { get; set; }
        [ForeignKey(nameof( ShiftID))]
        public Shift shift { get; set; }
    }
}