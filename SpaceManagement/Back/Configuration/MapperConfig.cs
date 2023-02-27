using AutoMapper;
using SpaceManagment.DTO;
using SpaceManagment.Model;

namespace SpaceManagment.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<HostDto,
                Model.Host>().
                ForMember(p => p.Spaces, opt => opt.Ignore()).
                ForMember(p => p.Supervisors, opt => opt.Ignore());

            CreateMap<Model.Host,
               HostDto>();

            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.Reservation, opt => opt.MapFrom(src => src.Reservations)).ReverseMap();

            CreateMap<Space, SpaceDto>().ReverseMap().ForMember(p=>p.Id,opt=>opt.Ignore());

            CreateMap<User, UserDto>().ReverseMap().ForMember(p=>p.Id,opt=>opt.Ignore());

            CreateMap<Supervisor,
               SupervisorDto>().ReverseMap().
               ForMember(p => p.Reservations, opt => opt.Ignore()).
               ForMember(p => p.SpaceSupervisors, opt => opt.Ignore()).
               ForMember(p => p.Host, opt => opt.Ignore());

            CreateMap<User,
             UserDto>().ReverseMap();
        }
    }
}
