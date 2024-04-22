using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinSharkAPI.Data;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.IRepositories;
using FinSharkAPI.Mappers;
using FinSharkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinSharkAPI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _dbContext;
         private readonly IMapper _mapper;

        public StockRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _dbContext.Stocks.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock =await _dbContext.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
            if(stock is null)
            {
                return null;
            }
            _dbContext.Stocks.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _dbContext.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
           return await _dbContext.Stocks.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock =await _dbContext.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
            if(existingStock is null)
            {
                return null;
            }
           _mapper.Map(stockDto,existingStock);
            await _dbContext.SaveChangesAsync();
            return existingStock;
        }
    }
}