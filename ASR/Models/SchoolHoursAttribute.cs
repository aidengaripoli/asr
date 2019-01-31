using System;
using System.ComponentModel.DataAnnotations;

namespace ASR.Models
{
    internal class SchoolHoursAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var slotRoomsViewModel = (SlotRoomsViewModel)validationContext.ObjectInstance;

            TimeSpan start = new TimeSpan(9, 0, 0); // 9:00am
            TimeSpan end = new TimeSpan(13, 0, 0); // 1:00pm

            if ((slotRoomsViewModel.StartTime.TimeOfDay < start) || (slotRoomsViewModel.StartTime.TimeOfDay > end))
            {
                return new ValidationResult(GetSchoolHoursErrorMessage());
            }

            if (slotRoomsViewModel.StartTime.TimeOfDay.Minutes != 0)
            {
                return new ValidationResult(GetOnHourErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetSchoolHoursErrorMessage()
        {
            return "Time must be between 9:00am and 2:00pm.";
        }

        private string GetOnHourErrorMessage()
        {
            return "Time must be on the hour.";
        }
    }
}