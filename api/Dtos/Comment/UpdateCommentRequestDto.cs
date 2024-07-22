using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class UpdateCommentRequestDto
{
   [Required]
    [MinLength(5, ErrorMessage ="Title must be at least five characters")]
    [MaxLength(280, ErrorMessage ="Title Can't be over 280 characters")]
    public string?  Title { get; set; }
    [Required]
    [MinLength(5, ErrorMessage ="Content must be at least five characters")]
    [MaxLength(280, ErrorMessage ="Content Can't be over 280 characters")]
     public string? Content { get; set; }
}