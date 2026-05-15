
//import basic system functionality
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Import Entity Framework (used for database access)
using System.Data.Entity;

//Import ASP.NET Identity (used for authentication and users)
using Microsoft.AspNet.Identity.EntityFramework;

namespace ats_maintenance_tracker_group2.Models
{
    
    // Database context class
    // Inherits from IdentityDbContext to include user authentication features

    public class ATSDBContext : IdentityDbContext<Staff> // IdentityDbContex adds login/user system using Staff user model
    {
        // Tables:
        // DbSet<> represents a table in the database
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<WindFarm> WindFarms { get; set; }
        public DbSet<Turbine> Turbines { get; set; }

        // Constructor for the database context
        // ATSDBContext = the database connection + tables
        public ATSDBContext() : base("ATSMaintenanceTrackerConnection", throwIfV1Schema: false)
        {
            // Sets the database initializer (used to seed data or create DB)
            Database.SetInitializer(new DatabaseInitialiser()); // DatabaseInitialiser = sets up or seeds the database
        }


        // Static method to create a new instance of the context
        // Used by ASP.NET Identity internally

        public static ATSDBContext Create() // Create() used by the framework to create the context
        {
            return new ATSDBContext();
        }
    }
}