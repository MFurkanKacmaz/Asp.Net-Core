using AspNetCoreIdentityApp.Web.OptionsModels;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AspNetCoreIdentityApp.Web.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmail(string Name, string Subject, string Message, string ToEmail)
        {
            var smptClient = new SmtpClient();

            smptClient.Host = _emailSettings.Host;
            smptClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smptClient.UseDefaultCredentials = false;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smptClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailSettings.Email);
            mailMessage.To.Add(ToEmail);
            mailMessage.To.Add(_emailSettings.Email);

            mailMessage.Subject = $"{Subject}";
            mailMessage.Body = @$"Gönderen: {Name} <br>
                            Mesaj: {Message} <br><br>
                            <small><i>Bu bir sistem mesajıdır, lütfen cevap yazmayınız!</i></small>";

            mailMessage.IsBodyHtml = true;


            await smptClient.SendMailAsync(mailMessage);



        }
    }
}
