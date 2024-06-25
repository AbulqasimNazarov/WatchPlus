using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#pragma warning disable CS8618
namespace WatchPlus.Models;

public class Film
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Presentation { get; set; }
    public List<string> Category { get; set; } = new List<string>();
    public string Star { get; set; }
    public string TrailerVideo { get; set; }
    public string Image { get; set; }
    public double Rate { get; set; } 
    public DateTime PresentationDate { get; set; }

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Comment> Comments { get; set; }
}

