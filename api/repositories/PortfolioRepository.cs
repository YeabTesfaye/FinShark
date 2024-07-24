using api.Data;
using api.Interfaces;
using api.models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly AppDbContext _context;
    public PortfolioRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
    {
        var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id 
        && x.Stock.Symbol.ToLower() == symbol.ToLower());

        if (portfolioModel is null) return null;

        _context.Portfolios.Remove(portfolioModel);
        await _context.SaveChangesAsync();
        return portfolioModel;
    }
    public async Task<Portfolio> CreateAsync(Portfolio portfolio){
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }

    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
        .Select(stock => new Stock
        {
            StockId = stock.StockId,
            Symbol = stock.Stock.Symbol,
            CompanyName = stock.Stock.CompanyName,
            Purchase = stock.Stock.Purchase,
            LastDiv = stock.Stock.LastDiv,
            Industry = stock.Stock.Industry,
            MarketCap = stock.Stock.MarketCap
        }).ToListAsync();
    }
}