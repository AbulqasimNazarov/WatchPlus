namespace WatchPlus.Services.Base;

using Microsoft.AspNetCore.Mvc;
using WatchPlus.Dtos;
using WatchPlus.Models;
using WatchPlus.Validators;

public interface IUserService
{
    public Task CreateNewUserAsync(User newUser, IFormFile image);
    public Task<User> GetUserAsync(string? email);
    public Task<User> GetUserByIdAsync(Guid id);
}
