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

            context.Jobs.Add(new Job
            {
                TurbineID = turbine.TurbineID,
                JobType = "Scheduled Service",
                JobCompleteStatus = "Pending",
                //JobDate = 
            });
            context.SaveChanges();
        }


        // Assign engineer
        //TODO: Implement a more robust engineer assignment method (Ciaran, Philip)

        // call the method AssignEngineer() in the CreateServicedJob method after creating the job




        //var engineer = context.Staff
        //            .Where(s => s.EmploymentRole == "Engineer")
        //            .OrderBy(s => s.StaffID)
        //            .FirstOrDefault();

        //        if (engineer == null)
        //            return;

        //        Job job = new Job
        //        {
        //            JobDate = DateTime.Today.AddDays(1),
        //            JobTime = "Early",
        //            JobType = "Scheduled Service",

        //            TurbineID = turbine.TurbineID,
        //            Turbine = turbine,

        //            FarmID = turbine.FarmID,
        //            WindFarm = turbine.WindFarm,

        //            StaffID = engineer.StaffID,
        //            Staff = engineer,

        //            MainGeneratorServiced = true,
        //            GearboxServiced = true,
        //            YawMotorServiced = true,
        //            InternalPassengerLiftServiced = true,



        //            JobCompleteStatus = "Awaiting Engineer"
        //        };

        //        context.Jobs.Add(job);
        //        context.SaveChanges();
        //    }
        
    }
}