using AutoMapper;
using EventMangamentAPI.Entities;
using EventMangamentAPI.ViewModel;
namespace EventMangamentAPI.MapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventVM>()
                .ForMember(dest => dest.NameLenght, opt => opt.MapFrom(src => src.Name.Length));
            CreateMap<Organizer, OrganizerVM>();
            CreateMap<Participant, ParticipantVM>();
            CreateMap<Registration, RegistrationVM>();
            CreateMap<Review, ReviewVM>();
        }
    }

}
