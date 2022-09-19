using AutoMapper;
using Events.Models;

namespace Events.Repositories {
    public class EFEventsRepository : IEventsRepository {
        private EventsDbContext _context;
        private IMapper _mapper;

        public EFEventsRepository(EventsDbContext ctx, IMapper mapper) {
            _context = ctx;
            _mapper = mapper;
        }

        public IQueryable<Event> Events => _context.Events;

        public EventDto? GetEvent(int id) {
            Event? evt = _context.Events.FirstOrDefault(ev => ev.Id == id);
            if (evt == null)
                return null;
            else {
                EventDto eventDto = _mapper.Map<EventDto>(evt);
                return eventDto;
            }
        }

        public EventDto? AddEvent(EventDto evtDto) {
            Event evt = _mapper.Map<Event>(evtDto);
            Console.WriteLine(evt.Id);
            _context.Events.Add(evt);
            Console.WriteLine(evt.Id);
            _context.SaveChanges();
            Event evtCur = _context.Events.ToList().Last();
            return _mapper.Map<EventDto>(evtCur);
        }

        public EventDto? UpdateEvent(EventDto evtDto) {
            Event evt = _mapper.Map<Event>(evtDto);
            if (!_context.Events.Any(ev => ev.Id == evt.Id)) {
                return null;
            }
            _context.Update(evt);
            _context.SaveChanges();
            return evtDto;
        }

        public EventDto? DeleteEvent(int id) {
            Event? evt = _context.Events.FirstOrDefault(ev => ev.Id == id);
            if (evt is null) {
                return null;
            }
            _context.Remove(evt);
            _context.SaveChanges();
            EventDto evtDto = _mapper.Map<EventDto>(evt);
            return evtDto;
        }
    }
}
