using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialWidget : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
