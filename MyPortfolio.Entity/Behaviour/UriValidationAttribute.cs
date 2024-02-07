using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Entity.Behaviour
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class UriValidationAttribute : ValidationAttribute
    {
        public UriValidationAttribute()
        {
            ErrorMessage = "Invalid Url!";
        }
        public override bool IsValid(object? value)
        {
            if (value == null || value is not string url)
            {
                return false;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
