using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Management.Common;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;
using Management.Data.AppDbContext;
using System.Data.Entity;
using System.Linq;

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
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _applicationDbContext;
        

        public AccountController(IAccountRepository accountRepository, UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, ILogger<AccountController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ApplicationDbContext applicationDbContext)
        {
            _emailService = emailService;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = webHostEnvironment;
            _applicationDbContext = applicationDbContext;
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
                    }) ; 
                }
                _logger.LogError("{Email} {Constants}", model.Email, Constants.IncorrectCredentials);
                return BadRequest(new LoginResponse { Success = false, Error = Constants.IncorrectCredentials });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var message = new Message(user.Email, Constants.ResetPassword, content);
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


        [HttpPost("CreatePassword")]
        public async Task<IActionResult> CreatePassword(CreatePasswordDto createPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var user = await _userManager.FindByEmailAsync(createPasswordModel.Email);
                if (user == null)
                {
                    // User does not exist, create a new user
                    user = new User { UserName = createPasswordModel.Email, Email = createPasswordModel.Email };
                  
                    await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, "User");
                    /*    bool checkUser = await _accountRepository.CheckUserAsync(createPasswordModel.Email);
                        if (checkUser)
                        {

                            var createUser = await _accountRepository.SignUpUserAsync(createPasswordModel);
                        }*/

                    // Generate token

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedToken = HttpUtility.UrlEncode(token);
                    var frontEndCreatePasswordUrl = _configuration["NavigateURl"];

                    var callbackUrl = $"{frontEndCreatePasswordUrl}{encodedToken}&email={user.Email}";



                    var content = await System.IO.File.ReadAllTextAsync(Path.Combine(_hostingEnvironment.ContentRootPath, "NavigateToSignUp.html"));

                    content = content.Replace("{NavigateRoute}", callbackUrl);
                    var message = new Message(createPasswordModel.Email, Constants.SetPassword, content);

                    await _emailService.SendEmailAsync(message);
                    return Ok(new BaseResponse
                    {
                        Success = true,
                    });
                }
                return Ok(new BaseResponse
                {
                    Success = false,
                });

            }
            
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpPost("SetPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordDto setPasswordDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var user = await _userManager.FindByEmailAsync(setPasswordDto.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, setPasswordDto.Token, setPasswordDto.Password);
                    if (result.Succeeded)
                    {
                        Message message = new Message(user.Email, Constants.SetPassword, Constants.SetSuccessfull);
                        await _emailService.SendEmailAsync(message);
                        return Ok(new BaseResponse { Message = Constants.SetSuccessfull, Success = true, });
                    }
                    return NotFound(new BaseResponse { Message = result?.Errors?.FirstOrDefault().Code, Success = false, });

                }
                return NotFound(Constants.DontExist);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        [HttpPost("NavigateToSignUp")]
        public async Task<IActionResult> NavigateToSignUp(ResetPasswordDto resetPasswordDto)
        {
            return RedirectToAction("Login", "Account");
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

        private static Expression<Func<User, object>> GetPropertyExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(User), "x");
            var property = Expression.Property(parameter, propertyName);
            var convert = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<User, object>>(convert, parameter);
            return lambda;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllProducts(int page = 1, int pageSize = 5, string? search = "", string? sort = "", string? column = "")
        {
            var query = _applicationDbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                //query = query.Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search) || x.Id.Contains(search));
                query = query.Where(x => x.FirstName.Contains(search));
            }

            var totalCount = query.Count();
            var totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);

            if (string.IsNullOrEmpty(column))
            {
                column = "Name";
            }

            /*switch (sort)
            {
                case "asc":
                    query = query.OrderByDescending(GetPropertyExpression(column));
                    sort = "asc";
                    break;
                default:
                    query = query.OrderBy(GetPropertyExpression(column));
                    sort = "desc";
                    break;
            }*/

            var productPerPage = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                TotalCount = totalCount,
                TotalPage = totalPage,
                ProductPerPage = productPerPage
            };

            return Ok(response);
        }
    }
}