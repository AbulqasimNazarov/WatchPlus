using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#pragma warning disable CS8618
namespace WatchPlus.Dtos;

public class RatingRequest
{
    public Guid FilmId { get; set; }
    public int Rating { get; set; }
}
