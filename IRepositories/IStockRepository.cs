using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Helpers;
using FinSharkAPI.Models;

namespace FinSharkAPI.IRepositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock?> DeleteAsync(int id);
        Task<Stock?> UpdateAsync(int id,Stock stock);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<bool> StockExist(int id);
    }
}