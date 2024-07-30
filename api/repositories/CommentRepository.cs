using api.Data;
using api.Dtos.Comment;
using api.helper;
using api.Interfaces;
using api.models;

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

    public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
    {
        var comments =  _context.Comments.Include(a => a.AppUser).AsQueryable();
        if(!string.IsNullOrWhiteSpace(queryObject.Symbol)){
            comments = comments.Where(s => s.Stock.Symbol == queryObject.Symbol);
        }
        if(queryObject.IsDecsending == true){
            comments = comments.OrderByDescending(c => c.CreatedOn);
        }

        return await comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        var commnet = await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.CommentId == id);
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