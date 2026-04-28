using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ats_maintenance_tracker_group2.Models
{
    public class Shift
    {
        [Key]
        public string shiftRecordID { get; set; }

        //Navigational Property
        public string staffID { get; set; }
        [ForeignKey(nameof(staffID))]
        public Staff staff { get; set; }
        public string shiftType { get; set; }
        public bool mon { get; set; }jfsdjosfjsodjfosd
        public bool tue { get; set; }
        public bool wed { get; set; }
        public bool thu { get; set; }
        public bool fri { get; set; }
        public bool sat { get; set; }
        public bool sun { get; set; }
    }
    }