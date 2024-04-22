using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Models;

namespace FinSharkAPI.IRepositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> DeleteAsync(int id);
        Task<Stock?> UpdateAsync(int id,UpdateStockRequestDto stockDto);
        Task<Stock> CreateAsync(Stock stockModel);
    }
}