using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.IServices;
using FinSharkAPI.Models;
using Newtonsoft.Json;

namespace FinSharkAPI.Services
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public FMPService(HttpClient httpClient, IConfiguration configuration, IMapper mapper)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPKey"]}");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];
                    if (stock is not null)
                    {
                        return _mapper.Map<Stock>(stock);
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }
    }
}