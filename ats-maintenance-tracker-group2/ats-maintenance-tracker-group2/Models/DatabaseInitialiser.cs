using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ats_maintenance_tracker_group2.Models
{
    public class DatabaseInitialiser : DropCreateDatabaseIfModelChanges<ATSDBContext>
    {
        protected override void Seed(ATSDBContext context)
        {
            base.Seed(context);

            List<Staff> Staffs = new List<Staff>();
            List<Shift> Shifts = new List<Shift>();
            List<WindFarm> Farms = new List<WindFarm>();
            List<Turbine> Turbines = new List<Turbine>();
            List<Job> Jobs = new List<Job>();


            // all engineers
            Staff engineer1 = new Staff() {
                Id = "S001",
                StaffID = "S001",
                FirstName = "Alex",
                LastName = "MacLeod",
                WorkMobileNumber = "07123456789",
                HomeMobileNumber = "01411234567",
                UserName = "S001",
                Email = "alex.macleod@ats.com",
                WorkEmailAddress = "alex.macleod@ats.com",
                Address1 = "12 Windmill Road",
                City = "Glasgow",
                Postcode = "G1 2AB",
                Salary = 42000,
                EmploymentRole = "Engineer"
            };
            Staff engineer2 = new Staff()
            {
                Id = "S002",
                StaffID = "S002",
                FirstName = "Jamie",
                LastName = "Fraser",
                WorkMobileNumber = "07987654321",
                HomeMobileNumber = "01315556666",
                UserName = "S002",
                Email = "jamie.fraser@ats.com",
                WorkEmailAddress = "jamie.fraser@ats.com",
                Address1 = "45 Turbine Way",
                City = "Edinburgh",
                Postcode = "EH1 3CD",
                Salary = 30000m,
                EmploymentRole = "Engineer"

            };
            Staff engineer3 = new Staff() {
                Id = "S003",
                StaffID = "S003",
                FirstName = "Morgan",
                LastName = "Reid",
                WorkMobileNumber = "07011223344",
                HomeMobileNumber = "01416321234",
                UserName = "S003",
                Email = "morgan.reid@ats.com",
                WorkEmailAddress = "morgan.reid@ats.com",
                Address1 = "8 Greenhill Drive",
                City = "Stirling",
                Postcode = "FK8 1EF",
                Salary = 39500m,
                EmploymentRole = "Engineer"
            };
            Staff engineer4 = new Staff()
            {
                Id = "S004",
                StaffID = "S004",
                FirstName = "Taylor",
                LastName = "Campbell",
                WorkMobileNumber = "07899887766",
                HomeMobileNumber = "01224555111",
                UserName = "S004",
                Email = "taylor.campbell@ats.com",
                WorkEmailAddress = "taylor.campbell@ats.com",
                Address1 = "21 Harbour Street",
                City = "Aberdeen",
                Postcode = "AB11 5NB",
                Salary = 45000m,
                EmploymentRole = "Engineer"

            };
            Staff engineer5 = new Staff() {
                Id = "S005",
                StaffID = "S005",
                FirstName = "Casey",
                LastName = "Douglas",
                WorkMobileNumber = "07199887755",
                HomeMobileNumber = "01382444555",
                UserName = "S005",
                Email = "casey.douglas@ats.com",
                WorkEmailAddress = "casey.douglas@ats.com",
                Address1 = "3 Riverside Walk",
                City = "Dundee",
                Postcode = "DD1 4HN",
                Salary = 28000m,
                EmploymentRole = "Engineer"

            };
            Staff engineer6 = new Staff() {
                Id = "S006",
                StaffID = "S006",
                FirstName = "Jordan",
                LastName = "Stewart",
                WorkMobileNumber = "07766554433",
                HomeMobileNumber = "01413216543",
                UserName = "S006",
                Email = "jordan.stewart@ats.com",
                WorkEmailAddress = "jordan.stewart@ats.com",
                Address1 = "14 Kelvin Road",
                City = "Glasgow",
                Postcode = "G3 7PL",
                Salary = 32000m,
                EmploymentRole = "Engineer"
            };
            Staff engineer7 = new Staff() {
                Id = "S007",
                StaffID = "S007",
                FirstName = "Riley",
                LastName = "Henderson",
                WorkMobileNumber = "07555123456",
                HomeMobileNumber = "01698555123",
                UserName = "S007",
                Email = "riley.henderson@ats.com",
                WorkEmailAddress = "riley.henderson@ats.com",
                Address1 = "19 Meadows Lane",
                City = "Hamilton",
                Postcode = "ML3 6HF",
                Salary = 41000m,
                EmploymentRole = "Engineer"

            };
            Staff engineer8 = new Staff() {
                Id = "S008",
                StaffID = "S008",
                FirstName = "Avery",
                LastName = "Paterson",
                WorkMobileNumber = "07444333221",
                HomeMobileNumber = "01563555123",
                UserName = "S008",
                Email = "avery.paterson@ats.com",
                WorkEmailAddress = "avery.paterson@ats.com",
                Address1 = "27 Orchard View",
                City = "Kilmarnock",
                Postcode = "KA1 3BU",
                Salary = 29000m,
                EmploymentRole = "Engineer"
            };
            Staffs.Add(engineer1);
            Staffs.Add(engineer2);
            Staffs.Add(engineer3);
            Staffs.Add(engineer4);
            Staffs.Add(engineer5);
            Staffs.Add(engineer6);
            Staffs.Add(engineer7);
            Staffs.Add(engineer8);

            // 2 call handlers
            Staff callHandler1 = new Staff()
            {
                Id = "S009",
                StaffID = "S009",
                FirstName = "Quinn",
                LastName = "Robertson",
                WorkMobileNumber = "07888111222",
                HomeMobileNumber = "01786444123",
                UserName = "S009",
                Email = "quinn.robertson@ats.com",
                WorkEmailAddress = "quinn.robertson@ats.com",
                Address1 = "6 Hillcrest Avenue",
                City = "Perth",
                Postcode = "PH1 5JT",
                Salary = 46000m,
                EmploymentRole = "CallHandler"
            };
            Staff callHandler2 = new Staff() {
                Id = "S010",
                StaffID = "S010",
                FirstName = "Cameron",
                LastName = "Lawson",
                WorkMobileNumber = "07222333444",
                HomeMobileNumber = "01307333222",
                UserName = "S010",
                Email = "cameron.lawson@ats.com",
                WorkEmailAddress = "cameron.lawson@ats.com",
                Address1 = "10 Seaview Terrace",
                City = "Falkirk",
                Postcode = "FK1 1LR",
                Salary = 31000m,
                EmploymentRole = "CallHandler"
            };

            // 1 manager
            Staff manager1 = new Staff()
            {
                Id = "S011",   
                StaffID = "S011",
                FirstName = "Kamran",
                LastName = "Ledford",
                WorkMobileNumber = "07888111222",
                HomeMobileNumber = "01786444123",
                UserName = "S011",
                Email = "kamran.ledford@ats.com",
                WorkEmailAddress = "kamran.ledford@ats.com",
                Address1 = "10 Hillcrest Avenue",
                City = "Perth",
                Postcode = "PH1 5JT",
                Salary = 46000m,
                EmploymentRole = "Manager"
            };
            Staffs.Add(callHandler1);
            Staffs.Add(callHandler2);
            Staffs.Add(manager1);

            // add 8 shifts to engineers
            int shiftId = 1;
            var random = new Random();

            foreach (var engineer in Staffs.FindAll(staff => staff.EmploymentRole == "Engineer")) {
                var days = new List<string>{ "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

                // Pick exactly 5 unique days
                var workingDays = days.OrderBy(x => random.Next()).Take(5).ToList();

                Shifts.Add(new Shift() {
                    ShiftRecordID = shiftId,
                    Id = engineer.StaffID,
                    ShiftType = shiftId <= 4 ? "Early" : "Late",
                    Mon = workingDays.Contains("Mon"),
                    Tue = workingDays.Contains("Tue"),
                    Wed = workingDays.Contains("Wed"),
                    Thu = workingDays.Contains("Thu"),
                    Fri = workingDays.Contains("Fri"),
                    Sat = workingDays.Contains("Sat"),
                    Sun = workingDays.Contains("Sun")
                });

                shiftId++;
            }


            // create 4 wind farms
            WindFarm windFarm1 = new WindFarm() {
                FarmID = "WF001",
                FarmName = "Clyde Valley Wind Farm",
                Address1 = "Hilltop Estate",
                Address2 = "Lanark Road",
                City = "Lanark",
                Postcode = "ML11 9TA",
                Region = "South Lanarkshire"
            };
            WindFarm windFarm2 = new WindFarm() {
                FarmID = "WF002",
                FarmName = "Highland Breeze Wind Farm",
                Address1 = "Strathfield Range",
                Address2 = "A9 North",
                City = "Inverness",
                Postcode = "IV2 7PA",
                Region = "Highlands"
            };
            WindFarm windFarm3 = new WindFarm() {
                FarmID = "WF003",
                FarmName = "North Sea Edge Wind Farm",
                Address1 = "Coastal Industrial Park",
                Address2 = "Harbour Road",
                City = "Aberdeen",
                Postcode = "AB12 3FG",
                Region = "Aberdeenshire"
            };
            WindFarm windFarm4 = new WindFarm() {
                FarmID = "WF004",
                FarmName = "Forth Estuary Wind Farm",
                Address1 = "Estuary View",
                Address2 = "Dockside Lane",
                City = "Grangemouth",
                Postcode = "FK3 8UL",
                Region = "Falkirk"
            };
            Farms.Add(windFarm1);
            Farms.Add(windFarm2);
            Farms.Add(windFarm3);
            Farms.Add(windFarm4);

            // create 40 turbines

            // ======================================================
            // WF001 – Clyde Valley Wind Farm (Vestas)
            // ======================================================
            for (int i = 1; i <= 10; i++)
            {
                Turbines.Add(new Turbine
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
                Turbines.Add(new Turbine
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
                Turbines.Add(new Turbine
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
                Turbines.Add(new Turbine
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

            // create 10 jobs
            var engineers = Staffs.Where(s => s.EmploymentRole == "Engineer").ToList();
            //var random2 = new Random();
            int jobId = 1;

            DateTime startDate = DateTime.Today;

            foreach (var engineer in engineers)
            {
                var shift = Shifts.First(s => s.Id == engineer.StaffID);

                for (int day = 0; day < 7; day++) {
                    DateTime jobDate = startDate.AddDays(day);

                    bool isWorkingDay =
                        jobDate.DayOfWeek >= DayOfWeek.Monday &&
                        jobDate.DayOfWeek <= DayOfWeek.Friday;

                    if (!isWorkingDay) continue;

                    var turbine = Turbines.Count > 0
                        ? Turbines[random.Next(Turbines.Count)]
                        : throw new Exception("Turbines list is empty");

                    Jobs.Add(new Job () {
                        JobID = jobId++,
                        JobDate = jobDate,
                        JobTime = shift.ShiftType,
                        FarmID = turbine.FarmID,
                        TurbineID = turbine.TurbineID,
                        StaffID = engineer.StaffID,
                        JobType = "Service",

                        FaultDescription = null,

                        MainGeneratorServiced = true,
                        GearboxServiced = true,
                        YawMotorServiced = true,
                        InternalPassengerLiftServiced = true,

                        JobCompleteStatus = "Awaiting Engineer"
                    });
                }
            }

            // create roles using role manager


            // connect user manager and add roles to users
            UserManager<Staff> userManager = new UserManager<Staff>(new UserStore<Staff>(context));
            foreach (var staff in Staffs) {
                if (userManager.FindByName(staff.UserName) == null) {
                    userManager.Create(staff, "123456!");
                }
            }

            // save changes and seed context
            foreach (var turbine in Turbines) {
                context.Turbines.Add(turbine);
            }
            foreach (var job in Jobs)
            {
                context.Jobs.Add(job);
            }
            foreach (var shift in Shifts)
            {
                context.Shifts.Add(shift);
            }
            foreach (var windFarm in Farms)
            {
                context.WindFarms.Add(windFarm);
            }
            context.SaveChanges();  
        }
    }
}