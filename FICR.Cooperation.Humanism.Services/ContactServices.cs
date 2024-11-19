using FICR.Cooperation.Humanism.Data.Contracts;
using FICR.Cooperation.Humanism.Data.Contracts.Base;
using FICR.Cooperation.Humanism.Entities.DTO.ContactDTO;
using FICR.Cooperation.Humanism.Entities.Pagination;
using FICR.Cooperation.Humanism.Entities;
using FICR.Cooperation.Humanism.Services.Contracts;

namespace FICR.Cooperation.Humanism.Services
{
    public class ContactServices : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;

        public ContactServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._contactRepository = unitOfWork.ContactRepository;
        }

        public async Task<PagedResult<ContactOutPutDTO>> GetAllEvents(PaginationFilter filter)
        {
            var pagedResult = await _contactRepository.FindAllByPagedAsync(filter);

            var contactsDTO = pagedResult.List.Select(contact => new ContactOutPutDTO
            {
                Id = contact.Id,
                name = contact.name,
                number = contact.number
            }).ToList();

            return new PagedResult<ContactOutPutDTO>
            {
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount,
                PageCount = pagedResult.PageCount,
                CurrentPage = pagedResult.CurrentPage,
                List = contactsDTO
            };
        }

        public async Task<ContactOutPutDTO> GetEventsById(Guid id)
        {
            var contact = await _contactRepository.FindByIdAsync(id) ?? throw new ArgumentException("Contact not found.", nameof(id));

            return new ContactOutPutDTO
            {
                Id = contact.Id,
                name = contact.name,
                number = contact.number
            };
        }

        public async Task<ContactOutPutDTO> CreateEvent(ContactInputDTO contactInputDTO)
        {
            if (contactInputDTO == null)
            {
                throw new ArgumentNullException(nameof(contactInputDTO));
            }

            var newContact = new Contact
            {
                name = contactInputDTO.name,
                number = contactInputDTO.number
            };

            await _contactRepository.SaveAsync(newContact);
            await _unitOfWork.SaveChangesAsync();

            return new ContactOutPutDTO
            {
                Id = newContact.Id,
                name = newContact.name,
                number = newContact.number
            };
        }

        public async Task<ContactOutPutDTO> UpdatEvent(ContactOutPutDTO contactOutPutDTO)
        {
            if (contactOutPutDTO == null)
            {
                throw new ArgumentNullException(nameof(contactOutPutDTO));
            }

            var existingContact = await _contactRepository.FindByIdAsync(contactOutPutDTO.Id) ?? throw new ArgumentException("Contact not found.", nameof(contactOutPutDTO.Id));
            existingContact.name = contactOutPutDTO.name;
            existingContact.number = contactOutPutDTO.number;

            _contactRepository.Update(existingContact);
            await _unitOfWork.SaveChangesAsync();

            return new ContactOutPutDTO
            {
                Id = existingContact.Id,
                name = existingContact.name,
                number = existingContact.number
            };
        }

        public async Task DeleteEvento(Guid id)
        {
            var contact = await _contactRepository.FindByIdAsync(id) ?? throw new ArgumentException("Contact not found.", nameof(id));
            _contactRepository.Delete(contact);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
