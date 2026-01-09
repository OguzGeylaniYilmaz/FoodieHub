using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
