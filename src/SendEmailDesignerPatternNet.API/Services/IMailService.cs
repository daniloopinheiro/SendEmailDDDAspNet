using SendEmailDesignerPatternNet.API.Domain.Models.Requests;

namespace SendEmailDesignerPatternNet.API.Services;

public interface IMailService
{
   Task SendEmailAsync(MailRequest mailRequest);
}