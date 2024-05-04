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
            CreateMap<FMPStock,Stock>()
               .ForMember(a=>a.Symbol,opt=>opt.MapFrom(src=>src.symbol))
               .ForMember(a=>a.CompanyName,opt=>opt.MapFrom(src=>src.companyName))
               .ForMember(a=>a.Purchase,opt=>opt.MapFrom(src=>(decimal)src.price))
               .ForMember(a=>a.LastDiv,opt=>opt.MapFrom(src=>(decimal)src.lastDiv))
               .ForMember(a=>a.Industry,opt=>opt.MapFrom(src=>src.industry))
               .ForMember(a=>a.MarketCap,opt=>opt.MapFrom(src=>src.mktCap));
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