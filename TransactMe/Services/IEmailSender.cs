using System.Threading.Tasks;

namespace TransactMe.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}