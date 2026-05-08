using ats_maintenance_tracker_group2.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers {
    public class HomeController : Controller {

        private ATSDBContext context = new ATSDBContext();
        public ActionResult Index() {
            if (Request.IsAuthenticated) {
                var staff = context.Users.Find(User.Identity.GetUserId());
                return View(staff);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}