using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequest
    {
      [Required]
      [MaxLength(10, ErrorMessage ="Symbol Cannot be over 10 charactes")]  
    public string? Symbol { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage ="Company Name can't be more than 10 characters")]
    public string? CompanyName { get; set; }
    [Required]
    [Range(1,100000000000000000)]
    public decimal Purchase  { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage ="Industry cannot be over 10 characters ")]
    public string?  Industry { get; set; }
    [Required]
    [Range(1,1000000000000000000)]
    public long MarketCap { get; set; }
    }
}