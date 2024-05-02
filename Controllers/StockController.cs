using AutoMapper;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Helpers;
using FinSharkAPI.IRepositories;
using FinSharkAPI.Mappers;
using FinSharkAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = _mapper.Map<Stock>(stockDto);
            stockModel=await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById),new { id=stockModel.Id},_mapper.Map<StockDto>(stockModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var stock=_mapper.Map<Stock>(stockDto);
            stock =await _stockRepo.UpdateAsync(id,stock);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock =await _stockRepo.DeleteAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StockDto>(stock));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks=await _stockRepo.GetAllAsync(query);
            var stockDto=_mapper.Map<List<StockDto>>(stocks);
            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock =await _stockRepo.GetByIdAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<StockDto>(stock));
        }
    }
}