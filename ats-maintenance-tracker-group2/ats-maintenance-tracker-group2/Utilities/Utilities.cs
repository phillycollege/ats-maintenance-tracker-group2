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

    public class DailyStaffScheduleViewModel {
        public List<DailyStaffScheduleItem> DailyStaffScheduleItems = new List<DailyStaffScheduleItem>();
    }

    public class ShiftStaff {
        public Staff staff;
        public bool isAssignedJob {  get; set; }
        public Job assignedJob { get; set; }
    }

    public class DailyStaffScheduleItem {
        public DateTime Date { get; set; }
        public List<ShiftStaff> EarlyShiftStaffs { get; set; } = new List<ShiftStaff>();
        public List<ShiftStaff> LateShiftStaffs { get; set; } = new List<ShiftStaff>();
    }
}