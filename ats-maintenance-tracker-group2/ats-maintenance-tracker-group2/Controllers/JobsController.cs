using ats_maintenance_tracker_group2.Models;
using ats_maintenance_tracker_group2.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        // AJAX: Get turbines for selected wind farm
        [HttpGet]
        public JsonResult GetTurbines(string farmId) {
            // gets all turbines in a particular windfarm using the farmId
            var turbines = db.Turbines
                .Where(t => t.FarmID == farmId)
                .Where(t => t.OperationalStatus == "1 Operational")
                .Select(t => new {
                    Value = t.TurbineID,
                    Text = t.TurbineID
                })
                .ToList();

            return Json(turbines, JsonRequestBehavior.AllowGet);
        }

        // GET: Jobs/CreateFaultJob
        public ActionResult CreateFaultJob () {
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());

                if (staff.EmploymentRole == "CallHandler") {
                    var engineer = db.Users.Where(s => s.EmploymentRole == "Engineer").ToList().First();

                    CreateFaultJobViewModel viewModel = new CreateFaultJobViewModel {
                        WindFarms = db.WindFarms
                            .Select(w => new SelectListItem {
                                Value = w.FarmID,
                                Text = w.FarmName
                            }).ToList(),
                        Turbines = new List<SelectListItem>()
                    };

                    return View(viewModel);
                } else {
                    return RedirectToAction("Index", "Home");
                }
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFaultJob(CreateFaultJobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // repopulate dropdowns
                model.WindFarms = db.WindFarms.Select(w => new SelectListItem
                {
                    Value = w.FarmID.ToString(),
                    Text = w.FarmName
                }).ToList();

                model.Turbines = db.Turbines.Select(t => new SelectListItem
                {
                    Value = t.TurbineID.ToString(),
                    Text = t.TurbineID
                }).ToList();

                return View(model);
            }

            // Get turbine from selected turbine id
            var turbine = db.Turbines.Find(model.SelectedTurbineId);

            if (turbine == null)
            {
                ModelState.AddModelError("", "Turbine not found");
                return View(model);
            }

            // Call assignment method
            AssignedEngineerShift assignedEngineerShift =
                new AssignEngineer().Assign();

            // Create job
            Job job = new Job()
            {
                TurbineID = turbine.TurbineID,

                FarmID = model.SelectedWindFarmId,

                FaultDescription = model.FaultDescription,

                JobType = "Fault",

                JobCompleteStatus = "Awaiting Engineer",

                // Engineer chosen from assignment method
                StaffID = assignedEngineerShift.Engineer.StaffID,

                // Shift chosen from assignment method
                JobDate = assignedEngineerShift.ShiftSession.jobTime,
                JobTime = assignedEngineerShift.ShiftSession.shiftTime,

                // defaults
                MainGeneratorServiced = false,
                GearboxServiced = false,
                YawMotorServiced = false,
                InternalPassengerLiftServiced = false
            };

            db.Jobs.Add(job);
            ViewBag.Result = $"Fault Job created";
            db.SaveChanges();

            return RedirectToAction("Index");
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

        public ActionResult UpdateStatus(int? id) {
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
                UpdateJobStatusViewModel updateJobStatusViewModel = new UpdateJobStatusViewModel() {
                    JobID = job.JobID,
                    FarmName = job.WindFarm.FarmName,
                    TurbineId = job.TurbineID,
                    JobType = job.JobType,
                    JobCompleteStatus = job.JobCompleteStatus
                };
                return View(updateJobStatusViewModel);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(UpdateJobStatusViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // Find the job in the database
            var job = db.Jobs.Find(model.JobID);

            if (job == null) {
                return HttpNotFound();
            }

            // Update the status and save changes
            job.JobCompleteStatus = model.JobCompleteStatus;
            db.SaveChanges();

            return RedirectToAction("Index");
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