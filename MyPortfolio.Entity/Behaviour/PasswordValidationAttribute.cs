using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Entity.Behaviour
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class PasswordValidationAttribute : ValidationAttribute
    {
        public PasswordValidationAttribute()
        {
            ErrorMessage = "Invalid password format. It must have a minimum of 8 characters, at least 1 uppercase letter, 1 digit and 1 symbol!";
        }

        public override bool IsValid(object? value)
        {
            if (value == null || value is not string password)
            {
                return false;
            }

            // Password criteria: Minimum 8 characters, at least 1 uppercase letter, 1 digit and 1 symbol
            const string passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";

#pragma warning disable SYSLIB1045

            return System.Text.RegularExpressions.Regex.IsMatch(password, passwordPattern);

#pragma warning restore SYSLIB1045
        }
    }
}
