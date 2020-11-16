using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Manager.Services.Domain.Models
{
    public class NewEventModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide event name.")]
        public string Event_Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide event description.")]
        public string Event_Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide event venue.")]
        public string Event_Venue { get; set; }

        [Required(ErrorMessage = "Please provide event start date.")]
        public DateTime? Event_Start_Date { get; set; }
        public DateTime? Event_End_Date { get; set; }
    }
}
