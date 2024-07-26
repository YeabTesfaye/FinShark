using api.models;

namespace api.Interfaces;

public interface IFMService
{
    Task<Stock?> FindStockBySymbolAsync(string symbol);
}