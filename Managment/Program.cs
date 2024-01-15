using Management.Common.Models;
using Management.Services.Interfaces;
using Management.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

            builder.Services.AddSingleton<EmailConfiguration>(emailConfig);

            builder.Services.AddTransient<IApiHelperService, ApiHelperService>();

            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }
    }
}