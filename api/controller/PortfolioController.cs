using api.Extensions;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.controller;

[Route("api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IPortfolioRepository _protfolioRepo;
    private readonly IStockRepository _stockRepo;
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockrepo, IPortfolioRepository protfolioRepo){
        _stockRepo = stockrepo;
        _userManager = userManager;
        _protfolioRepo = protfolioRepo;
    }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _protfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol){
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _stockRepo.GetBySymbolAsync(symbol);
        if (stock is null) return BadRequest("Stock Not Found");
        var userPortfolio = await _protfolioRepo.GetUserPortfolio(appUser);
        if(userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) {
            return BadRequest("Can not add same stock to portfolio");
        }

        var portfolioModel = new Portfolio
        {
            StockId = stock.StockId,
            AppUserId = appUser.Id
        };
       
        await _protfolioRepo.CreateAsync(portfolioModel);
        if (portfolioModel is null) return StatusCode(500);
        else return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol){
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

        var userPortfolio = await _protfolioRepo.GetUserPortfolio(appUser);

        var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

        if(filteredStock.Count == 1){
            await _protfolioRepo.DeletePortfolio(appUser, symbol);
        }
        else{
            return BadRequest("Stock Not in your portofolio");
        }
        return NoContent();
        }
    }
