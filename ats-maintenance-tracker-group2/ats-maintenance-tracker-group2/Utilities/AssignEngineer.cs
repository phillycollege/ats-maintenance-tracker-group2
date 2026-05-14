using ats_maintenance_tracker_group2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Utilities {
    public class ShiftSession {
        public DateTime jobTime;
        public string shiftTime; // Early or Late
    }

    public class AssignedEngineerShift {
        public ShiftSession ShiftSession { get; set; }
        public Engineer Engineer { get; set; }
    }

    public class AssignEngineer {
        public ATSDBContext db = new ATSDBContext();

        public ShiftSession getNextShiftSession (ShiftSession shiftSession) {
            ShiftSession currentShiftSession = shiftSession;
            if (currentShiftSession.shiftTime == "Early") {
                // if current shift session is early
                // the next shift session is the same day but a late shift
                currentShiftSession.shiftTime = "Late";
            } else {
                // if the current shift session is late
                // the next shift session is the next day but an early shift
                // set time to 9am
                currentShiftSession.shiftTime = "Early";
                currentShiftSession.jobTime = new DateTime(
                    currentShiftSession.jobTime.Year, currentShiftSession.jobTime.Month, currentShiftSession.jobTime.Day,
                    9, 0, 0
                ).AddDays(1);
            }
            return currentShiftSession;
        }

        public Engineer findEngineerUsingShiftSession (ShiftSession shiftSession) {
            // get day of the week
            DayOfWeek dayOfTheWeek = shiftSession.jobTime.DayOfWeek;

            // get all shifts that match the criteria of the current shift session
            List<Shift> shifts = db.Shifts
                .Include(s => s.Staff)
                .Where(shift =>
                    shift.ShiftType == shiftSession.shiftTime &&
                    (
                        (dayOfTheWeek == DayOfWeek.Monday && shift.Mon) ||
                        (dayOfTheWeek == DayOfWeek.Tuesday && shift.Tue) ||
                        (dayOfTheWeek == DayOfWeek.Wednesday && shift.Wed) ||
                        (dayOfTheWeek == DayOfWeek.Thursday && shift.Thu) ||
                        (dayOfTheWeek == DayOfWeek.Friday && shift.Fri) ||
                        (dayOfTheWeek == DayOfWeek.Saturday && shift.Sat) ||
                        (dayOfTheWeek == DayOfWeek.Sunday && shift.Sun)
                    )
                ).ToList();

            // loop through each shift and find engineers that are not assigned to a job on that day
            // and when an engineer who isnt assigned is found immediately assign them
            foreach (var engineerShift in shifts) {
                // find if a job has been assigned to any engineer for this shift
                List<Job> jobs = db.Jobs.Include(j => j.Staff)
                    .Where(j => j.JobDate == shiftSession.jobTime && j.JobTime == shiftSession.shiftTime)
                    .Where(j => j.Staff.StaffID == engineerShift.Staff.StaffID)
                    .ToList();

                if (jobs.Count > 0) {
                    // continue to next iteration
                    continue;
                } else {
                    // assign them
                    return new Engineer() {
                        StaffID = engineerShift.Staff.StaffID,
                        FirstName = engineerShift.Staff.FirstName,
                        LastName = engineerShift.Staff.LastName,
                        WorkMobileNumber = engineerShift.Staff.WorkMobileNumber,
                        HomeMobileNumber = engineerShift.Staff.HomeMobileNumber,
                        WorkEmailAddress = engineerShift.Staff.WorkEmailAddress,
                        Address1 = engineerShift.Staff.Address1,
                        City = engineerShift.Staff.City,
                        Postcode = engineerShift.Staff.Postcode,
                        Salary = engineerShift.Staff.Salary,
                        EmploymentRole = engineerShift.Staff.EmploymentRole
                    };
                }
            }

            return null;
        }

        public AssignedEngineerShift Assign () {
            // Get the fault datetime and shift type
            // shift session will be a looped value
            ShiftSession shiftSession = new ShiftSession() {
                jobTime = DateTime.Now,
                shiftTime = DateTime.Now.Hour > 12 ? "Early" : "Late"
            };
            Engineer engineerFound = null;

            // begin loop to find engineer
            // this loops through each shift session
            while (engineerFound == null) {
                if (DateTime.Now.Date == shiftSession.jobTime.Date) {
                    // if working date is today
                    if (shiftSession.jobTime.Hour > 12) {
                        // the shift was found in the afternoon so we need to move to the next shift session
                        shiftSession = getNextShiftSession(shiftSession);
                        continue;
                    } else {
                        // move to next shift session
                        shiftSession = getNextShiftSession(shiftSession);

                        // the shift was found in the morning so we need to find an engineer on a late shift on the same day
                        // get day of the week
                        DayOfWeek dayOfTheWeek = shiftSession.jobTime.DayOfWeek;

                        // find what engineers are free on this day of the week and have a free shift that day (late shift)
                        engineerFound = findEngineerUsingShiftSession(shiftSession);
                    }
                } else {
                    // if working date is NOT today
                    var maybeFoundEngineer = findEngineerUsingShiftSession(shiftSession);
                    if (maybeFoundEngineer == null) {
                        shiftSession = getNextShiftSession(shiftSession);
                        continue;
                    } else {
                        engineerFound = findEngineerUsingShiftSession(shiftSession);
                    }
                }
            }

            AssignedEngineerShift assignedEngineerShift = new AssignedEngineerShift() {
                Engineer = engineerFound,
                ShiftSession = shiftSession
            };

            return assignedEngineerShift;
        }
    }
}