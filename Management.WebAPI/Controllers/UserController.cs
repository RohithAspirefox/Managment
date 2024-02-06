using Management.Common.Models.ApiResponse;
using Management.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Management.Services.Interfaces;
using Management.Data.AppDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;

namespace Management.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
      
        private readonly SignInManager<User> _signInManager;


        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserController(IAccountRepository accountRepository, SignInManager<User> signInManager, IEmailService emailService, ILogger<AccountController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, ApplicationDbContext applicationDbContext)
        {

            _emailService = emailService;
            _accountRepository = accountRepository;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _hostingEnvironment = webHostEnvironment;
            _applicationDbContext = applicationDbContext;

        }

        [HttpPost("UpdateUsers")]
        public async Task<IActionResult> UpdateUsers(UserEdit userDto)
        {
            var createUser = await _accountRepository.UpdateUsersData(userDto);
            if (createUser)
            {
                return Ok(new BaseResponse { Success = true });
            }
            return Ok(new BaseResponse { Success = false });
        }
    }
}
