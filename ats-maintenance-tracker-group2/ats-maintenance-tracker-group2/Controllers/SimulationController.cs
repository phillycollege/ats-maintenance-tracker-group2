using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ats_maintenance_tracker_group2.Models;

namespace ats_maintenance_tracker_group2.Controllers
{


    public class SimulationController : Controller
    {

        private ATSDBContext db = new ATSDBContext();

        // GET: Simulation
        public ActionResult Index()
        {

            // WindFarm list
            var windFarms = db.WindFarms.ToList();

            ViewBag.WindFarms = windFarms.Select(w => new SelectListItem
            {
                Text = w.FarmName,   
                Value = w.FarmID     // key (WF001…)
            }).ToList();

            return View();

        }




        [HttpPost]
        public ActionResult Index(int hours, string windFarmId, string turbineId)
        {
            ViewBag.Result = $"Simulation updated for {turbineId} with {hours} hours.";

            // reload WindFarms
            var windFarms = db.WindFarms.ToList();
            ViewBag.WindFarms = windFarms.Select(w => new SelectListItem
            {
                Text = w.FarmName,
                Value = w.FarmID
            }).ToList();

            return View();
        }




        public JsonResult GetTurbines(string windFarmId)
        {
            var turbines = db.Turbines
                .Where(t => t.FarmID == windFarmId)
                .Select(t => new
                {
                    Id = t.TurbineID,
                    Name = t.TurbineID
                }).ToList();


            return Json(turbines, JsonRequestBehavior.AllowGet);

        }
    }
}