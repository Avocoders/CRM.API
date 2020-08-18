using AutoMapper;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.Core;
using CRM.Data;
using CRM.Data.DTO;
using System;
using System.Globalization;

namespace CRM.API.Configuration
{
    public class MappingProfile : Profile
    {       
         public MappingProfile()
         {
            CreateMap<AccountDto, AccountOutputModel>()
                 .ForPath(dest => dest.CurrencyCode, o => o.MapFrom(src => Enum.GetName(typeof(CurrenciesCode), src.CurrencyId)))
                 .ForPath(dest => dest.CurrencyName, o => o.MapFrom(src => Enum.GetName(typeof(CurrenciesName), src.CurrencyId)));

            CreateMap<LeadInputModel, LeadDto>()
                 .ForPath(dest => dest.City.Id, o => o.MapFrom(src => src.CityId))
                 .ForPath(dest => dest.BirthDate, o => o.MapFrom(src => DateTime.ParseExact(src.BirthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture)));                  
            
            CreateMap<PasswordInputModel, PasswordDto>()
                 .ForPath(dest => dest.Id, o => o.MapFrom(src => src.Id))
                 .ForPath(dest => dest.Password, o => o.MapFrom(src => src.Password));

            CreateMap<EmailInputModel, EmailDto>()
                 .ForPath(dest => dest.LeadId, o => o.MapFrom(src => src.LeadId))
                 .ForPath(dest => dest.Email, o => o.MapFrom(src => src.Email));

            CreateMap<LeadDto, LeadOutputModel>()
                  .ForPath(dest => dest.City, o => o.MapFrom(src => src.City.Name))
                  .ForPath(dest => dest.Role, o => o.MapFrom(src => src.Role.Name))
                  .ForPath(dest => dest.BirthDate, o => o.MapFrom(src => src.BirthDate.ToString("dd.MM.yyyy")))
                  .ForPath(dest => dest.ChangeDate, o => o.MapFrom(src => src.ChangeDate.ToString("dd.MM.yyyy HH:mm:ss")))
                  .ForPath(dest => dest.Accounts, o => o.MapFrom(src => src.Accounts))
                  .ForPath(dest => dest.RegistrationDate, o => o.MapFrom(src => src.RegistrationDate.ToString("dd.MM.yyyy HH:mm:ss")));

             CreateMap<SearchParametersInputModel, LeadSearchParameters>()
                 .ForPath(dest => dest.RegistrationDateBegin, o => o.MapFrom(src => DateTime.ParseExact(src.RegistrationDateBegin, "dd.MM.yyyy", CultureInfo.InvariantCulture)))
                 .ForPath(dest => dest.RegistrationDateEnd, o => o.MapFrom(src => DateTime.ParseExact(src.RegistrationDateEnd, "dd.MM.yyyy", CultureInfo.InvariantCulture)))
                 .ForPath(dest => dest.BirthDateEnd, o => o.MapFrom(src => DateTime.ParseExact(src.BirthDateEnd, "dd.MM.yyyy", CultureInfo.InvariantCulture)))
                 .ForPath(dest => dest.BirthDateBegin, o => o.MapFrom(src => DateTime.ParseExact(src.BirthDateBegin, "dd.MM.yyyy", CultureInfo.InvariantCulture)));

            CreateMap<AccountInputModel, AccountDto>();

            CreateMap<AccountWithLeadDto, AccountWithLeadOutputModel>()
                 .ForMember(dest => dest.BirthDate, o => o.MapFrom(src => src.BirthDate.ToString("dd.MM.yyyy")))
                 .ForMember(dest => dest.CurrencyCode, o => o.MapFrom(src => Enum.GetName(typeof(CurrenciesCode), src.CurrencyId)))
                 .ForMember(dest => dest.CurrencyName, o => o.MapFrom(src => Enum.GetName(typeof(CurrenciesName), src.CurrencyId)));
        }
    }
}
