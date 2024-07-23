using api.Dtos.Comment;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Mvc;

namespace api.controller;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
   
    private readonly IStockRepository _stockRepo;
    public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
    {
        _commentRepo = commentRepo;
        _stockRepo = stockRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var comments = await _commentRepo.GetAllAsync();
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var commnet = await _commentRepo.GetByIdAsync(id);
        if(commnet is null){
            return NotFound();
        }
        return Ok(commnet.ToCommentDto());
    }
    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        if(!await _stockRepo.StockExists(stockId)){
            return BadRequest("Stock does not exist");
        }

        var commentModel = commentDto.ToCommnetFromCreate(stockId);
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
    }
    [HttpPut("{id}")]

    public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateCommentRequestDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        var commnet = await _commentRepo.UpdateAsync(id,updateCommentRequestDto.ToCommentFromUpdate());
        if(commnet is null){
            return NotFound();
        }
        return Ok(commnet.ToCommentDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id){
        var commnet = await _commentRepo.DeleteAsync(id);
        if(commnet is null){
            return NotFound();
        }
        return NoContent();
    }
}