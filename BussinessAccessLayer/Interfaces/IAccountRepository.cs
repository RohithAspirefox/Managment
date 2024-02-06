using Management.Common.Models;

namespace Management.Services.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CheckUserAsync(string Email);

        Task<bool> SignUpUserAsync(SignUpDto UserDto);

        Task<bool> LoginUserAsync(LoginModelDto loginDto);

        Task<string> CreateTokenAsync(string Email);

        Task<bool> UpdateUsersData(UserEdit userDto);
    }
}