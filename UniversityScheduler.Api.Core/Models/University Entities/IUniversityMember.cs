namespace UniversityScheduler.Api.Core.Models.University_Entities;

public interface IUniversityMember : IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthdayDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}