using ats_maintenance_tracker_group2.Models;
using Microsoft.Owin.BuilderProperties;

//using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class ATSMaintenanceTrackerDbContext : DbContext
    {
       
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<WindFarm> WindFarms { get; set; }
        public DbSet<Turbine> Turbines { get; set; }


        public ATSMaintenanceTrackerDbContext() : base("ATSMaintenanceTrackerConnection")
        {
            Database.SetInitializer(new ATSMaintenanceTrackerDbInitialiser());
        }

    }


    public class ATSMaintenanceTrackerDbInitialiser : DropCreateDatabaseIfModelChanges<ATSMaintenanceTrackerDbContext>
    {
        protected override void Seed(ATSMaintenanceTrackerDbContext context)
        {
            base.Seed(context);

            //create 10 engineers
            Engineer engineer1 = new Engineer()

            {

                StaffID = "S001",
                FirstName = "Alex",
                LastName = "MacLeod",
                WorkMobileNumber = "07123456789",
                HomeMobileNumber = "01411234567",
                WorkEmailAddress = "alex.macleod@ats.com",
                Address1 = "12 Windmill Road",
                City = "Glasgow",
                Postcode = "G1 2AB",
                Salary = 42000,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"

            };

            Engineer engineer2 = new Engineer()

            {

                StaffID = "S002",
                FirstName = "Jamie",
                LastName = "Fraser",
                WorkMobileNumber = "07987654321",
                HomeMobileNumber = "01315556666",
                WorkEmailAddress = "jamie.fraser@ats.com",
                Address1 = "45 Turbine Way",
                City = "Edinburgh",
                Postcode = "EH1 3CD",
                Salary = 30000m,
                EmploymentRole = "Call Handler",
                StaffType = "CallHandler"

            };
            Engineer engineer3 = new Engineer()
            {
                StaffID = "S003",
                FirstName = "Morgan",
                LastName = "Reid",
                WorkMobileNumber = "07011223344",
                HomeMobileNumber = "01416321234",
                WorkEmailAddress = "morgan.reid@ats.com",
                Address1 = "8 Greenhill Drive",
                City = "Stirling",
                Postcode = "FK8 1EF",
                Salary = 39500m,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"
            };

            Engineer engineer4 = new Engineer()
            {

                StaffID = "S004",
                FirstName = "Taylor",
                LastName = "Campbell",
                WorkMobileNumber = "07899887766",
                HomeMobileNumber = "01224555111",
                WorkEmailAddress = "taylor.campbell@ats.com",
                Address1 = "21 Harbour Street",
                City = "Aberdeen",
                Postcode = "AB11 5NB",
                Salary = 45000m,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"

            };

            Engineer engineer5 = new Engineer()
            {

                StaffID = "S005",
                FirstName = "Casey",
                LastName = "Douglas",
                WorkMobileNumber = "07199887755",
                HomeMobileNumber = "01382444555",
                WorkEmailAddress = "casey.douglas@ats.com",
                Address1 = "3 Riverside Walk",
                City = "Dundee",
                Postcode = "DD1 4HN",
                Salary = 28000m,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"

            };

            Engineer engineer6 = new Engineer()
            {

                StaffID = "S006",
                FirstName = "Jordan",
                LastName = "Stewart",
                WorkMobileNumber = "07766554433",
                HomeMobileNumber = "01413216543",
                WorkEmailAddress = "jordan.stewart@ats.com",
                Address1 = "14 Kelvin Road",
                City = "Glasgow",
                Postcode = "G3 7PL",
                Salary = 32000m,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"
            };

            Engineer engineer7 = new Engineer()
            {

                StaffID = "S007",
                FirstName = "Riley",
                LastName = "Henderson",
                WorkMobileNumber = "07555123456",
                HomeMobileNumber = "01698555123",
                WorkEmailAddress = "riley.henderson@ats.com",
                Address1 = "19 Meadows Lane",
                City = "Hamilton",
                Postcode = "ML3 6HF",
                Salary = 41000m,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"

            };

            Engineer engineer8 = new Engineer()
            {
                StaffID = "S008",
                FirstName = "Avery",
                LastName = "Paterson",
                WorkMobileNumber = "07444333221",
                HomeMobileNumber = "01563555123",
                WorkEmailAddress = "avery.paterson@ats.com",
                Address1 = "27 Orchard View",
                City = "Kilmarnock",
                Postcode = "KA1 3BU",
                Salary = 29000m,
                EmploymentRole = "Call Handler",
                StaffType = "CallHandler"

            };

            Engineer engineer9 = new Engineer()
            {
                StaffID = "S009",
                FirstName = "Quinn",
                LastName = "Robertson",
                WorkMobileNumber = "07888111222",
                HomeMobileNumber = "01786444123",
                WorkEmailAddress = "quinn.robertson@ats.com",
                Address1 = "6 Hillcrest Avenue",
                City = "Perth",
                Postcode = "PH1 5JT",
                Salary = 46000m,

                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"
            };

            Engineer engineer10 = new Engineer()
            {
                StaffID = "S010",
                FirstName = "Cameron",
                LastName = "Lawson",
                WorkMobileNumber = "07222333444",
                HomeMobileNumber = "01307333222",
                WorkEmailAddress = "cameron.lawson@ats.com",
                Address1 = "10 Seaview Terrace",
                City = "Falkirk",
                Postcode = "FK1 1LR",
                Salary = 31000m,
                EmploymentRole = "Maintenance Engineer",
                StaffType = "Engineer"
            };
            // create the Windfarms

            WindFarm windFarm1 = new WindFarm()
            {
                farmID = "WF001",
                farmName = "Clyde Valley Wind Farm",
                address1 = "Hilltop Estate",
                address2 = "Lanark Road",
                city = "Lanark",
                postcode = "ML11 9TA",
                region = "South Lanarkshire"

            };


            WindFarm windFarm2 = new WindFarm()
            {
                farmID = "WF002",
                farmName = "Highland Breeze Wind Farm",
                address1 = "Strathfield Range",
                address2 = "A9 North",
                city = "Inverness",
                postcode = "IV2 7PA",
                region = "Highlands"

            };


            WindFarm windFarm3 = new WindFarm()
            {
                farmID = "WF003",
                farmName = "North Sea Edge Wind Farm",
                address1 = "Coastal Industrial Park",
                address2 = "Harbour Road",
                city = "Aberdeen",
                postcode = "AB12 3FG",
                region = "Aberdeenshire"

            };


            WindFarm windFarm4 = new WindFarm()
            {
                farmID = "WF004",
                farmName = "Forth Estuary Wind Farm",
                address1 = "Estuary View",
                address2 = "Dockside Lane",
                city = "Grangemouth",
                postcode = "FK3 8UL",
                region = "Falkirk"

            };

            // creating some turbines
            var turbines = new List<Turbine>();

            // ======================================================
            // WF001 – Clyde Valley Wind Farm (Vestas)
            // ======================================================
            for (int i = 1; i <= 10; i++)
            {
                turbines.Add(new Turbine
                {
                    TurbineID = $"WF001-T{i:D2}",
                    TurbineMake = "Vestas",
                    TurbineModel = "V90-2.0MW",
                    RuntimeHours = 1100 + (i * 70), // max 1800
                    IsHighWinds = i % 5 == 0,
                    OperationalStatus = i % 5 == 0
                        ? "6 High Wind Shutdown"
                        : "1 Operational",
                    Coordinates = $"55.67{i}, -3.78{i}",
                    FarmID = "WF001"
                });
            }

            // ======================================================
            // WF002 – Highland Breeze Wind Farm (Siemens Gamesa)
            // ======================================================
            for (int i = 1; i <= 10; i++)
            {
                turbines.Add(new Turbine
                {
                    TurbineID = $"WF002-T{i:D2}",
                    TurbineMake = "Siemens Gamesa",
                    TurbineModel = "SG 3.6-130",
                    RuntimeHours = 1500 + (i * 30), // max 1800
                    IsHighWinds = false,
                    OperationalStatus = i % 4 == 0
                        ? "2 Maintenance Required"
                        : "1 Operational",
                    Coordinates = $"57.47{i}, -4.21{i}",
                    FarmID = "WF002"
                });
            }

            // ======================================================
            // WF003 – North Sea Edge Wind Farm (GE Renewable Energy)
            // ======================================================
            for (int i = 1; i <= 10; i++)
            {
                turbines.Add(new Turbine
                {
                    TurbineID = $"WF003-T{i:D2}",
                    TurbineMake = "GE Renewable Energy",
                    TurbineModel = "GE 2.5-120",
                    RuntimeHours = 900 + (i * 90), // max 1800
                    IsHighWinds = false,
                    OperationalStatus = i % 3 == 0
                        ? "3 Fault Detected"
                        : "1 Operational",
                    Coordinates = $"57.14{i}, -2.09{i}",
                    FarmID = "WF003"
                });
            }

            // ======================================================
            // WF004 – Forth Estuary Wind Farm (Enercon)
            // ======================================================
            for (int i = 1; i <= 10; i++)
            {
                turbines.Add(new Turbine
                {
                    TurbineID = $"WF004-T{i:D2}",
                    TurbineMake = "Enercon",
                    TurbineModel = "E-115 EP3",
                    RuntimeHours = 1600 + (i * 30), // max 1900
                    IsHighWinds = false,
                    OperationalStatus = i % 2 == 0
                        ? "4 Under Maintenance"
                        : "1 Operational",
                    Coordinates = $"56.00{i}, -3.71{i}",
                    FarmID = "WF004"
                });
            }

            context.Turbines.AddRange(turbines);
            context.SaveChanges();



        }
    }


}