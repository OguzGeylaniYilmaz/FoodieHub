using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
