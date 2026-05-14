using ats_maintenance_tracker_group2.Models;
using ats_maintenance_tracker_group2.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers {
    public class ShiftsController : Controller {
        private ATSDBContext db = new ATSDBContext();

        // ✅ GET: Shifts
        public ActionResult Index() {
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());

                // ✅ Only Engineers can view shifts
                if (staff.EmploymentRole != "Engineer") {
                    return RedirectToAction("Index", "Home");
                }

                var shifts = db.Shifts
                    .Include(s => s.Staff)
                    .Where(s => s.Id == staff.Id)
                    .ToList();

                return View(shifts);
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult ShiftView () {
            List<Shift> shifts = db.Shifts.Include(s => s.Staff).ToList();
            List<Job> jobs = db.Jobs.ToList();

            DailyStaffScheduleViewModel dailyStaffScheduleViewModel = new DailyStaffScheduleViewModel();
            
            DateTime currentDay = DateTime.Now;

            for (int i = 0; i < 7; i++) {
                DailyStaffScheduleItem dailyStaffScheduleItem = new DailyStaffScheduleItem();
                DateTime currentDate = currentDay.AddDays(i);

                dailyStaffScheduleItem.Date = currentDate;

                foreach (var shift in shifts) {
                    if (currentDate.DayOfWeek == DayOfWeek.Monday && shift.Mon) {
                        // monday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }

                    } else if (currentDate.DayOfWeek == DayOfWeek.Tuesday && shift.Tue) {
                        // tuesday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }

                    } else if (currentDate.DayOfWeek == DayOfWeek.Wednesday && shift.Wed) {
                        // wednesday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }

                    } else if (currentDate.DayOfWeek == DayOfWeek.Thursday && shift.Thu) {
                        // thursday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }

                    } else if (currentDate.DayOfWeek == DayOfWeek.Friday && shift.Fri) {
                        // friday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }

                    } else if (currentDate.DayOfWeek == DayOfWeek.Saturday && shift.Sat) {
                        // saturday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }

                    } else if (currentDate.DayOfWeek == DayOfWeek.Sunday && shift.Sun) {
                        // sunday
                        ShiftStaff shiftStaff = new ShiftStaff() {
                            isAssignedJob = (jobs.FindAll(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date).Count > 0),
                            staff = shift.Staff,
                            assignedJob = jobs.Find(j => j.StaffID == shift.Staff.StaffID && j.JobDate.Date == currentDate.Date)
                        };
                        if (shift.ShiftType == "Early") {
                            dailyStaffScheduleItem.EarlyShiftStaffs.Add(shiftStaff);
                        } else {
                            dailyStaffScheduleItem.LateShiftStaffs.Add(shiftStaff);
                        }
                    }
                }
                
                dailyStaffScheduleViewModel.DailyStaffScheduleItems.Add(dailyStaffScheduleItem);
            }

            return View(dailyStaffScheduleViewModel);
        }

        // ✅ GET: Shifts/Details/1
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = db.Users.Find(User.Identity.GetUserId());
            var shift = db.Shifts
                .Include(s => s.Staff)
                .FirstOrDefault(s => s.ShiftRecordID == id.Value);

            if (shift == null) {
                return HttpNotFound();
            }

            if (shift.Id != staff.Id) {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(shift);
        }
    }
}

