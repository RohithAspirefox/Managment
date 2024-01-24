using Management.Common.Models;
using Management.Common.Models.Entity;

namespace Management.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Message message);
    }
}