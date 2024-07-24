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
        
    }
