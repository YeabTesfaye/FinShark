
using api.Dtos.Comment;

namespace api.Dtos.Stock;

public static class StockMappers
{
    public static StockDto ToStockDto(this api.models.Stock stockModel){
        return new StockDto
        {
            StockId = stockModel.StockId,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
            Commnets = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),

        };
        
    } 

    public static api.models.Stock ToStockFromCreateDto(this CreateStockRequest stockDto){
        return new api.models.Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
    
}