using AutoMapper;
using web_panel_api.Dto;
using web_panel_api.Models;

namespace web_panel_api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<ConfirmPayRequest, PayHistory>();
            CreateMap<User, GetReferralDto>();
            CreateMap<ResolveFreeSubDto, UsersTariff>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<PayHistory, GetPayHistoryDto>();
            CreateMap<SettingDto, Setting>();
            CreateMap<AddTariffDto, Tariff>();
            CreateMap<User, UserForStat>();
            CreateMap<PayHistory, PayHistoryForStat>();
        }

    }
}
