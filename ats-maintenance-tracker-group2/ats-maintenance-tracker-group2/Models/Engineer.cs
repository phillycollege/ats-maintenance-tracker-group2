using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Engineer: Staff
    {

        // Navigation Property
        public ICollection<Job> Jobs { get; set; }
        public Shift Shift { get; set; }


        public void CompleteJob()
        {
            Console.WriteLine($"Completing job: ");
        }


    }
}