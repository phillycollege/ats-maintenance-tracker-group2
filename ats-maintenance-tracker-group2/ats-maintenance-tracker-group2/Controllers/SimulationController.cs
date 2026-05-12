using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers
{
    public class SimulationController : Controller
    {
        // GET: Simulation
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(int hours, string turbine)
        {
            ViewBag.Result = $"Simulation updated for {turbine} with {hours} hours.";
            return View();
        }
    }
}