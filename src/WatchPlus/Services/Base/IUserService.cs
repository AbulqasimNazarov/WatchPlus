namespace WatchPlus.Services.Base;

using Microsoft.AspNetCore.Mvc;
using WatchPlus.Dtos;
using WatchPlus.Models;
using WatchPlus.Validators;

public interface IUserService
{
    public Task CreateNewUserAsync(User newUser, IFormFile image);
    
    public Task<User> GetUserByIdAsync(Guid id);
    public Task<User> GetUserByEmailAsync(string email);
    public Task UpdateUserRoleAsync(User user);
    public Task UpdateUserAsync(User user);
}
