using FICR.Cooperation.Humanism.Controllers;
using FICR.Cooperation.Humanism.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FICR.Cooperation.Humanism.APIs.Controllers
{
    public class SendMessageWhappController : ControllerBase
    {
        private readonly IMenssagemService _menssagemService;

        public SendMessageWhappController(IMenssagemService menssagemService)
        {
            _menssagemService = menssagemService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequestDTO request)
        {
            if (request == null ||request.RecipientNumber.Count() < 0 || string.IsNullOrEmpty(request.MessageBody))
            {
                return BadRequest("Invalid request.");
            }

            // Chamar o serviço para enviar a mensagem
            var variables = request.Variables ?? new Dictionary<string, string>();
            await _menssagemService.SendMessage(request.RecipientNumber, request.MessageBody, variables);

            return Ok("Message sent successfully.");
        }
    }
}
