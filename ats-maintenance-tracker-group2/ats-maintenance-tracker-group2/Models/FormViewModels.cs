using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Models {
    // update job statis form view model: ENGINEER
    public class UpdateJobStatusViewModel {
        public int JobID { get; set; }
        public string FarmName { get; set; }
        public string JobType { get; set; }
        public string TurbineId { get; set; }

        public bool MainGeneratorServiced { get; set; } = false;
        public bool GearboxServiced { get; set; } = false;
        public bool YawMotorServiced { get; set; } = false;
        public bool InternalPassengerLiftServiced { get; set; } = false;

        [Required]
        public string JobCompleteStatus { get; set; } // Awaiting Engineer, Complete
    }

    // create fault job form view model: FAULT LOGGIN
    public class CreateFaultJobViewModel {

        [Display(Name = "Wind Farm")]
        public string SelectedWindFarmId { get; set; }

        [Display(Name = "Turbine")]
        public string SelectedTurbineId { get; set; }

        public string FaultDescription { get; set; }

        // list of windfarms and turbines for dropdown data
        public List<SelectListItem> WindFarms { get; set; }
        public List<SelectListItem> Turbines { get; set; }
    }

    // update turbine hours form view model: SIMULATION
    public class UpdateTurbineHoursViewModel {
        [Display(Name = "Wind Farm")]
        public string SelectedWindFarmId { get; set; }

        [Display(Name = "Turbine")]
        public string SelectedTurbineId { get; set; }

        public int Hours { get; set; }

        // list of windfarms and turbines for dropdown data
        public List<SelectListItem> WindFarms { get; set; }
        public List<SelectListItem> Turbines { get; set; }
    }

    // create a staff form view model: MANAGER
    public class CreateStaffViewModel {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string WorkEmailAddress { get; set; }
        [Required]
        public string HomeMobileNumber { get; set; }
        [Required]
        public string WorkMobileNumber { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string EmploymentRole { get; set; } // Call Handler, Engineer or Manager
        public string Password { get; set; }
    }
}