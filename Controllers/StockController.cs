using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkAPI.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public StockController(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _dbContext.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingleStock([FromRoute] int id)
        {
            var stock = _dbContext.Stocks.Find(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
}