namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface IHashService
    {
        string GetHash(string password);
        bool VerifyHash(string password, string paswordHash);
    }
}
