using EventMangamentAPI.ViewModel;

namespace EventMangamentAPI.Service.Interface
{
    public interface IEventService
    {
        bool CreateEvent(CreateEventVM request, out string errorMessage);
        List<EventVM> GetAllEvents(out string errorMessage);
        EventVM GetEventById(int id, out string errorMessage);
        bool UpdateEvent(int id, UpdateEventVM request, out string errorMessage);
        bool DeleteEvent(int id, out string errorMessage);
    }
}
