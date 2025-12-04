using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialFooter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
