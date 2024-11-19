

using FICR.Cooperation.Humanism.Entities;
using FICR.Cooperation.Humanism.Entities.DTO.ContactDTO;
using FICR.Cooperation.Humanism.Entities.DTO.EventoDTO;
using FICR.Cooperation.Humanism.Entities.Pagination;

namespace FICR.Cooperation.Humanism.Services.Contracts
{
    public interface IContactService
    {
        Task<PagedResult<ContactOutPutDTO>> GetAllEvents(PaginationFilter filter);
        Task<ContactOutPutDTO> GetEventsById(Guid id);
        Task<ContactOutPutDTO> CreateEvent(ContactInputDTO eventInputDTO);
        Task<ContactOutPutDTO> UpdatEvent(ContactOutPutDTO eventInputDTO);
        Task DeleteEvento(Guid Id);

    }
}
