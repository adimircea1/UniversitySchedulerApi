using UniversityScheduler.Api.Core.Models;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface IUserService
{
    public Task<User> GetUserByIdAsync(int id);
    public Task<List<User>> GetAllUsersAsync();
    public Task<User> FindUserByEmailAsync(string email);

    public Task QueueAddUserAsync(User user);
    public Task QueueAddUsersAsync(List<User> users);
    public Task QueueUpdateUserByIdAsync(int id, User user);
    public Task QueueDeleteUserByIdAsync(int id);
    public void QueueDeleteAllUsers();
    public Task QueueSendConfirmationEmailAsync(int userId);
    public Task QueueUpdateUserByEmailAsync(string userEmail, User user);
    public Task QueueSendConfirmationEmailAsync(string userEmail);
    
    public Task AddUserAsync(User user);
    public Task AddUsersAsync(List<User> users);
    public Task UpdateUserByIdAsync(int id, User user);
    public Task DeleteUserByIdAsync(int id);
    public Task DeleteAllUsersAsync();
    public Task<User> UserLoginAsync(string email, string password);
    public Task SendConfirmationEmailAsync(int userId);
    public Task UpdateUserByEmailAsync(string userEmail, User user);
    public Task SendConfirmationEmailAsync(string userEmail);
}