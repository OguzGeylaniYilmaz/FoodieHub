using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }
    }
}
