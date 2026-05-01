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
        //Primary Key
        [Key]
        public string JobID { get; set; }
        public DateTime JobDate { get; set; }
        public string JobTime { get; set; } //Early 07:00 - 14:00, Late 14:00 - 21:00
        public string JobType { get; set; } //Service or Fault Job
        public string FaultDescription { get; set; }
        public bool MainGeneratorServiced { get; set; }
        public bool GearboxServiced { get; set; }
        public bool YawMotorServiced { get; set; }
        public bool InternalPassengerLiftServiced { get; set; }
        public string JobCompleteStatus { get; set; }

        //Navigational Properties & Foreign Keys
        public string FarmID { get; set; }
        [ForeignKey(nameof(FarmID))]
        public WindFarm windfarm { get; set; }

        public string TurbineID { get; set; }
        [ForeignKey(nameof(TurbineID))]
        public Turbine turbine { get; set; }

        public string StaffID { get; set; }
        [ForeignKey(nameof(StaffID))]
        public Staff staff { get; set; }

        //Logic
        public Engineer AssignEngineer(IEnumerable<Engineer> engineers)
        {
            foreach (var engineer in engineers)
            {
                var shift = engineer.Shift;

                // Shift type must match the job ("Early" / "Late")
                if (!string.Equals(shift.ShiftType, JobTime,
                    StringComparison.OrdinalIgnoreCase))
                    continue;

                // Engineer must be working on JobDate
                bool worksThatDay =
                    (JobDate.DayOfWeek == DayOfWeek.Monday && shift.Mon) ||
                    (JobDate.DayOfWeek == DayOfWeek.Tuesday && shift.Tue) ||
                    (JobDate.DayOfWeek == DayOfWeek.Wednesday && shift.Wed) ||
                    (JobDate.DayOfWeek == DayOfWeek.Thursday && shift.Thu) ||
                    (JobDate.DayOfWeek == DayOfWeek.Friday && shift.Fri) ||
                    (JobDate.DayOfWeek == DayOfWeek.Saturday && shift.Sat) ||
                    (JobDate.DayOfWeek == DayOfWeek.Sunday && shift.Sun);

                if (!worksThatDay)
                    continue;

                // Engineer must NOT already have a job on this date
                bool alreadyHasJob =
                    engineer.Jobs.Any(j => j.JobDate.Date == JobDate.Date);

                if (alreadyHasJob)
                    continue;

                //Engineer is valid – assign
                StaffID = engineer.StaffID;
                return engineer;
            }

            return null; // No available engineer

        }



        public static Job CreateServiceJob(Turbine turbine, Engineer engineer, string jobTime, DateTime jobDate)
        {

            return new Job
            {
                JobID = Guid.NewGuid().ToString(),
                JobDate = jobDate,
                JobTime = jobTime,
                JobType = "Service",
                TurbineID = turbine.TurbineID,
                JobCompleteStatus = "Scheduled"
            };


        }
    }
}