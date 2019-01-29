using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace ASR.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    [DataContract]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [DataMember]
        public string SchoolID { get; set; }
    }
}
