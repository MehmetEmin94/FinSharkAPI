using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Models;

namespace FinSharkAPI.Mappers
{
    public class AMapper:Profile
    {
        public AMapper()
        {
            CreateMap<Stock,StockDto>();
            CreateMap<CreateStockRequestDto,Stock>();
            CreateMap<UpdateStockRequestDto,Stock>();
        }
    }
}