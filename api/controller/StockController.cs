using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using utils;

namespace api.controller;

[ApiController]
[Route("/api/stock")]
public class StockController : ControllerBase 
{
    private readonly IStockRepository _stockRepo;
    public StockController(AppDbContext context, IStockRepository stockRepo)
    {

        _stockRepo = stockRepo;
    }

[HttpGet]
[Authorize]
public async Task<IActionResult> GetStocks([FromQuery] QueryObject query)
{     
        var stocks = await _stockRepo.GetAllAsync(query);
        //Select projection happens after the database call
       var stockDto =   stocks.Select(s => s.ToStockDto()).ToList();
        return Ok(stockDto);
}
// select is the same as how map works in js

[HttpGet("{id}")]
[Authorize]
public async Task<IActionResult> GetStockById([FromRoute] int id){
        var stock = await _stockRepo.GetByIdAsync(id);
        if(stock is null){
            return NotFound();
        }
        return Ok(stock.ToStockDto());
}
[HttpPost]
[Authorize]
  public async Task<IActionResult> Create([FromBody] CreateStockRequest stockDto){
      if(!ModelState.IsValid){
            return BadRequest(ModelState);
      }
      var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetStockById), new { id = stockModel.StockId }, stockModel);
  }
  [HttpPut("{id}")]
  [Authorize]
  public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        
        var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
        if(stockModel is null){
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id){
        var stockModel = await _stockRepo.DeleteAsync(id);
      if(stockModel is null){
            return NotFound();
       }
        return NoContent();

    }
}