namespace AspNetCoreIdentityApp.Web.Services
{
    public interface IEmailService
    {

        Task SendEmail(string Name, string Subject, string Message, string ToEmail);
    }
}
