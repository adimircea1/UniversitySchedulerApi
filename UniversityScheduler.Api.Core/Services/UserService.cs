using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class UserService : IUserService
{
    private readonly IDatabaseGenericRepository<User> _userRepository;

    public UserService(IDatabaseGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find user with id {id}");
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllEntitiesAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await QueueAddUserAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task AddUsersAsync(List<User> users)
    {
        await QueueAddUsersAsync(users);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateUserByIdAsync(int id, User user)
    {
        await QueueUpdateUserByIdAsync(id, user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserByIdAsync(int id)
    {
        await QueueDeleteUserByIdAsync(id);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteAllUsersAsync()
    {
        QueueDeleteAllUsers();
        await _userRepository.SaveChangesAsync();
    }

    public async Task<User> UserLoginAsync(string email, string password)
    {
        //get all users from db
        var users = await GetAllUsersAsync();

        //now try to get the user with the given email
        var user = users.First(u => u.Email == email);

        //verify if the password is okay
        if (user.Password != new PasswordHasher().ComputeHash(password)) throw new Exception("Password is incorrect!");

        //finally return the user
        return user;
    }

    public async Task QueueAddUserAsync(User user)
    {
        var hasher = new PasswordHasher();

        await _userRepository.AddEntityAsync(new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = hasher.ComputeHash(user.Password)
        });
    }

    public async Task QueueAddUsersAsync(List<User> users)
    {
        var hasher = new PasswordHasher();

        foreach (var user in users)
            await _userRepository.AddEntityAsync(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = hasher.ComputeHash(user.Password)
            });
    }

    public async Task QueueUpdateUserByIdAsync(int id, User user)
    {
        await _userRepository.UpdateEntityByIdAsync(id, user);
    }

    public async Task QueueDeleteUserByIdAsync(int id)
    {
        await _userRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllUsers()
    {
        _userRepository.DeleteAllEntities();
    }
}