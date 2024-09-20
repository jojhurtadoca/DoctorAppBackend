using AutoMapper;
using Models.DTO;
using Models.Entities;

namespace Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Specialty, SpecialtyDTO>()
                .ForMember(d => d.Status, m => m.MapFrom(o => o.Status == true ? 1 : 0));
        }
    }
}
