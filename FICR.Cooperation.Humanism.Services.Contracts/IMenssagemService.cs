
namespace FICR.Cooperation.Humanism.Services.Contracts
{
   public interface  IMenssagemService
    {
        Task SendMessage(List<string> recipientNumbers, string messageBody, Dictionary<string, string> variables = null);
    }
}
