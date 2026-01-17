using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialDashboardScript : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
