using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
