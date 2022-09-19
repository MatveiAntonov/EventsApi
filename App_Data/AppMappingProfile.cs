using AutoMapper;
using Events.Models;

namespace Events.App_Data {
    public class AppMappingProfile : Profile{
        public AppMappingProfile() {
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
