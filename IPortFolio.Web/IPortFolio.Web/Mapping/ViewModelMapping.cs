using AutoMapper;
using IPortFolio.Models;
using IPortFolio.ViewModels;

namespace IPortFolio.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping() {

            CreateMap<Slider, SliderViewModel>().ReverseMap();
            CreateMap<User, ProfileViewModel>().ReverseMap();
            CreateMap<About, AboutViewModel>().ReverseMap();
            CreateMap<Skill, SkillViewModel>().ReverseMap();
            CreateMap<Resume, ResumeViewModel>().ReverseMap();
            CreateMap<Portfolio, PortfolioViewModel>().ReverseMap();
            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Login, LoginViewModel>().ReverseMap();

        }

    }
}
