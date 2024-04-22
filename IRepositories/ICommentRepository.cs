using FinSharkAPI.Models;

namespace FinSharkAPI.IRepositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
    }
}