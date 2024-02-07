namespace MyPortfolio.Entity.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException()
            : base("The requested entity was not found.")
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
