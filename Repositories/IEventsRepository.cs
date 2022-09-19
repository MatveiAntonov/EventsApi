using Events.Models;

namespace Events.Repositories {
    public interface IEventsRepository {
        IQueryable<Event> Events { get; }
        EventDto? GetEvent(int id);
        EventDto? AddEvent(EventDto evtDto);
        EventDto? UpdateEvent(EventDto evtDto);
        EventDto? DeleteEvent(int id);
    }
}
