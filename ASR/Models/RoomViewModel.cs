using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Models
{
    public class RoomViewModel
    {
        public List<Room> Rooms { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
    }
}
