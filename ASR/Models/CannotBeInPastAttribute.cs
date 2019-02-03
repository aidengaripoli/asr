using System;
using System.ComponentModel.DataAnnotations;

namespace ASR.Models
{
    internal class CannotBeInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var slotRoomsViewModel = (SlotRoomsViewModel)validationContext.ObjectInstance;

            // throw validation error if datetime is less than the current datetime
            if (slotRoomsViewModel.StartTime < DateTime.Now)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "You cannot create a slot in the past.";
        }
    }
}