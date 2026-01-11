using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialMainChart : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
