using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentity.Controllers
{
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                return View();
            }
            public IActionResult Privacy()
            {
                return View();
            }
            [AllowAnonymous]
            public IActionResult NonSecureMethod()
            {
                return View();
            }
            [Authorize]
            public IActionResult SecureMethod()
            {
                return View();
            }
        }
    }
