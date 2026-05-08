using ats_maintenance_tracker_group2.Models;
using ats_maintenance_tracker_group2.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers {
    public class JobsController : Controller {
        // database connection context
        private ATSDBContext db = new ATSDBContext();

        // GET: Jobs
        public ActionResult Index() {
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());

                if (staff.EmploymentRole == "CallHandler" || staff.EmploymentRole == "Manager") {
                    // can view all jobs
                    var jobs = db.Jobs
                        .Include(j => j.Staff)
                        .Include(j => j.WindFarm)
                        .Include(j => j.Turbine)
                        .ToList();

                    AllJobsPageValues allJobsPageValues = new AllJobsPageValues() {
                        staff = staff, jobs = jobs
                    };

                    return View(allJobsPageValues);
                } else {
                    // engineer
                    // can view ALL THEIR OWN jobs
                    var jobs = db.Jobs
                        .Include(j => j.Staff)
                        .Include(j => j.WindFarm)
                        .Include(j => j.Turbine)
                        .Where(j => j.StaffID == staff.StaffID)
                        .ToList();

                    AllJobsPageValues allJobsPageValues = new AllJobsPageValues() {
                        staff = staff,
                        jobs = jobs
                    };

                    return View(allJobsPageValues);
                }
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Jobs/Details/1
        public ActionResult Details(int? id) {
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Job job = db.Jobs
                    .Include(j => j.Staff)
                    .Include(j => j.WindFarm)
                    .Include(j => j.Turbine)
                    .ToList().Find(j => j.JobID == id);

                if (job == null) {
                    return HttpNotFound();
                }

                JobDetailsPageValues jobDetailsPageValues = new JobDetailsPageValues() {
                    job = job,
                    staff = staff
                };
                return View(jobDetailsPageValues);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = db.Users.Find(User.Identity.GetUserId());
            var job = db.Jobs
                .Include(j => j.Staff)
                .FirstOrDefault(j => j.JobID == id);

            if (job == null) {
                return HttpNotFound();
            }

            // ✅ Restrict engineers to their own jobs only
            if (staff.EmploymentRole == "Engineer" && job.StaffID != staff.StaffID) {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(job);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job) {
            var existingJob = db.Jobs.Find(job.JobID);

            if (existingJob == null) {
                return HttpNotFound();
            }

            var staff = db.Users.Find(User.Identity.GetUserId());

            // ✅ Prevent editing other engineers' jobs
            if (staff.EmploymentRole == "Engineer" && existingJob.StaffID != staff.StaffID) {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid) {
                // Update allowed fields only
                existingJob.FaultDescription = job.FaultDescription;
                existingJob.MainGeneratorServiced = job.MainGeneratorServiced;
                existingJob.GearboxServiced = job.GearboxServiced;
                existingJob.YawMotorServiced = job.YawMotorServiced;
                existingJob.InternalPassengerLiftServiced = job.InternalPassengerLiftServiced;
                existingJob.JobCompleteStatus = job.JobCompleteStatus;

                // ✅ If completed → unassign engineer
                if (job.JobCompleteStatus == "Completed") {
                    existingJob.StaffID = null;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }


    }
}