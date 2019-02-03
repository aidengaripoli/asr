using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASR.Models
{
    public class SlotRoomsViewModel : IValidatableObject
    {
        public SelectList Rooms { get; set; }

        [MaxBookingPerDay(2)]
        public string RoomID { get; set; }

        [DataType(DataType.DateTime)]
        [SchoolHours]
        [CannotBeInPast]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (ASRContext)validationContext.GetService(typeof(ASRContext));
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            var slot = context.Slot.FirstOrDefault(x => x.StartTime == StartTime && x.RoomID == RoomID);

            if (slot != null)
            {
                yield return new ValidationResult($"This room has already been booked for this time.", new[] { "RoomID" });
            }

            var currentUser = context.ApplicationUser
                .FirstOrDefault(u => u.Email == httpContextAccessor.HttpContext.User.Identity.Name);

            var numSlotsForDate = context.Slot.Where(x => x.StaffID == currentUser.Id)
                .Where(x => x.StartTime.Date == StartTime.Date)
                .Count();

            if (numSlotsForDate == 4)
            {
                yield return new ValidationResult($"You have already booked 4 slots for that Date.", new[] { "StartTime" });
            }

            yield return ValidationResult.Success;
        }
    }
}
