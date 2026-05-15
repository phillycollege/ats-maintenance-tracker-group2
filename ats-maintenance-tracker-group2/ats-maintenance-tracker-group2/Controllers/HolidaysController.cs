using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ats_maintenance_tracker_group2.Models;
using Microsoft.AspNet.Identity;

public class HolidaysController : Controller
{
    private ATSDBContext db = new ATSDBContext();

    // ✅ INDEX: View all holidays
    public ActionResult Index()
    {
        if (Request.IsAuthenticated)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            if (user == null)
            {
                return HttpNotFound();
            }
            var holidays = db.Holidays
                         .Include(h => h.Staff)
                         .ToList();

            return View(holidays);
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    // ✅ GET: Create Holiday
    public ActionResult BookHoliday()
    {
        return View();
    }

    // ✅ POST: Create Holiday (Book leave)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult BookHoliday(Holiday model)
    {
        // Get logged-in user ID (IMPORTANT)
        string userId = User.Identity.GetUserId();

        // Set system values
        model.StaffID = userId;
        model.DateRequested = DateTime.Now;
        model.Status = "Pending";

        // ✅ Validation
        if (model.EndDate < model.StartDate)
        {
            ModelState.AddModelError("", "End date cannot be before start date.");
            return View(model);
        }

        // ✅ Optional: prevent overlapping bookings
        bool overlap = db.Holidays.Any(h =>
            h.StaffID == userId &&
            h.StartDate <= model.EndDate &&
            h.EndDate >= model.StartDate
        );

        if (overlap)
        {
            ModelState.AddModelError("", "You already have a holiday booked in this date range.");
            return View(model);
        }

        // ✅ Save booking
        db.Holidays.Add(model);
        db.SaveChanges();

        return View(model);

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
