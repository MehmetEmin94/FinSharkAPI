using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkAPI.Extensions;
using FinSharkAPI.IRepositories;
using FinSharkAPI.IServices;
using FinSharkAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkAPI.Controllers
{
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fmpService;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, 
                                   IPortfolioRepository portfolioRepository, IFMPService fmpService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock =  await _stockRepository.GetBySymbolAsync(symbol);
            
            if (stock is null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock is null)
                {
                    return BadRequest("Stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateAsync(stock);
                }
            }

            if (stock is null)
                return BadRequest("Stock not found");

            var userPortfolios = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolios.Any(p=>p.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            portfolioModel = await _portfolioRepository.CreateAsync(portfolioModel);

            if(portfolioModel is null)
            {
                return StatusCode(500,"Could not create");
            }

            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var portfolio = await _portfolioRepository.DeleteAsync(appUser,symbol);

            if (portfolio is null)
            {
                return BadRequest("Portfolio not exist");
            }
            return Ok();
        }
    }
}