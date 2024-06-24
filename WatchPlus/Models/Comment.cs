namespace WatchPlus.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string UserImage { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    // public Guid Id { get; set; }
    // public string Text { get; set; }
    // public DateTime CreatedDate { get; set;}
    // public Guid UserId { get; set; }
    // public Guid FilmId { get; set; }
     public User User { get; set; }
     public Film Film { get; set; }
}
