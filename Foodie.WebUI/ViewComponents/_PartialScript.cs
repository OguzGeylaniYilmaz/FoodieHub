using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialScript : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
