using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ats_maintenance_tracker_group2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Staff : IdentityUser
    {
        [Key]
        public string StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkMobileNumber { get; set; }
        public string HomeMobileNumber { get; set; }
        public string WorkEmailAddress { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public decimal Salary { get; set; }
        public string EmploymentRole { get; set; } // Call Handler, Engineer or Manager

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Staff> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}