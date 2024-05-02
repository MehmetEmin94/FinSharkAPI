using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Data;
using FinSharkAPI.IRepositories;
using FinSharkAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinSharkAPI.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext _dbContext;

        public PortfolioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolioModel)
        {
            await _dbContext.Portfolios.AddAsync(portfolioModel);
            await _dbContext.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<Portfolio?> DeleteAsync(AppUser user,string symbol)
        {
            var portfolio =await _dbContext.Portfolios.FirstOrDefaultAsync(s=>s.AppUserId==user.Id && s.Stock.Symbol.ToLower() == symbol.ToLower());
            if(portfolio is null)
            {
                return null;
            }
            _dbContext.Portfolios.Remove(portfolio);
            await _dbContext.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _dbContext.Portfolios.Where(u => u.AppUserId == user.Id)
                   .Select(s => new Stock
                   {
                      Id = s.StockId,
                      Symbol = s.Stock.Symbol,
                      CompanyName = s.Stock.CompanyName,
                      Purchase = s.Stock.Purchase,
                      LastDiv = s.Stock.LastDiv,
                      Industry = s.Stock.Industry,
                      MarketCap = s.Stock.MarketCap
                   }).ToListAsync();
        }
    }
}