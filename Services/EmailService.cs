using System.Net;
using System.Net.Mail;

namespace Adocao.Services;

public class EmailService 
{
    public bool sendMessage(string toNome, string toEmail, string assunto, string corpo,
        string fromNome = "Equipe KBR - Adoção de animais", string fromEmail = "vitorschneider6@hotmail.com")
    {
        var clienteSmtp = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);

        clienteSmtp.Credentials = new NetworkCredential(Configuration.Smtp.Username, Configuration.Smtp.Password);
        clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        clienteSmtp.EnableSsl = true;

        var email = new MailMessage();

        email.From = new MailAddress(fromEmail, fromNome);
        email.To.Add(new MailAddress(toEmail, toNome));
        email.Subject = assunto;
        email.Body = corpo;
        email.IsBodyHtml = true;

        try
        {
            clienteSmtp.Send(email);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }
}