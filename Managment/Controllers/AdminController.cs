using Management.Common.Models;
using Management.Data.AppDbContext;
using Management.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Management.Common.Models.ApiResponse;
using System.Web;
using Management.Services.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Collections.Specialized;
using Management.Common;
using Management.Services.Services;
using Newtonsoft.Json;

namespace Management.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _application;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEmailService _emailService;


        public AdminController(ApplicationDbContext application, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IEmailService emailService)
        {
            _application = application;
            _configuration = configuration;
            _hostingEnvironment = webHostEnvironment;
            _emailService = emailService;
        }

        public IActionResult Index(int page = 1, int pageSize = 6, string? search = "", string? sort = "", string? column = "")
        {
            var query = _application.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(user =>
                    user.Active == "Yes" &&
                    (user.FirstName.Contains(search) || user.LastName.Contains(search) || user.Id.Contains(search))
                );
            }
            else
            {
                query = query.Where(user => user.Active == "Yes");
            }

            var totalCount = query.Count();
            var totalPage = (int)Math.Ceiling((decimal)totalCount / pageSize);

            if (string.IsNullOrEmpty(column))
            {
                column = ""; // Provide a default sorting column
                sort = "asc";
            }

            var orderByExpression = GetPropertyExpression(column);

            query = sort == "asc" ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);

            var data = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Data = data;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.Column = column;
            ViewBag.TotalCount = totalCount;
            ViewBag.Count = data.Count;
            return View();
        }

        private static Expression<Func<User, object>> GetPropertyExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(User), "x");
            var propertyInfo = typeof(User).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                propertyName = "FirstName";
            }
            var property = Expression.Property(parameter, propertyName);
            var convert = Expression.Convert(property, typeof(object));
            var lambda = Expression.Lambda<Func<User, object>>(convert, parameter);
            return lambda;
        }

        public IActionResult Active(string id)
        {
            var user = _application.Users.Find(id);
            user.Active = "No";
            _application.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _application.SaveChanges();
            
            return RedirectToAction("Index","Admin");
        }


        public IActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NavigateToSignUp(ForgotPasswordDto navigate)
        {
            try
            {
    
                var frontEndsetPasswordUrl = _configuration["NavigateURl"];
                /*                var callbackUrl = $"{frontEndResetPasswordUrl}{}&email={user.Email}";
                */


                HttpContext.Session.SetString("SetPassword", navigate.Email.ToString());
                var content = await System.IO.File.ReadAllTextAsync(Path.Combine(_hostingEnvironment.ContentRootPath, "NavigateToSignUp.html"));



                // Email to add to the URL
                string emailToAdd = navigate.Email;

                // Build the URL with the email parameter
                UriBuilder uriBuilder = new UriBuilder(frontEndsetPasswordUrl);
                NameValueCollection queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
                queryParams["email"] = emailToAdd;
                uriBuilder.Query = queryParams.ToString();

                string modifiedUrl = uriBuilder.ToString();


                content = content.Replace("{NavigateRoute}", modifiedUrl);
                var message = new Message(navigate.Email, "Please ", content);
                await _emailService.SendEmailAsync(message);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");

            }
            catch (Exception)
            {
                throw;
            }
           
        }


    }

}