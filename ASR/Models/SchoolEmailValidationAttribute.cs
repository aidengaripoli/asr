using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ASR.Areas.Identity.Pages.Account.RegisterModel;

namespace ASR.Models
{
    public class SchoolEmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            InputModel inputModel = (InputModel)validationContext.ObjectInstance;

            const string staffEmailRegex = @"^((e)\d{5})@rmit.edu.au$";
            const string studentEmailRegex = @"^((s)\d{7})@student.rmit.edu.au$";

            bool isValidStaffEmail = Regex.IsMatch(inputModel.Email, staffEmailRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool isValidStudentEmail = Regex.IsMatch(inputModel.Email, studentEmailRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if ((!isValidStaffEmail) && (!isValidStudentEmail))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Not a valid school Email";
        }
    }
}
