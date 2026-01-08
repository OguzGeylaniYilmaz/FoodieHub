using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
