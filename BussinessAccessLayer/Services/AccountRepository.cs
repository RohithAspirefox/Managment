using AutoMapper;
using Management.Common.Models;
using Management.Common.Models.DTO;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Management.Services.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> CheckUserAsync(string Email)
        {
            var userCheck = await _userManager.FindByEmailAsync(Email);
            if (userCheck != null)
                return false;
            return true;
        }

        public async Task<bool> SignUpUserAsync(SignUpDto UserDto)
        {
            var user = _mapper.Map<User>(UserDto);
            var result = await _userManager.CreateAsync(user, UserDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return true;
            }
            return false;
        }

        public async Task<bool> LoginUserAsync(LoginModelDto UserLogin)
        {

            var user = await _userManager.FindByEmailAsync(UserLogin.Email);
            if (user != null)
            {
                var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, UserLogin.Password);
                if (isPasswordCorrect)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<string> CreateTokenAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            var role = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role[0]),
                new Claim(ClaimTypes.NameIdentifier,
            user.Id)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                _configuration.GetSection("JWT:Issuer").Value,
                _configuration.GetSection("JWT:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}