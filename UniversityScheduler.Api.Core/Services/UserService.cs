using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;
using UniversityScheduler.Api.Core.Utils.Email;
using UniversityScheduler.Api.Core.Utils.Interfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class UserService : IUserService
{
    private readonly IDatabaseGenericRepository<User> _userRepository;
    private readonly IConfirmationEmailSender _confirmationEmailSender;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IDatabaseGenericRepository<User> userRepository,
        IConfirmationEmailSender confirmationEmailSender, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _confirmationEmailSender = confirmationEmailSender;
        _passwordHasher = passwordHasher;
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

    public async Task<User> FindUserByEmailAsync(string email)
    {
        return (await GetAllUsersAsync()).FirstOrDefault(u => u.Email == email) ??
               throw new Exception($"User having email {email} does not exist!");
    }
    
    public async Task AddUserAsync(User user)
    {
        await QueueAddUserAsync(user);
        await _userRepository.SaveChangesAsync();
        
        //now send a confirmation link
        //my id field is automatic, so whenever i add a new User, the id value will grow => i have to get the is from the db, otherwise it will be 0
        var id = (await FindUserByEmailAsync(user.Email)).Id;
        await _confirmationEmailSender.SendEmailAsync(user.Email, id);
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
        if (user.Password != _passwordHasher.ComputeHash(password)) throw new Exception("Password is incorrect!");

        //finally return the user
        return user;
    }

    public async Task SendConfirmationEmailAsync(int userId)
    {
        await QueueSendConfirmationEmailAsync(userId);
    }

    public async Task SendConfirmationEmailAsync(string userEmail)
    {
        await QueueSendConfirmationEmailAsync(userEmail);
    }

    public async Task UpdateUserByEmailAsync(string userEmail, User updatedUser)
    {
        await QueueUpdateUserByEmailAsync(userEmail, updatedUser);
        await _userRepository.SaveChangesAsync();
    }
    
    public async Task QueueAddUserAsync(User user)
    {
        await _userRepository.AddEntityAsync(new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = _passwordHasher.ComputeHash(user.Password)
        });
    }

    public async Task QueueAddUsersAsync(List<User> users)
    {
        foreach (var user in users)
            await _userRepository.AddEntityAsync(new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = _passwordHasher.ComputeHash(user.Password)
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

    public async Task QueueSendConfirmationEmailAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId);

        if (!user.EmailConfirmed)
        {
            await _confirmationEmailSender.SendEmailAsync(user.Email, user.Id);
        }
        else
        {
            throw new Exception("This email address has been already confirmed!");
        }
    }

    public async Task QueueSendConfirmationEmailAsync(string userEmail)
    {
        var user = await FindUserByEmailAsync(userEmail);

        if (!user.EmailConfirmed)
        {
            await _confirmationEmailSender.SendEmailAsync(user.Email, user.Id);
        }
        else
        {
            throw new Exception("This email address has been already confirmed!");
        }
    }

    public async Task QueueUpdateUserByEmailAsync(string userEmail, User updatedUser)
    {
        var user = await FindUserByEmailAsync(userEmail);
        await _userRepository.UpdateEntityByIdAsync(user.Id, updatedUser);
    }
    
}