using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Models
{
    public class SlotStaffViewModel
    {
        public List<Slot> Slots { get; set; }

        public SelectList Staff { get; set; }
        
        [Required]
        public string StaffID { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
    }
}
