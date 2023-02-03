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
        }
    }
}
