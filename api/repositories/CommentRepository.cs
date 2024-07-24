using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace api.repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;
    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commnet = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
        if(commnet is null){
            return null;
        }
        _context.Comments.Remove(commnet);
        await _context.SaveChangesAsync();
        return commnet;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.Include(a => a.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        var commnet = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.StockId == id);
        return commnet;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
    {
        var commnet =await _context.Comments.FindAsync(id);
        if(commnet is null){
            return null;
        }
        commnet.Content = commentModel.Content;
        commnet.Title = commentModel.Title;
        await _context.SaveChangesAsync();
        return commnet;

    }

}