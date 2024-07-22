
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace api.Dtos.Comment;

public class CreateCommentDto
{
    [Required]
    [MinLength(5, ErrorMessage ="Title must be at least five characters")]
    [MaxLength(280, ErrorMessage ="Title Can't be over 280 characters")]
    public string?  Title { get; set; }
    [Required]
    [MinLength(5, ErrorMessage ="Cotent must be at least five characters")]
    [MaxLength(280, ErrorMessage ="Cotent Can't be over 280 characters")]
    public string? Content { get; set; }

   
}