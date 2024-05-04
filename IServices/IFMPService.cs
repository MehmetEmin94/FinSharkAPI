using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Models;

namespace FinSharkAPI.IServices
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}