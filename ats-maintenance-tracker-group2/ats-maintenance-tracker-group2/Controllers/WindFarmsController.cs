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
    public class WindFarmsController : Controller
    {
        private ATSDBContext db = new ATSDBContext();

        // GET: WindFarms
        public ActionResult Index()
        {
            return View(db.WindFarms.ToList());
        }

        // GET: WindFarms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WindFarm windFarm = db.WindFarms.Find(id);
            if (windFarm == null)
            {
                return HttpNotFound();
            }
            return View(windFarm);
        }

        // GET: WindFarms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WindFarms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FarmID,FarmName,Address1,Address2,City,Postcode,Region")] WindFarm windFarm)
        {
            if (ModelState.IsValid)
            {
                db.WindFarms.Add(windFarm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(windFarm);
        }

        // GET: WindFarms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WindFarm windFarm = db.WindFarms.Find(id);
            if (windFarm == null)
            {
                return HttpNotFound();
            }
            return View(windFarm);
        }

        // POST: WindFarms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FarmID,FarmName,Address1,Address2,City,Postcode,Region")] WindFarm windFarm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(windFarm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(windFarm);
        }

        // GET: WindFarms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WindFarm windFarm = db.WindFarms.Find(id);
            if (windFarm == null)
            {
                return HttpNotFound();
            }
            return View(windFarm);
        }

        // POST: WindFarms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WindFarm windFarm = db.WindFarms.Find(id);
            db.WindFarms.Remove(windFarm);
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
