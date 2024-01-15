using Management.Common.Models;

namespace Management.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}