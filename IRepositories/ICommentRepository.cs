using FinSharkAPI.Models;

namespace FinSharkAPI.IRepositories
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id,Comment comment);
        Task<Comment?> DeleteAsync(int id);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
    }
}