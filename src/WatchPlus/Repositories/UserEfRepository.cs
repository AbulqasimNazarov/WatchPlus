using Microsoft.EntityFrameworkCore;
using WatchPlus.Data;
using WatchPlus.Dtos;
using WatchPlus.Models;
using WatchPlus.Repositories.Base;

namespace WatchPlus.Repositories;

public class UserEfRepository : IUserRepository
{
    private readonly WatchPlusDbContext dbContext;

    public UserEfRepository(WatchPlusDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task CreatableAsync(User user, IFormFile image)
    {
        user.Id = Guid.NewGuid();
        if (string.IsNullOrEmpty(user.Role)){
            user.Role = "DefaultRole";

        }

        var extension = new FileInfo(image.FileName).Extension[1..];
        user.Image = $"Assets/UsersImg/{user.Id}.{extension}";

        using var newFileStream = System.IO.File.Create(user.Image);
        await image.CopyToAsync(newFileStream);

        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User> GetByEmailAsync(string? email)
    {
        var users = await this.GetAllAsync();
        var user = users?.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return null!;
        }
        return user;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var users = await this.GetAllAsync();
        var user = users?.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return null!;
        }
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserRoleAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }
}
