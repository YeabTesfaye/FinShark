using System.ComponentModel.DataAnnotations.Schema;

namespace api.models;

[Table("Comments")]
public class Comment
{
    public int CommentId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; } 
    public Stock? Stock { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}