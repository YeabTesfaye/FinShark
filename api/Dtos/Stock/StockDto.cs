using api.Dtos.Comment;

namespace api.Dtos.Stock;

public class StockDto
{
public int StockId{ get; set; }
public string? Symbol { get; set; }
public string? CompanyName { get; set; }
public decimal Purchase  { get; set; }
public decimal LastDiv { get; set; }
public string?  Industry { get; set; }
public long MarketCap { get; set; }
public List<CommnetDto> Commnets { get; set; } = [];

}