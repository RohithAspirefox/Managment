using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Management.Common;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
namespace Management.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AccountController(IAccountRepository accountRepository, UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, ILogger<AccountController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _emailService = emailService;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = webHostEnvironment;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpDto model)
        {
            try
            {
                bool checkUser = await _accountRepository.CheckUserAsync(model.Email);
                if (checkUser)
                {
                    var createUser = await _accountRepository.SignUpUserAsync(model);
                    if (createUser)
                    {
                        return Ok(new BaseResponse { Success = true });
                    }
                    return Ok(new BaseResponse { Success = false, Error = Constants.ReTryPassword });
                }
                return NotFound(Constants.AlreadyExist);
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.RegistrationFailed);
                throw ex;
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModelDto model)
        {
            try
            {
                bool checkUser = await _accountRepository.LoginUserAsync(model);
                if (checkUser)
                {
                    string token = await _accountRepository.CreateTokenAsync(model.Email);
                    return Ok(new LoginResponse
                    {
                        Success = true,
                        Token = token
                    });
                }
                _logger.LogError("{Email} {Constants}", model.Email, Constants.IncorrectCredentials);
                return BadRequest(new LoginResponse { Success = false, Error = Constants.IncorrectCredentials });
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordDto forgotPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                    return NotFound();
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = HttpUtility.UrlEncode(token);
                var frontEndResetPasswordUrl = _configuration["FrontEndResetPasswordUrl"];

                var callbackUrl = $"{frontEndResetPasswordUrl}{encodedToken}&email={user.Email}";
                var content = await System.IO.File.ReadAllTextAsync(Path.Combine(_hostingEnvironment.ContentRootPath, "EmailTemplate.html"));
                content = content.Replace("{ResetPasswordRoute}", callbackUrl);
                Message message = new Message(user.Email, Constants.ResetPassword, content);
                await _emailService.SendEmailAsync(message);
                return Ok(new BaseResponse
                {
                    Success = true,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
                    if (result.Succeeded)
                    {
                        Message message = new Message(user.Email, Constants.ResetPassword, Constants.SuccessfullySet);
                        await _emailService.SendEmailAsync(message);
                        return Ok(new BaseResponse{Message = Constants.ResetSuccessfull,Success = true,});
                    }
                    return NotFound(new BaseResponse { Message = result?.Errors?.FirstOrDefault().Code , Success = false, });
                }
                return NotFound(Constants.DontExist);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}