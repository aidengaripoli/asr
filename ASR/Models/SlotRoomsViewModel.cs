using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Models
{
    public class SlotRoomsViewModel
    {
        public SelectList Rooms { get; set; }

        [MaxBookingPerDay(2)]
        public string RoomID { get; set; }

        [DataType(DataType.DateTime)]
        [SchoolHours]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
    }
}
