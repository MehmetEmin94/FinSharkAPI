using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Data;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public StockController(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateStockRequestDto();
            _dbContext.Add(stockModel);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetSingleStock),new { id=stockModel.Id},stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = _dbContext.Stocks.Find(id);
            if(stock is null)
            {
                return NotFound();
            }
              
            stock=stockDto.ToStockFromUpdateStockRequestDto(stock);

            _dbContext.SaveChanges();
            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stock = _dbContext.Stocks.Find(id);
            if(stock is null)
            {
                return NotFound();
            }
            _dbContext.Stocks.Remove(stock);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _dbContext.Stocks.ToList().Select(s => s.ToStockDto());
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
            return Ok(stock.ToStockDto());
        }
    }
}