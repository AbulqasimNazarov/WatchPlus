using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchPlus.Models;

public class TvShowRating
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public int Value { get; set; }
    
    [Required]
    public Guid FilmId { get; set; }
    
    [ForeignKey("TvShowId")]
    public TvSHow Film { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
}
