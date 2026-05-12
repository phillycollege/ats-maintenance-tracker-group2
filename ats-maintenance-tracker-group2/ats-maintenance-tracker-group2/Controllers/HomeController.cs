using ats_maintenance_tracker_group2.Models;
using ats_maintenance_tracker_group2.Utilities;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers {
    public class HomeController : Controller {

        private ATSDBContext context = new ATSDBContext();
        public ActionResult Index() {
            if (Request.IsAuthenticated) {
                var staff = context.Users.Find(User.Identity.GetUserId());

                if (staff.EmploymentRole == "Engineer") {
                    List<Job> engineerJobs = context.Jobs
                        .Where(j => j.StaffID == staff.StaffID)
                        .Where(j => j.JobCompleteStatus == "Awaiting Engineer")
                        .Include(j => j.WindFarm)
                        .ToList();
                    var homePageValues = new HomePageValues() {
                        staff = staff, incompleteJobs = engineerJobs
                    };

                    return View(homePageValues);
                } else {
                    List<Job> emptyJobs = new List<Job>();
                    var homePageValues = new HomePageValues() {
                        staff = staff, incompleteJobs = emptyJobs
                    };
                    return View(homePageValues);
                }
            } else {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}