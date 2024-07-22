using System.ComponentModel.DataAnnotations.Schema;

namespace api.models;

public class Stock
{
    public int StockId{ get; set; }
    public string? Symbol { get; set; }
    public string? CompanyName { get; set; }
    [Column(TypeName ="decimal(18,2)")]
    public decimal Purchase  { get; set; }
     [Column(TypeName ="decimal(18,2)")]
    public decimal LastDiv { get; set; }
    public string?  Industry { get; set; }
    public long MarketCap { get; set; }
    public  List<Comment> Comments { get; set; } = [];
}

// The `Stock` and `Comment` models interact through a one-to-many relationship

