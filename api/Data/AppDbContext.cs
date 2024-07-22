using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    public required DbSet<Stock> Stocks { get; set; }
    public required DbSet<Comment> Comments { get; set; }
}