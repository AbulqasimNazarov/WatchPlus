using Microsoft.AspNetCore.Mvc;
using WatchPlus.Dtos;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;
using WatchPlus.Services.Base;
using WatchPlus.Validators;

namespace WatchPlus.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }


    public async Task CreateNewUserAsync(User newUser, IFormFile image)
    {
        //ArgumentNullException.ThrowIfNullOrEmpty(newUser.Name);
        await this.userRepository.CreatableAsync(newUser, image);
    }




    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await this.userRepository.GetByIdAsync(id);
        if (user == null){
            throw new ArgumentNullException("User not found");
        }
        return user;
    }
    public async Task<User> GetUserByEmailAsync(string? email)
    {
        return await this.userRepository.GetByEmailAsync(email);
    }

    public async Task UpdateUserRoleAsync(User user)
    {
        await userRepository.UpdateUserRoleAsync(user);
    }


    public async Task UpdateUserAsync(User user)
    {
        await this.userRepository.UpdateUserAsync(user);
    }
}
