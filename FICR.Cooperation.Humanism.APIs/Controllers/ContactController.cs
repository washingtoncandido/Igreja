using Microsoft.AspNetCore.Mvc;
using FICR.Cooperation.Humanism.Services.Contracts;
using FICR.Cooperation.Humanism.Entities.DTO.ContactDTO;
using FICR.Cooperation.Humanism.Entities.Pagination;


namespace FICR.Cooperation.Humanism.APIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // Endpoint para listar todos os contatos com paginação
        [HttpGet]
        public async Task<IActionResult> GetAllContacts([FromQuery] PaginationFilter filter)
        {
            var result = await _contactService.GetAllEvents(filter);
            return Ok(result);
        }

        // Endpoint para buscar um contato por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            try
            {
                var result = await _contactService.GetEventsById(id);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return NotFound($"Contato com ID {id} não encontrado.");
            }
        }

        // Endpoint para criar um novo contato
        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactInputDTO contactInputDTO)
        {
            if (contactInputDTO == null)
            {
                return BadRequest("Os dados do contato são necessários.");
            }

            var result = await _contactService.CreateEvent(contactInputDTO);
            return CreatedAtAction(nameof(GetContactById), new { id = result.Id }, result);
        }

        // Endpoint para atualizar um contato existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact( [FromBody] ContactOutPutDTO contactOutPutDTO)
        {
            if (contactOutPutDTO == null || contactOutPutDTO.Id == null)
            {
                return BadRequest("Os dados fornecidos são inválidos.");
            }

            try
            {
                var result = await _contactService.UpdatEvent(contactOutPutDTO);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return NotFound($"Contato com ID {contactOutPutDTO.Id} não encontrado.");
            }
        }

        // Endpoint para excluir um contato
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            try
            {
                await _contactService.DeleteEvento(id);
                return NoContent(); // Retorna 204 No Content para uma exclusão bem-sucedida
            }
            catch (ArgumentException)
            {
                return NotFound($"Contato com ID {id} não encontrado.");
            }
        }
    }
}
