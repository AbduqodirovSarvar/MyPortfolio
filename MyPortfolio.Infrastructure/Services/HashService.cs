using MyPortfolio.Application.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.Services
{
    public sealed class HashService : IHashService
    {
        public string GetHash(string password)
        {
            var salt = new byte[32];
            RandomNumberGenerator.Create().GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);

            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[64];

            Array.Copy(salt, 0, hashBytes, 0, 32);
            Array.Copy(hash, 0, hashBytes, 32, 32);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
