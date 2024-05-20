using System.Text.Json.Serialization;

namespace WatchPlus.Models;

public class TvSHow
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; } 
    public string? Star { get; set; }
    public string? Rate { get; set; }
    public string? Image { get; set; }
    public string? VideoTrailer { get; set; }
}
