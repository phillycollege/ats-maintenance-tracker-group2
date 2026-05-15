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
using System.Web.UI;

namespace ats_maintenance_tracker_group2.Controllers {
    public class JobsController : Controller {
        // database connection context
        private ATSDBContext db = new ATSDBContext();

        // GET: Jobs
        public ActionResult Index() {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());

                if (staff.EmploymentRole == "CallHandler" || staff.EmploymentRole == "Manager") {
                    // call handler and manager
                    // can view all jobs
                    var jobs = db.Jobs
                        .Include(j => j.Staff)
                        .Include(j => j.WindFarm)
                        .Include(j => j.Turbine)
                        .OrderByDescending(j => j.JobID)
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
                        .OrderByDescending(j => j.JobID)
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
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not call handlers tp the home page
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
        public ActionResult CreateFaultJob(CreateFaultJobViewModel model) {
            if (!ModelState.IsValid) {
                // repopulate dropdowns
                model.WindFarms = db.WindFarms.Select(w => new SelectListItem {
                    Value = w.FarmID.ToString(),
                    Text = w.FarmName
                }).ToList();

                model.Turbines = db.Turbines.Select(t => new SelectListItem {
                    Value = t.TurbineID.ToString(),
                    Text = t.TurbineID
                }).ToList();

                return View(model);
            }

            // Get turbine from selected turbine id
            var turbine = db.Turbines.Find(model.SelectedTurbineId);

            if (turbine == null) {
                ModelState.AddModelError("", "Turbine not found");
                return View(model);
            }

            // Call assignment method
            AssignedEngineerShift assignedEngineerShift = new AssignEngineer().Assign();

            // Redifined fault description based on components that were marked as faulty
            List<string> componentsToService = new List<string>();

            if (model.MainGeneratorServiced) componentsToService.Add("Main Generator");
            if (model.InternalPassengerLiftServiced) componentsToService.Add("Internal Passenger Lift");
            if (model.YawMotorServiced) componentsToService.Add("Yaw Motor");
            if (model.GearboxServiced) componentsToService.Add("Gear Box");

            string componentFaultDescription = "The following components are faulty: " + string.Join(", ", componentsToService) + ".";
            string fullFaultDescription = model.FaultDescription + Environment.NewLine + componentFaultDescription;

            // Create job
            Job job = new Job() {
                TurbineID = turbine.TurbineID,
                FarmID = model.SelectedWindFarmId,
                FaultDescription = fullFaultDescription,
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
            // check if user is logged in
            // redirect users who are not logged in to the login page
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // get the job to be updated
                Job job = db.Jobs
                    .Include(j => j.Staff)
                    .Include(j => j.WindFarm)
                    .Include(j => j.Turbine)
                    .ToList().Find(j => j.JobID == id);

                if (job == null) {
                    return HttpNotFound();
                }

                // pass the update job status form view model to the frontend
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

            // Update the components that may have been serviced
            job.MainGeneratorServiced = model.MainGeneratorServiced;
            job.InternalPassengerLiftServiced = model.InternalPassengerLiftServiced;
            job.GearboxServiced = model.GearboxServiced;
            job.YawMotorServiced = model.YawMotorServiced;

            // set turbine runtime hours to 0
            var turbine = db.Turbines.Where(t => t.TurbineID == model.TurbineId).ToList().FirstOrDefault();
            turbine.RuntimeHours = 0;

            // Update the status and save changes
            job.JobCompleteStatus = model.JobCompleteStatus;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Jobs/Delete/5
        public ActionResult Delete(int id) {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect engineers to the home page
            if (Request.IsAuthenticated) {
                var staff = db.Users.Find(User.Identity.GetUserId());
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // redirect users who are engineers
                if (staff.EmploymentRole != "Engineer") {
                    if (id == null) {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Job job = db.Jobs.Include(j => j.WindFarm)
                        .Include(j => j.Staff)
                        .Where(j => j.JobID == id)
                        .ToList()
                        .FirstOrDefault();

                    if (job == null) {
                        return HttpNotFound();
                    }
                    return View(job);
                } else {
                    return RedirectToAction("Index", "Jobs");
                }
            } else {
                return RedirectToAction("Login", "Account");
            }

        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}