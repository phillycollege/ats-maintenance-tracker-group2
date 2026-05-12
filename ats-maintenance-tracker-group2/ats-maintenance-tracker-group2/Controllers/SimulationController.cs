using ats_maintenance_tracker_group2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers
{


    public class SimulationController : Controller
    {

        private ATSDBContext db = new ATSDBContext();

        // GET: Simulation
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var staff = db.Users.Find(User.Identity.GetUserId());

                if (staff.EmploymentRole == "CallHandler")
                {
                    UpdateTurbineHoursViewModel viewModel = new UpdateTurbineHoursViewModel
                    {
                        WindFarms = db.WindFarms
                            .Select(w => new SelectListItem
                            {
                                Value = w.FarmID,
                                Text = w.FarmName
                            }).ToList(),
                        Turbines = new List<SelectListItem>()
                    };

                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
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




        public JsonResult GetTurbines(string farmId)
        {
            var turbines = db.Turbines
                .Where(t => t.FarmID == farmId)
                .Select(t => new
                {
                    Value = t.TurbineID,
                    Text = t.TurbineID
                }).ToList();


            return Json(turbines, JsonRequestBehavior.AllowGet);

        }
    }
}