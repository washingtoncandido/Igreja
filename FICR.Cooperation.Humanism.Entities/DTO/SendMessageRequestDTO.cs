using System.ComponentModel.DataAnnotations;

namespace FICR.Cooperation.Humanism.Controllers
{
    public class SendMessageRequestDTO
    {
        [Required(ErrorMessage = "O número do destinatário é obrigatório.")]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public List<string> RecipientNumber { get; set; }

        [Required(ErrorMessage = "O corpo da mensagem é obrigatório.")]
        public string MessageBody { get; set; }

        public Dictionary<string, string> Variables { get; set; }

        public string mediaUrl { get; set; }
    }
}
