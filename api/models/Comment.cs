namespace api.models;

public class Comment
{
    public int CommentId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; } 
    public Stock? Stock { get; set; }
}