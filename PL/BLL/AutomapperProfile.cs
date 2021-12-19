using AutoMapper;
using DAL.Entities;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Article, ArticleModel>()
                .ForMember(p => p.ThemesIds, c => c.MapFrom(card => card.Themes.Select(x => x.Id)))
                .ForMember(p => p.ArticleRatesIds, c => c.MapFrom(card => card.ArticleRates.Select(x => x.Id)))
                .ReverseMap();

            CreateMap<ArticleRate, ArticleRateModel>().ReverseMap();
                
            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<Theme, ThemeModel>().ReverseMap();
        }
    }
}
