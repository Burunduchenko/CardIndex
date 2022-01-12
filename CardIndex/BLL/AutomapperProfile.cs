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
            CreateMap<Card, CardVievModel>().ReverseMap();
            CreateMap<Card, CardAddmodel>().ReverseMap();
            CreateMap<Theme, ThemeAddModel>().ReverseMap();
            CreateMap<Theme, ThemeVievModel>().ReverseMap();
            CreateMap<CardAssessment, CardAssessmentAddModel>().ReverseMap();
            CreateMap<CardAssessment, CardAssementVievModel>().ReverseMap();
        }
    }
}
