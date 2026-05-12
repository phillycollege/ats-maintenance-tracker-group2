using ats_maintenance_tracker_group2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Utilities {
    public class AllJobsPageValues {
        public Staff staff;
        public List<Job> jobs;
    }
    public class JobDetailsPageValues {
        public Staff staff;
        public Job job;
    }

    public class HomePageValues {
        public Staff staff;
        public List<Job> incompleteJobs;
    }
}