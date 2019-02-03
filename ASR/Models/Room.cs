using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Models
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string RoomID { get; set; }
    }
}
