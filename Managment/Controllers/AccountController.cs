using Management.Common;
using Management.Common.Enum;
using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Data.AppDbContext;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;

namespace Management.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        public IApiHelperService _apiHelperService;
        public IEmailService _emailService { get; }

        private readonly ApplicationDbContext _application;
        /*         HttpContext context = HttpContext.Current;
        */
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountController(IApiHelperService apiHelperService, IEmailService emailService, ApplicationDbContext application, IHttpContextAccessor httpContextAccessor)
        {
            _application = application;
            _apiHelperService = apiHelperService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;

        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelDto model)
        {
            try
            {
                var result = await _apiHelperService.PostAsync<LoginResponse>(ApiRoute.Login, model);
                if (result != null && result.Success)
                {
                    HttpContext.Session.SetString("UserId", model.Email.ToString());
                    var role = await GetRole(result.Token);
                    var userEmail = HttpContext.Session.GetString("UserId");
                    var user = _application.Users.FirstOrDefault(emp => emp.Email == userEmail.ToLower());
                    var Islogged = false;
                    if(user != null)
                    {

                        Islogged = (bool)user.IsLogged;
                        if(role=="User" && Islogged)
                        {
                            return RedirectToAction("Profile", "User");
                        }
                    }
                    switch (role)
                    {

                         
                        case "HR":
                            return RedirectToAction("Index", "HR");
                        case "Admin":
                            return RedirectToAction("Index", "Admin");
                        case "User":
                            return RedirectToAction("Index", "User");
                    }
                }
                ModelState.AddModelError(string.Empty, result?.Error ?? string.Empty);
                return View();
            }
            catch (Exception)
            { throw;}
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUpDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.SignUp, model);
                    if (result.Success)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    ModelState.AddModelError("", result.Error ?? Constants.ErrorMessage);
                }
                return View();
            }
            catch (Exception ex)
            {throw ex;}

        }

        public async Task<IActionResult> Logout()
        {
            using (var client = new HttpClient())
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.Session.Clear();

                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreatePassword()
        {
            return View();
        }


        public async Task<IActionResult> CreatePassword(CreatePasswordDto createtPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.CreatePassword, createtPasswordModel);
                    if (result.Success)
                    {
                        return RedirectToAction("ForgotPasswordConfirmation", "Account");
                    }
                }
                ModelState.AddModelError(Constants.Empty, Constants.EmailExists);
                return View();
            }
            catch (Exception)
            { throw; }
        }



        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.ForgetPassword, forgotPasswordModel);
                    if (result.Success)                    
                        return RedirectToAction("ForgotPasswordConfirmation", "Account");
                    ModelState.AddModelError(Constants.Empty, result.Error ?? Constants.ValidEmail);
                    return View();
                }
                ModelState.AddModelError(Constants.Empty, Constants.DataRequired);
                return View();
            }
            catch (Exception)
            {throw;}
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SetPassword(string token, string email)
        {
            var model = new SetPasswordDto { Token = token, Email = email };
            return View(model);
        }



        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPassword(SetPasswordDto setPasswordDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.SetPassword, setPasswordDto);
                    if (result.Success && result != null)
                        return RedirectToAction("Login", "Account");
                    ModelState.AddModelError(Constants.Empty, result.Message ?? Constants.ErrorMessage);
                }
                ModelState.AddModelError(Constants.Empty,Constants.Empty);
                return View();
            }
            catch (Exception ex)
            { throw ex; }
        }




        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.ResetPassword, resetPasswordDto);
                    if (result.Success && result!=null)
                        return RedirectToAction("Login", "Account");
                    ModelState.AddModelError(Constants.Empty, result.Message ?? Constants.ErrorMessage);
                }
                ModelState.AddModelError(Constants.Empty, Constants.DataRequired);
                return View();
            }
            catch (Exception ex)
            {throw ex;}
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View("Error");
        }

        private async Task<string> GetRole(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string role = jwt.Claims.ToList()[1].Value;
            HttpContext.Session.SetString("userToken", token);

            var claimsIdentity = new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return role;
        }
    }
}