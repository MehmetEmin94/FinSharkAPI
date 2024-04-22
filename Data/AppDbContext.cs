using FinSharkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinSharkAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}