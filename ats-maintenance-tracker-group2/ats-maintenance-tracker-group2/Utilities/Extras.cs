using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

//using ats_maintenance_tracker_group2.Data;
using ats_maintenance_tracker_group2.Models;


namespace ats_maintenance_tracker_group2.Utilities
{
    // Extra functions that are not related to the main functionality of the application but can be useful for future development
    public class Extras {
        //create a  method to create a job when the turbine hours arises 2000 hours

        //code to create a job

        public static void CreateServicedJob(
            ATSDBContext context,
            Turbine turbine)
        {
            // Exit if not eligible
            if (turbine == null || turbine.RuntimeHours < 2000)
                return;

            // Prevent duplicate scheduled services
            bool serviceExists = context.Jobs.Any(j =>
                j.TurbineID == turbine.TurbineID &&
                j.JobType == "Scheduled Service" &&
                j.JobCompleteStatus != "Complete"
            );

            if (serviceExists)
                return;

            // Update turbine status , TODO: first assign engineer
            turbine.OperationalStatus = "Requires Service";

            AssignedEngineerShift assignedEngineerShift = new AssignEngineer().Assign();
            Job job = new Job() {
                TurbineID = turbine.TurbineID,
                JobType = "Service",
                JobCompleteStatus = "Awaiting Engineer",
                JobDate = assignedEngineerShift.ShiftSession.jobTime,
                StaffID = assignedEngineerShift.Engineer.StaffID,
                MainGeneratorServiced = false,
                GearboxServiced = false,
                YawMotorServiced = false,
                InternalPassengerLiftServiced = false,
                FarmID = turbine.FarmID,
                JobTime = assignedEngineerShift.ShiftSession.shiftTime
            };

            context.Jobs.Add(job);
            context.SaveChanges();
        }
    }
}