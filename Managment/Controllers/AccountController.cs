using Management.Common;
using Management.Common.Enum;
using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Management.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        public IApiHelperService _apiHelperService;
        public IEmailService _emailService { get; }

        public AccountController(IApiHelperService apiHelperService, IEmailService emailService)
        {
            _apiHelperService = apiHelperService;
            _emailService = emailService;
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
                    var role = await GetRole(result.Token);
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
                    ModelState.AddModelError(Constants.Empty, result.Error ?? Constants.ErrorMessage);
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
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
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