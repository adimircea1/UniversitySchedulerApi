using System.Security.Cryptography;
using System.Text;
using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Utils.Interfaces;

namespace UniversityScheduler.Api.Infrastructure.Utils;

[Registration(Type = RegistrationKind.Scoped)]
public class PasswordHasher : IPasswordHasher
{
    public string ComputeHash(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            //get the byte value of password
            var byteValue = Encoding.UTF8.GetBytes(password);

            //compute hash
            var byteHash = sha256.ComputeHash(byteValue);

            //get current hash
            var hash = Convert.ToBase64String(byteHash);

            //call ComputeHash again
            return hash;
        }
    }
}