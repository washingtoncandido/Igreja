using FICR.Cooperation.Humanism.Data.Contracts.Base;
using FICR.Cooperation.Humanism.Services.Contracts;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace FICR.Cooperation.Humanism.Services.Twilio
{
    public class MenssagemService : IMenssagemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioWhatsAppNumber;

        public MenssagemService(IUnitOfWork unitOfWork, string accountSid, string authToken, string twilioWhatsAppNumber)
        {
            _unitOfWork = unitOfWork;
            _accountSid = accountSid;
            _authToken = authToken;
            _twilioWhatsAppNumber = twilioWhatsAppNumber;

            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendMessage(
            List<string> recipientNumbers,
            string messageBody,
            Dictionary<string, string> variables = null,
            string mediaUrl = null)
        {
            try
            {
                if (string.IsNullOrEmpty(messageBody) && string.IsNullOrEmpty(mediaUrl))
                    throw new ArgumentException("É necessário fornecer um corpo de mensagem ou uma URL de mídia.");

                foreach (var recipientNumber in recipientNumbers)
                {
                    if (string.IsNullOrEmpty(recipientNumber))
                    {
                        Console.WriteLine("Número do destinatário inválido. Pulando para o próximo.");
                        continue;
                    }

                    var messageOptions = new CreateMessageOptions(new PhoneNumber($"whatsapp:{recipientNumber}"))
                    {
                        From = new PhoneNumber($"whatsapp:{_twilioWhatsAppNumber}"),
                        Body = messageBody
                    };

                    if (variables != null && variables.Count > 0)
                    {
                        var formattedVariables = string.Join(Environment.NewLine, variables.Select(v => $"{v.Key}: {v.Value}"));
                        messageOptions.Body = $"{messageBody}{Environment.NewLine}{formattedVariables}";
                    }

                    if (!string.IsNullOrEmpty(mediaUrl))
                    {
                        messageOptions.MediaUrl = new List<Uri> { new Uri(mediaUrl) };
                    }

                    try
                    {
                        var message = await MessageResource.CreateAsync(messageOptions);
                        Console.WriteLine("Message sent to " + recipientNumber + ": " + message.Body);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending message to {recipientNumber}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw;
            }
        }
    }
}
