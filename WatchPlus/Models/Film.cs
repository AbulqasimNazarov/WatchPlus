using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WatchPlus.Models;

public class Film 
{
    public Guid Id { get; set; }
    public string? presentationDate { get; set; }
    public string? Name { get; set; }
    public string? Presentation { get; set; }
    public string? Category { get; set; } 
    public string? Star { get; set; }
    public long? Rate { get; set; }
    public string? Image { get; set; }
    public string? TrailerVideo { get; set; }
}
