using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Authorize(Roles = "HR")]
    [Route("[controller]/[action]")]
    public class HRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}