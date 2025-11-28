using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialFeature : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}