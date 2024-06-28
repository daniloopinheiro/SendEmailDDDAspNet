using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendEmailDesignerPatternNet.API.Domain.Models.Requests;
using SendEmailDesignerPatternNet.API.Settings;

namespace SendEmailDesignerPatternNet.API.Services;

public class MailService(IOptions<MailSettings> mailSettings) : IMailService
{
    private readonly MailSettings _mailSettings = mailSettings.Value;

    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(_mailSettings.Correspondencia);
        email.To.Add (MailboxAddress.Parse(mailRequest.ParaEmail));
        email.Subject = mailRequest.Assunto;
        
        var builder = new BodyBuilder();
        
        if (mailRequest.Anexos != null)
        {
            byte[] fileBytes;
            foreach (var file in mailRequest.Anexos)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                }
            }    
        }

        builder.HtmlBody = mailRequest.Corpo;
        email.Body = builder.ToMessageBody();
        
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Hospedar, _mailSettings.Porta, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Correspondencia, _mailSettings.Senha);
        
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
    }
}


#region MailService codigo fonte antigo
// public class MailService : IMailService
// {
//     private readonly MailSettings _mailSettings;
//     public MailService(IOptions<MailSettings> mailSettings)
//     {
//         _mailSettings = mailSettings.Value;
//     }
//     
//     public Task SendEmailAsync(MailRequest mailRequest)
//     {
//         var email = new MimeMessage();
//         email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
//         email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
//         email.Subject = mailRequest.Subject;

//         var builder = new BodyBuilder();
//         if (mailRequest.Attachments != null)
//         {
//             byte[] fileBytes;
//             foreach (var file in mailRequest.Attachments)
//             {
//                 if (file.Length > 0)
//                 {
//                     using (var ms = new MemoryStream())
//                     {
//                         file.CopyTo(ms);
//                         fileBytes = ms.ToArray();
//                     }
//                     builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
//                 }
//             }
//         }

//         builder.HtmlBody = mailRequest.Body;
//         email.Body = builder.ToMessageBody();

//         using var smtp = new SmtpClient();
//         smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
//         smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
//         await smtp.SendAsync(email);
//         smtp.Disconnect(true);
//     }
// }
#endregion