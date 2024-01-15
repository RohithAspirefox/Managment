using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles ="User")]
    public class UserController : Controller
    {
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
    }
}
