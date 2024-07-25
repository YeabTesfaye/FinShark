using api.Dtos.Comment;
using api.Dtos.Stock;
using api.models;
using utils;


namespace api.Interfaces;

public interface IStockRepository
{

    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
    Task<Stock?> GetBySymbolAsync(string symbol);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
    Task<Stock?> GetStockWithOwnerAsync(int stockId);
}