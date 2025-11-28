using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.Models
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
