using AutoMapper;
using DascoPlasticRecyclingApp.Dto;
using DascoPlasticRecyclingApp.Models;

namespace DascoPlasticRecyclingApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PlasticType, PlasticTypeDto>();
            CreateMap<ContactType, ContactTypeDto>();
            CreateMap<Contact, CContactDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserAccount, CUserAccountDto>();
            CreateMap<UserAccount, SingleUserAccountDto>();

            CreateMap<PlasticTypeDto, PlasticType>();
            CreateMap<ContactTypeDto, ContactType>();
            CreateMap<CContactDto, Contact>();
            CreateMap<UserDto, User>();
            CreateMap<CUserAccountDto, UserAccount>();
            CreateMap<SingleUserAccountDto, UserAccount>();

        }
    }
}
