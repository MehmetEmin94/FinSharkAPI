using FinSharkAPI.Models;
using FinSharkAPI.Dtos.Stock;

namespace FinSharkAPI.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
         return new StockDto{
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap
         };   
        }

        public static Stock ToStockFromCreateStockRequestDto(this CreateStockDto stockDto)
        {
            return new Stock{
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }

        public static Stock ToStockFromUpdateStockRequestDto(this UpdateStockRequestDto stockDto,Stock stock)
        {
                stock.Symbol = stockDto.Symbol;
                stock.CompanyName = stockDto.CompanyName;
                stock.Purchase = stockDto.Purchase;
                stock.LastDiv = stockDto.LastDiv;
                stock.Industry = stockDto.Industry;
                stock.MarketCap = stockDto.MarketCap;
            return stock;
        }
    }
}