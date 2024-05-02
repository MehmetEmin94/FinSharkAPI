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
            CreateMap<Stock,Stock>();
            CreateMap<CreateStockDto,Stock>();
            CreateMap<UpdateStockRequestDto,Stock>();

            CreateMap<Comment,CommentDto>().ForMember(a=>a.CreatedBy,opt=>opt.MapFrom(src=>src.AppUser.UserName));
            CreateMap<Comment,Comment>();
            CreateMap<CreateCommentDto,Comment>();
            CreateMap<UpdateCommentRequestDto,Comment>();
        }
    }
}