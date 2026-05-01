using ats_maintenance_tracker_group2.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers
{
    public class HomeController : Controller
    {
        private ATSDBContext context = new ATSDBContext();
        public ActionResult Index()
        {
            var allWindFarms = context.WindFarms.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}