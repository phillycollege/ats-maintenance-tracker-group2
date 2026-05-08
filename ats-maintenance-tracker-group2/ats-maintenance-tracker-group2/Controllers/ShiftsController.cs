using ats_maintenance_tracker_group2.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers
{
    public class ShiftsController : Controller
    {
        private ATSDBContext db = new ATSDBContext();

        // ✅ GET: Shifts
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var staff = db.Users.Find(User.Identity.GetUserId());

                // ✅ Only Engineers can view shifts
                if (staff.EmploymentRole != "Engineer")
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                var shifts = db.Shifts
                    .Include(s => s.Staff)
                    .Where(s => s.Id == staff.Id)
                    .ToList();

                return View(shifts);
            }

            return RedirectToAction("Login", "Account");
        }

        // ✅ GET: Shifts/Details/1

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = db.Users.Find(User.Identity.GetUserId());

            var shift = db.Shifts
                .Include(s => s.Staff)
                .FirstOrDefault(s => s.ShiftRecordID == id.Value);

            if (shift == null)
            {
                return HttpNotFound();
            }

            if (shift.Id != staff.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(shift);
        }
    }
}

