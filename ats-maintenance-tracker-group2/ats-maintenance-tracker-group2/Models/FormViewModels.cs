using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ats_maintenance_tracker_group2.Models
{
    public class UpdateJobStatusViewModel
    {
        public int JobID { get; set; }
        public string FarmName { get; set; }
        public string JobType { get; set; }
        public string TurbineId { get; set; }
        [Required]
        public string JobCompleteStatus { get; set; } // Awaiting Engineer, Complete
    }

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

    public class UpdateTurbineHoursViewModel
    {

        [Display(Name = "Wind Farm")]
        public string SelectedWindFarmId { get; set; }

        [Display(Name = "Turbine")]
        public string SelectedTurbineId { get; set; }

        public int Hours { get; set; }

        // list of windfarms and turbines for dropdown data
        public List<SelectListItem> WindFarms { get; set; }
        public List<SelectListItem> Turbines { get; set; }

    }
}