using AutoMapper;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Data;
using CRM.Data.DTO;

namespace CRM.API.Configuration
{
    public class MappingProfile : Profile
    {       
            public MappingProfile()
            {
                CreateMap<LeadInputModel, LeadDto>()
                     .ForPath(dest => dest.City.Id, o => o.MapFrom(src => src.CityId));

                CreateMap<LeadDto, LeadOutputModel>()
                     .ForPath(dest => dest.City, o => o.MapFrom(src => src.City.Name))
                     .ForPath(dest => dest.Role, o => o.MapFrom(src => src.Role.Name));

                CreateMap<SearchParametersInputModel, LeadSearchParameters>();               
            }
    }
}
