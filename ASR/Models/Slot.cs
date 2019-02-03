using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Models
{
    public class Slot
     {
        [Required]
        [DisplayFormat(DataFormatString = "{0:h tt  dddd dd MMMM yyyy}",
            ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [ForeignKey("Id")]
        public string StaffID { get; set; }
        public virtual ApplicationUser Staff { get; set; }

        [ForeignKey("Id")]
        public string StudentID { get; set; }
        [Display(Name = "Booked By")]
        public virtual ApplicationUser Student { get; set; }

        [Required]
        [Display(Name = "Room")]
        public string RoomID { get; set; }
        public virtual Room Room { get; set; }
    }
}
