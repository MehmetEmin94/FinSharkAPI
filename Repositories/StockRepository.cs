using AutoMapper;
using FinSharkAPI.Data;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Helpers;
using FinSharkAPI.IRepositories;
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks =  _dbContext.Stocks.Include(s=>s.Comments).ThenInclude(a=>a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecsending ? stocks.OrderByDescending(s=>s.Symbol) : stocks.OrderBy(s=>s.Symbol); 
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
           return await _dbContext.Stocks.Include(s=>s.Comments).ThenInclude(a=>a.AppUser).FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _dbContext.Stocks.Include(s=>s.Comments).FirstOrDefaultAsync(s=>s.Symbol==symbol);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _dbContext.Stocks.AnyAsync(s=>s.Id==id);
        }

        public async Task<Stock?> UpdateAsync(int id, Stock stock)
        {
            var existingStock =await _dbContext.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
            if(existingStock is null)
            {
                return null;
            }
            existingStock.CompanyName=stock.CompanyName;
            existingStock.Industry=stock.Industry;
            existingStock.LastDiv=stock.LastDiv;
            existingStock.MarketCap=stock.MarketCap;
            existingStock.Purchase=stock.Purchase;
            existingStock.Symbol=stock.Symbol;

            await _dbContext.SaveChangesAsync();
            return existingStock;
        }
    }
}