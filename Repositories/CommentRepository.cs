using FinSharkAPI.Data;
using FinSharkAPI.IRepositories;
using FinSharkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinSharkAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _dbContext;

        public CommentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _dbContext.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment= await _dbContext.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            if (comment is null)
            {
                return null;
            }
            return comment;
        }
    }
}