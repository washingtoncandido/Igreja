using FICR.Cooperation.Humanism.Entities;
using FICR.Cooperation.Humanism.Entities.DTO.EventoDTO;
using FICR.Cooperation.Humanism.Entities.Pagination;

namespace FICR.Cooperation.Humanism.Services
{
    public interface IEventService
    {
        Task<PagedResult<EventOutputDTO>> GetAllEvents(PaginationFilter filter);
        Task<EventOutputDTO> GetEventsById(Guid id);
        Task<EventOutputDTO> CreateEvent(EventInputDTO eventInputDTO);
        Task<EventOutputDTO> UpdatEvent(EventOutputDTO eventInputDTO);
        Task DeleteEvento(Guid Id);

    }
}
