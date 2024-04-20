using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Data;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateStockRequestDto();
            await _dbContext.AddAsync(stockModel);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSingleStock),new { id=stockModel.Id},stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateStockRequestDto stockDto)
        {
            var stock =await _dbContext.Stocks.FindAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
              
            stock=stockDto.ToStockFromUpdateStockRequestDto(stock);

            await _dbContext.SaveChangesAsync();
            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock =await _dbContext.Stocks.FindAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            _dbContext.Stocks.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks =await _dbContext.Stocks.ToListAsync();
            var stockDto=stocks.Select(s => s.ToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleStock([FromRoute] int id)
        {
            var stock =await _dbContext.Stocks.FindAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
    }
}