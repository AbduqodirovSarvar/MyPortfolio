namespace MyPortfolio.Entity.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException()
            : base("The entity already exists.")
        {
        }

        public AlreadyExistsException(string message)
            : base(message)
        {
        }

        public AlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
