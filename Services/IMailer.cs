using System.Threading.Tasks;

namespace ECommerce.WebApi.Services
{
    public interface IMailer
    {
        Task<bool> SendEmailAsync(string email, string subject, string body);
    }
}
