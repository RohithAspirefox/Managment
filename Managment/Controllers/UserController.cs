using AutoMapper;
using Management.Common;
using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Data.AppDbContext;
using Management.Services.Interfaces;
using Management.Services.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.IO;
using System.Linq;


namespace Management.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _application;
        public IApiHelperService _apiHelperService;
        private IWebHostEnvironment _webHostEnvironment;
      

        public UserController(ApplicationDbContext application, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IApiHelperService apiHelperService )
        {
            _application = application;
            _apiHelperService = apiHelperService;
            _webHostEnvironment = webHostEnvironment;
           
        }

        [HttpGet]
        public IActionResult Index()
        {
     
            var userEmail = HttpContext.Session.GetString("UserId");
           var user = _application.Users.FirstOrDefault(emp => emp.Email == userEmail.ToLower());
            UserEdit userEdit = new UserEdit();
            userEdit.Email = user.Email;
           
            ViewBag.Image = user.ImageURL;
           
            return View(userEdit);
        }

        [HttpPost]
        public IActionResult UpdateUsers(UserEdit model)
        {
            string uniqueFileName = UploadedFile(model);

            var user = _application.Users.FirstOrDefault(emp => emp.Email == model.Email.ToLower());
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.City = model.City;
            user.PhoneNumber = model.Mobile;
            user.ImageURL = uniqueFileName;
            user.GithubURL = model.Github;
            user.FacebookURL = model.Facebook;
            user.TwitterURL = model.Twitter;
            user.InstagramURL = model.Instagram;
            user.IsLogged = true;
            user.Active = "Yes";

            _application.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _application.SaveChanges();

            return RedirectToAction("Profile", "User");

        }

        private string UploadedFile(UserEdit model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyToAsync(fileStream);
                }
            }
            
            return uniqueFileName;
        }


        public IActionResult UpdateProfile()
        {
            return View();
        }

        public IActionResult Profile()
        {
            var userEmail = HttpContext.Session.GetString("UserId");
            var user = _application.Users.FirstOrDefault(emp => emp.Email == userEmail.ToLower());
            UserEdit userEdit = new UserEdit();
            userEdit.FirstName = user.FirstName;
            userEdit.LastName = user.LastName;
            userEdit.Email = user.Email;
            userEdit.Address = user.Address;
            userEdit.Mobile = user.PhoneNumber;
            userEdit.Github = user.GithubURL;
            userEdit.Facebook = user.FacebookURL;
            userEdit.Twitter = user.TwitterURL;
            userEdit.Instagram = user.InstagramURL;
            ViewBag.Image = user.ImageURL;
            ViewBag.User=user.Id;
            return View(userEdit);
        }

        public IActionResult Edit(string id)
        {
            var user = _application.Users.Find(id);

            ProfileEdit userEdit = new ProfileEdit();
            userEdit.FirstName = user.FirstName;
            userEdit.LastName = user.LastName;
            userEdit.Email = user.Email;
            userEdit.Address = user.Address;
            userEdit.Mobile = user.PhoneNumber;
            ViewBag.Image = user.ImageURL;

            return View(userEdit);
        }

        [HttpPost]
        public IActionResult EditProfile(UserEdit model)
        {
            string uniqueFileName = UploadedFile(model);

            var user = _application.Users.FirstOrDefault(emp => emp.Email == model.Email.ToLower());
            string OldImgPath = user.ImageURL;
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", OldImgPath);

/*            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
*/            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.City = model.City;
            user.PhoneNumber = model.Mobile;
            if (model.ProfileImage != null)
            {
                user.ImageURL = uniqueFileName;
            }
            if (System.IO.File.Exists(filePath) && model.ProfileImage != null)
            {
                System.IO.File.Delete(OldImgPath);

            }
            /*            user.ImageURL = model.ProfileImage;
            */
            _application.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _application.SaveChanges();
            return RedirectToAction("Profile","User");
        }

    }
}
