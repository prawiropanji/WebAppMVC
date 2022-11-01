using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult UnAuthorized()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
