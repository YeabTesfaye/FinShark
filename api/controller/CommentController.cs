using api.Dtos.Comment;
using api.Extensions;
using api.helper;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.controller;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly UserManager<AppUser> _userManager;
    private readonly IFMService _fMService;
   
    private readonly IStockRepository _stockRepo;
    public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> userManager, IFMService fMService)
    {
        _commentRepo = commentRepo;
        _userManager = userManager;
        _stockRepo = stockRepo;
        _fMService = fMService;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery]CommentQueryObject queryObject){
        var comments = await _commentRepo.GetAllAsync(queryObject);
        var commentDto = comments.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id){
        var comment = await _commentRepo.GetByIdAsync(id);
        string username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        
        if(comment is null){
            return NotFound();
        }
        if(comment.AppUserId != appUser?.Id){
            return Forbid();
        }
        return Ok(comment.ToCommentDto());
    }
    [HttpPost("{symbol:alpha}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var stock = await _stockRepo.GetBySymbolAsync(symbol);
        if(stock == null){
            stock = await _fMService.FindStockBySymbolAsync(symbol);
            if(stock == null){
                return BadRequest("Stokc does not exist");
            }
            else{
                await _stockRepo.CreateAsync(stock);
            }
        }
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
   
        var commentModel = commentDto.ToCommnetFromCreate(stock.StockId);
        commentModel.AppUserId = appUser.Id;
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateCommentRequestDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        string username =  User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var comment = await _commentRepo.GetByIdAsync(id);
        if(comment is null){
            return NotFound();
        }
        if(comment?.AppUserId != appUser?.Id){
            return Forbid();
        }
        var updatedComment = await _commentRepo.UpdateAsync(id, updateCommentRequestDto.ToCommentFromUpdate());
        
        return Ok(updatedComment?.ToCommentDto());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id){
        var comment = await _commentRepo.GetByIdAsync(id);
        if(comment is null){
            NotFound();
        }
        string username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        if(comment?.AppUserId != appUser?.Id){
            return Forbid();
        }
        await _commentRepo.DeleteAsync(id);
        return NoContent();
    }
}