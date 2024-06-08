using System.Text.Json.Serialization;

namespace WatchPlus.Models;

public class Log
{
    public Guid Id { get; set; }

    public string? Url { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public DateTime? CreationDate { get; set;}
    public DateTime? EndDate { get; set;}
    public string? StatusCode { get; set;}
    public string? HttpMethod { get; set;}

    
}



