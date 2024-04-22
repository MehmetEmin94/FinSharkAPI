using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinSharkAPI.Data;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.IRepositories;
using FinSharkAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinSharkAPI.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly IMapper _mapper;
        public StockController(IStockRepository stockRepo, IMapper mapper )
        {
            _stockRepo = stockRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateStockRequestDto();
            stockModel=await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetSingleStock),new { id=stockModel.Id},_mapper.Map<StockDto>(stockModel));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateStockRequestDto stockDto)
        {
            
            var stock =await _stockRepo.UpdateAsync(id,stockDto);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock =await _stockRepo.DeleteAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks=await _stockRepo.GetAllAsync();
            var stockDto=stocks.Select(s => _mapper.Map<StockDto>(s));
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleStock([FromRoute] int id)
        {
            var stock =await _stockRepo.GetByIdAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StockDto>(stock));
        }
    }
}