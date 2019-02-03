using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ASR.Models
{
    internal class MaxBookingPerDayAttribute : ValidationAttribute
    {
        private int _maxBookingsPerDay;

        public MaxBookingPerDayAttribute(int maxBookingsPerDay)
        {
            _maxBookingsPerDay = maxBookingsPerDay;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var slotRoomsViewModel = (SlotRoomsViewModel)validationContext.ObjectInstance;

            var service = (ASRContext)validationContext.GetService(typeof(ASRContext));

            // get the number of slots created for a given room on a given date
            var numSlotsForRoomOnDate = service.Slot
                .Where(x => x.RoomID == slotRoomsViewModel.RoomID && x.StartTime.Date == slotRoomsViewModel.StartTime.Date)
                .Count();

            // if the number of slots is greater than the maximum, throw a validation error
            if (numSlotsForRoomOnDate >= _maxBookingsPerDay)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "This room has already been booked twice for this date.";
        }
    }
}