#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;

namespace WatchPlus.Models;


[Index(nameof(Email), IsUnique = true)]
public class User
{
    public Guid Id { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsBlocked { get; set; } = false;
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<Comment> Comments { get; set; }

}
