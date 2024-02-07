namespace MyPortfolio.Application.Models.ViewModels
{
    public sealed class LoginViewModel
    {
        public LoginViewModel(UserViewModel user, string token)
        {
            User = user;
            AccessToken = token;
        }

        public UserViewModel User { get; private set; }
        public string AccessToken { get; private set; }
    }
}
