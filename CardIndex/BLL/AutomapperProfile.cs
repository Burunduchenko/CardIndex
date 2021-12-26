using AutoMapper;
using DAL.Entities;
using BLL.Models;
using System;
using System.Linq;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Article, ArticleModel>()
                .ForMember(p => p.ThemeId, c => c.MapFrom(card => card.ThemeId))
                .ForMember(p => p.ArticleRatesIds, c => c.MapFrom(card => card.ArticleRates.Select(x => x.Id)))
                .ForMember(p => p.AvgRate, c => c.MapFrom(card => card.ArticleRates.Select(x => x.Rate).Average()))
                .ReverseMap();
                

            CreateMap<ArticleRate, ArticleRateModel>().ReverseMap();

            CreateMap<User, UserModel>()
                .ForMember(p=>p.ArticleRatesIds, c=>c.MapFrom(user => user.ArticleRates.Select(x=>x.Id)))
                .ReverseMap();

            CreateMap<Theme, ThemeModel>().ReverseMap();
        }
    }
}
