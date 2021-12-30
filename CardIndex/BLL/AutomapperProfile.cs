using AutoMapper;
using BLL.AddModels;
using BLL.VievModels;
using DAL.Entities;

namespace BLL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Article, ArticelVievModel>().ReverseMap();
            CreateMap<Article, ArticleAddmodel>().ReverseMap();
            CreateMap<Theme, ThemeAddModel>().ReverseMap();
            CreateMap<Theme, ThemeVievModel>().ReverseMap();
            CreateMap<ArticleRate, ArticleRateAddModel>().ReverseMap();
            CreateMap<ArticleRate, ArticleRateVievModel>().ReverseMap();
        }
    }
}
