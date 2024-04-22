using AutoMapper;
using FinSharkAPI.Dtos.Comment;
using FinSharkAPI.Dtos.Stock;
using FinSharkAPI.Models;

namespace FinSharkAPI.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Stock,StockDto>();
            CreateMap<CreateStockRequestDto,Stock>();
            CreateMap<UpdateStockRequestDto,Stock>();

            CreateMap<Comment,CommentDto>();
        }
    }
}