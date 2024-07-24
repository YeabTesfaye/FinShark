namespace api.Dtos.Comment;

public class CommnetDto
{
    
public int CommentId { get; set; }
public string? Title { get; set; }
public string? Content { get; set; }
public string? CreatedBy { get; set; }
public DateTime CreatedOn { get; set; } = DateTime.Now;
public int? StockId { get; set; }

}

