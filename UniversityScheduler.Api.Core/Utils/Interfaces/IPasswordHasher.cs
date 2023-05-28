namespace UniversityScheduler.Api.Core.Utils.Interfaces;

public interface IPasswordHasher
{
     public string ComputeHash(string password);
}