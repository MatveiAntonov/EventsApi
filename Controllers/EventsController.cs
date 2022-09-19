using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Events.Repositories;
using AutoMapper;

namespace Events.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase {
        private IEventsRepository _repository;
        private IMapper _mapper;

        public EventsController(IEventsRepository repo, IMapper mapper) { 
           this._repository = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns the full list of events from the database
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get() {
            return await _repository.Events.ToListAsync();
        }

        /// <summary>
        /// Returns certain EventDto-object by id from the database
        /// </summary>
        /// <param name="id">Id of the specific user to return</param>
        [HttpGet("{id}")]
        public ActionResult<EventDto> Get(int id) {
            EventDto? evt = _repository.GetEvent(id);
            return evt is not null ? new ObjectResult(evt) : NotFound(); 
        }

        /// <summary>
        /// Adds certain Event-object to the database
        /// </summary>
        /// <param name="evtDto">The EventDto-object required to be added to the database</param>
        [HttpPost]
        public ActionResult<EventDto> Post(EventDto evtDto) {
            if (evtDto == null)
                return BadRequest();            
            EventDto? evtCur = _repository.AddEvent(evtDto);
            if (evtCur is null)
                return StatusCode(500);
            return new ObjectResult(evtCur);
        }

        /// <summary>
        /// Edit certain Event-object in the database
        /// </summary>
        /// <param name="evtDto">The EventDto-object required to be edited to the database</param>
        [HttpPut] 
        public ActionResult<EventDto> Put(EventDto evtDto) {
            if (evtDto == null)
                return BadRequest();
            var evtRes = _repository.UpdateEvent(evtDto);
            if (evtRes is null)
                return NotFound();
            return new ObjectResult(evtRes);

        }

        /// <summary>
        /// Delete certain Event-object by id from the database
        /// </summary>
        /// <param name="id">Id of the specific user to delete</param>
        [HttpDelete("{id}")]
        public ActionResult<EventDto> Delete(int id) {
            var evtRes = _repository.DeleteEvent(id);
            if (evtRes is null)
                return NotFound();           
            return new ObjectResult(evtRes);
        }
    }
}
