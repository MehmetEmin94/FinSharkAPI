using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Models;

namespace FinSharkAPI.IRepositories
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolioModel);
        Task<Portfolio?> DeleteAsync(AppUser user,string symbol);
    }
}