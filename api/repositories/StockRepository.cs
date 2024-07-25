using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using utils;

namespace api.repositories;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _contex;
    public StockRepository(AppDbContext context)
    {
        _contex = context;
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _contex.Stocks.AddAsync(stockModel);
        await _contex.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _contex.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.StockId == id);
        if(stock is null){
            return null;
        }
        _contex.Comments.RemoveRange(stock.Comments);
        _contex.Stocks.Remove(stock);
       await _contex.SaveChangesAsync();
        return stock;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks =  _contex.Stocks.Include(c => c.Comments).ThenInclude(a =>a.AppUser).AsQueryable();
        if(!string.IsNullOrWhiteSpace(query.CompanyName)){
           stocks = stocks.Where(s => s.CompanyName != null && s.CompanyName.Contains(query.CompanyName));
        }
        if(!string.IsNullOrWhiteSpace(query.Symbol)){
        stocks = stocks.Where(s => s.Symbol != null && s.Symbol.Contains(query.Symbol));
        }
        if(!string.IsNullOrWhiteSpace(query.SortBy)){
            if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)){
                stocks = query.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }
        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public Task<Stock?> GetByIdAsync(int id)
    {
        return _contex.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.StockId == id);
    }

    public async Task<Stock?> GetBySymbolAsync(string symbol)
    {
        return await _contex.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
    }

    public async Task<Stock?> GetStockWithOwnerAsync(int stockId)
    {
        return await _contex.Stocks.FirstOrDefaultAsync(s => s.StockId == stockId);
    }

    public Task<bool> StockExists(int id)
    {
        return _contex.Stocks.AnyAsync(s => s.StockId == id); 
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
    {
        var stock = await _contex.Stocks.FirstOrDefaultAsync(s => s.StockId == id);
        if(stock is null){
            return null;
        }
         stock.MarketCap = stockDto.MarketCap;
        stock.CompanyName = stockDto.CompanyName;
        stock.Industry = stockDto.Industry;
        stock.LastDiv = stockDto.LastDiv;
        stock.Purchase = stockDto.Purchase;
        stock.Symbol = stockDto.Symbol;
        await _contex.SaveChangesAsync();
        return stock;
    }
}