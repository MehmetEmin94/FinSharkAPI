using System.IO.Compression;
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

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _dbContext.Comments.AddAsync(commentModel);
            await _dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment= await _dbContext.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            if (comment is null)
            {
                return null;
            }
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _dbContext.Comments.Include(a=>a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment= await _dbContext.Comments.Include(a=>a.AppUser).FirstOrDefaultAsync(c=>c.Id==id);
            if (comment is null)
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var existingComment = await _dbContext.Comments.FirstOrDefaultAsync(c=>c.Id==id);
            if (existingComment is null)
            {
                return null;
            }
            existingComment.Title=comment.Title;
            existingComment.Content=comment.Content;
            
            await _dbContext.SaveChangesAsync();
            return existingComment;
        }
    }
}