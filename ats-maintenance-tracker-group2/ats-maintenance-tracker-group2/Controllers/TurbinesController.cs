using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ats_maintenance_tracker_group2.Models;

namespace ats_maintenance_tracker_group2.Controllers
{
    public class TurbinesController : Controller
    {
        private ATSDBContext db = new ATSDBContext();

        // GET: Turbines
        public ActionResult Index()
        {
            var turbines = db.Turbines.Include(t => t.WindFarm);
            return View(turbines.ToList());
        }

        // GET: Turbines/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turbine turbine = db.Turbines.Find(id);
            if (turbine == null)
            {
                return HttpNotFound();
            }
            return View(turbine);
        }

        // GET: Turbines/Create
        public ActionResult Create()
        {
            ViewBag.FarmID = new SelectList(db.WindFarms, "FarmID", "FarmName");
            return View();
        }

        // POST: Turbines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TurbineID,TurbineMake,TurbineModel,RuntimeHours,IsHighWinds,OperationalStatus,Coordinates,FarmID")] Turbine turbine)
        {
            if (ModelState.IsValid)
            {
                db.Turbines.Add(turbine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FarmID = new SelectList(db.WindFarms, "FarmID", "FarmName", turbine.FarmID);
            return View(turbine);
        }

        // GET: Turbines/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turbine turbine = db.Turbines.Find(id);
            if (turbine == null)
            {
                return HttpNotFound();
            }
            ViewBag.FarmID = new SelectList(db.WindFarms, "FarmID", "FarmName", turbine.FarmID);
            return View(turbine);
        }

        // POST: Turbines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TurbineID,TurbineMake,TurbineModel,RuntimeHours,IsHighWinds,OperationalStatus,Coordinates,FarmID")] Turbine turbine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turbine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FarmID = new SelectList(db.WindFarms, "FarmID", "FarmName", turbine.FarmID);
            return View(turbine);
        }

        // GET: Turbines/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turbine turbine = db.Turbines.Find(id);
            if (turbine == null)
            {
                return HttpNotFound();
            }
            return View(turbine);
        }

        // POST: Turbines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Turbine turbine = db.Turbines.Find(id);
            db.Turbines.Remove(turbine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
