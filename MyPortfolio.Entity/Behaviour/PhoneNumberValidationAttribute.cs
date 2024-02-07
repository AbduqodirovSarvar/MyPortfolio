using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Entity.Behaviour
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class PhoneNumberValidationAttribute : ValidationAttribute
    {
        public PhoneNumberValidationAttribute()
        {
            ErrorMessage = "Invalid phone number.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null || value is not string phoneNumber)
            {
                return false;
            }

            const string phonePattern = @"^\+\d{1,3}(\d{10,12})?$";

#pragma warning disable SYSLIB1045

            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, phonePattern);

#pragma warning restore SYSLIB1045
        }
    }
}
