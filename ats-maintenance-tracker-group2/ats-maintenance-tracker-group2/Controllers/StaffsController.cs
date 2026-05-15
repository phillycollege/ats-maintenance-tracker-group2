using ats_maintenance_tracker_group2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Controllers {
    public class StaffsController : Controller {
        private ATSDBContext db = new ATSDBContext();

        // GET: Staffs
        public ActionResult Index() {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not managers back to home page
            if (Request.IsAuthenticated) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                if (user.EmploymentRole != "Manager") {
                    return RedirectToAction("Index", "Home");
                }
                

                // get all staff members and send to frontend
                return View(db.Users.ToList());
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Staffs/Details/5
        public ActionResult Details(string id) {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not managers back to home page
            if (Request.IsAuthenticated) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                if (user.EmploymentRole != "Manager") {
                    return RedirectToAction("Index", "Home");
                }
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                // get the details of the staff member using the id and send to the front end
                Staff staff = db.Users.Find(id);
                if (staff == null) {
                    return HttpNotFound();
                }
                return View(staff);                
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Staffs/Create
        public ActionResult Create() {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not managers back to home page
            if (Request.IsAuthenticated) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                if (user.EmploymentRole != "Manager") {
                    return RedirectToAction("Index", "Home");
                }
                
                // initialise the create staff form view model class and send to the front end 
                CreateStaffViewModel createStaffViewModel = new CreateStaffViewModel() {
                    FirstName = "",
                    LastName = "",
                    WorkEmailAddress = "",
                    HomeMobileNumber = "",
                    WorkMobileNumber = "",
                    Address1 = "",
                    City = "",
                    Postcode = "",
                    EmploymentRole = "",
                    Password = "",
                    Salary = 0
                };
                return View(createStaffViewModel);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStaffViewModel createStaffViewModel) {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not managers back to home page
            if (Request.IsAuthenticated) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                if (user.EmploymentRole != "Manager") {
                    return RedirectToAction("Index", "Home");
                }

                // get the last staff id and generate the following staff id for a new staff member
                string lastStaffId = db.Users.OrderByDescending(u => u.StaffID).Select(u => u.StaffID).FirstOrDefault();
                int newStaffIdAsInt = int.Parse(lastStaffId.Split('S')[1]) + 1;
                string newStaffId = $"S{newStaffIdAsInt:D3}";

                // create a new staff using the new staff id and the information filled in from the form
                // which is inside the create staff form view model
                Staff newStaff = new Staff() {
                    Id = newStaffId,
                    StaffID = newStaffId,
                    FirstName = createStaffViewModel.FirstName,
                    LastName = createStaffViewModel.LastName,
                    WorkMobileNumber = createStaffViewModel.WorkMobileNumber,
                    HomeMobileNumber = createStaffViewModel.HomeMobileNumber,
                    UserName = newStaffId,
                    Email = createStaffViewModel.WorkEmailAddress,
                    WorkEmailAddress = createStaffViewModel.WorkEmailAddress,
                    Address1 = createStaffViewModel.Address1,
                    City = createStaffViewModel.City,
                    Postcode = createStaffViewModel.Postcode,
                    Salary = createStaffViewModel.Salary,
                    EmploymentRole = createStaffViewModel.EmploymentRole
                };

                // add the new staff and save the changes in the database
                if (ModelState.IsValid) {
                    UserManager<Staff> userManager = new UserManager<Staff>(new UserStore<Staff>(db));
                    userManager.Create(newStaff, createStaffViewModel.Password);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Staffs");
                }

                return View(createStaffViewModel);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(string id) {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not managers back to home page
            if (Request.IsAuthenticated) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                if (user.EmploymentRole != "Manager") {
                    return RedirectToAction("Index", "Home");
                }
                
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                
                // get the current values of staff that is going to be edited
                Staff staff = db.Users.Find(id);
                if (staff == null) {
                    return HttpNotFound();
                }

                // add the staff values to the form view model
                CreateStaffViewModel createStaffViewModel = new CreateStaffViewModel() {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    WorkEmailAddress = staff.WorkEmailAddress,
                    HomeMobileNumber = staff.HomeMobileNumber,
                    WorkMobileNumber = staff.WorkMobileNumber,
                    Address1 = staff.Address1,
                    City = staff.City,
                    Postcode = staff.Postcode,
                    Salary = staff.Salary,
                    EmploymentRole = staff.EmploymentRole
                };

                // send the form view model to the front end with the values that can be changed
                return View(createStaffViewModel);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateStaffViewModel createStaffViewModel) {
            // check if user is logged in
            // redirect users who are not logged in to the login page
            // redirect users who are not managers back to home page
            if (Request.IsAuthenticated) {
                var user = db.Users.Find(User.Identity.GetUserId());
                if (user == null) {
                    return HttpNotFound();
                }
                if (user.EmploymentRole != "Manager") {
                    return RedirectToAction("Index", "Home");
                }

                // get the staff from the database using the id from the form view model
                Staff staff = db.Users.Find(createStaffViewModel.Id);
                if (staff == null) {
                    return HttpNotFound();
                }

                // if the model is valid
                // make the updates to the staff class and update the database
                if (ModelState.IsValid) {
                    staff.FirstName = createStaffViewModel.FirstName;
                    staff.LastName = createStaffViewModel.LastName;
                    staff.WorkEmailAddress = createStaffViewModel.WorkEmailAddress;
                    staff.HomeMobileNumber = createStaffViewModel.HomeMobileNumber;
                    staff.WorkMobileNumber = createStaffViewModel.WorkMobileNumber;
                    staff.Address1 = createStaffViewModel.Address1;
                    staff.City = createStaffViewModel.City;
                    staff.Postcode = createStaffViewModel.Postcode;
                    staff.Salary = createStaffViewModel.Salary;
                    staff.EmploymentRole = createStaffViewModel.EmploymentRole;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(staff);
            } else {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = db.Users.Find(id);
            if (staff == null) {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            Staff staff = db.Users.Find(id);
            db.Users.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
