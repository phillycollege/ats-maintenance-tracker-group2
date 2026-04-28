using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class CallHandler: Staff
    {
        // add job parameter
        public void LogFault()
        {
            Console.WriteLine($"Logging fault for job: ");
        }
    }
}