namespace MyPortfolio.Application.Exceptions
{
    public sealed class LoginException : Exception
    {
        public LoginException()
            : base("Email or password is not correct!")
        {
        }

        public LoginException(string message)
            : base(message)
        {
        }

        public LoginException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
