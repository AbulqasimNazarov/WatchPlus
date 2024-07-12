using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchPlus.Models;

public class Rating
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public int Value { get; set; }
    
    [Required]
    public Guid FilmId { get; set; }
    
    [ForeignKey("FilmId")]
    public Film Film { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
}



// public class Rating
// {
//     public Guid Id { get; set; }
//     public Guid FilmId { get; set; }
//     public int Value { get; set; }
//     public DateTime Date { get; set; } = DateTime.UtcNow;
//     public Guid UserId { get; set; }
//     public Film Film { get; set; }
// }
