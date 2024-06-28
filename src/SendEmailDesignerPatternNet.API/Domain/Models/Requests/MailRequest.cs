namespace SendEmailDesignerPatternNet.API.Domain.Models.Requests;

public class MailRequest
{
    public string ParaEmail { get; set; }
    public string Assunto { get; set; }
    public string Corpo { get; set; }
    public List<IFormFile> Anexos { get; set; } 
}

// public class MailRequest(List<IFormFile> anexos, string paraEmail, string assunto, string corpo)
// {
//     public string ParaEmail { get; set; } = paraEmail;
//     public string Assunto { get; set; } = assunto;
//     public string Corpo { get; set; } = corpo;
//     public List<IFormFile> Anexos { get; set; } = anexos;
// }