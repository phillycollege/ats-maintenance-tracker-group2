using ats_maintenance_tracker_group2.Models;
using ats_maintenance_tracker_group2.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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
        //public ActionResult Index(int hours, string windFarmId, string turbineId)

        
        public ActionResult Index(UpdateTurbineHoursViewModel model)
        {
            var turbine = db.Turbines.Find(model.SelectedTurbineId);

            if (turbine != null)
            {
                //update run time hours in the turbine table
                turbine.RuntimeHours += model.Hours;

                //save changes to the database
                // important to save changes before calling the service job creator to ensure the updated hours are reflected in the database when checking for service eli

                db.SaveChanges();

                //Call your service job creator
                Extras.CreateServicedJob(db, turbine);// check for service


                ViewBag.Result = $"Forced update → {turbine.RuntimeHours}";
                ViewBag.Result = $"UPDATED → {turbine.RuntimeHours}";
            }
            ViewBag.Result = $"Simulation updated for {model.SelectedTurbineId} with {model.Hours} hours.";
            //ViewBag.Result = $"Simulation updated for {model.SelectedTurbineId} with {model.Hours} hours.";
            ViewBag.Result = $"Turbine: {model.SelectedTurbineId}, Hours: {model.Hours}";


            // rebuild dropdowns model
            model.WindFarms = db.WindFarms.Select(w => new SelectListItem
            {
                Value = w.FarmID,
                Text = w.FarmName
            }).ToList();

            model.Turbines = db.Turbines
                .Where(t => t.FarmID == model.SelectedWindFarmId)
                .Select(t => new SelectListItem
                {
                    Value = t.TurbineID,
                    Text = t.TurbineID
                }).ToList();

            return View(model);
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