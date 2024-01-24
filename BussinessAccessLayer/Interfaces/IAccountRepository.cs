using Management.Common.Models;
using Management.Common.Models.DTO;

namespace Management.Services.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CheckUserAsync(string Email);

        Task<bool> SignUpUserAsync(SignUpDto UserDto);

        Task<bool> LoginUserAsync(LoginModelDto UserLogin);

        Task<string> CreateTokenAsync(string Email);
    }
}