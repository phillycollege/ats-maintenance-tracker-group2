using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ats_maintenance_tracker_group2.Models
{
    public class ATSDBContext : IdentityDbContext<Staff>
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<WindFarm> WindFarms { get; set; }
        public DbSet<Turbine> Turbines { get; set; }

        public ATSDBContext() : base("ATSMaintenanceTrackerConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitialiser());
        }

        public static ATSDBContext Create()
        {
            return new ATSDBContext();
        }

        public System.Data.Entity.DbSet<ats_maintenance_tracker_group2.Models.Staff> Staffs { get; set; }
    }
}