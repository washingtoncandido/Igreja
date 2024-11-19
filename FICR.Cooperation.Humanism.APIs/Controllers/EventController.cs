using FICR.Cooperation.Humanism.Entities;
using FICR.Cooperation.Humanism.Entities.DTO.EventoDTO;
using FICR.Cooperation.Humanism.Entities.Pagination;
using FICR.Cooperation.Humanism.Services;
using Microsoft.AspNetCore.Mvc;

namespace FICR.Cooperation.Humanism.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService _eventService)
        {
            this._eventService = _eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Event>>> GetAllEvents([FromQuery] PaginationFilter filter)
        {
            var events = await _eventService.GetAllEvents(filter);
            return Ok(events);
        }

        [HttpGet("GetEventsById")]
        public async Task<IActionResult> GetEventsById([FromQuery] Guid id) 
        {
            var events = await _eventService.GetEventsById(id);
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventInputDTO eventInputDto)
        {
            var newEvent = await _eventService.CreateEvent(eventInputDto);
            return Ok(newEvent);
        }

        [HttpPut]
        public async Task<IActionResult> UpadtEvent([FromBody] EventOutputDTO eventInputDto)
        {
            var newEvent = await _eventService.UpdatEvent(eventInputDto);
            return Ok(newEvent);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteEvento(Guid Id)
        {
            try
            {
                await _eventService.DeleteEvento(Id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
             
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Um erro ocorreu ao tentar excluir o aluno.");
            }
        }

    }
}
