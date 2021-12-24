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
                .ReverseMap();

            CreateMap<ArticleRate, ArticleRateModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Theme, ThemeModel>().ReverseMap();
        }
    }
}
