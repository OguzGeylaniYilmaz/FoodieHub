using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.AdminViewComponents
{
    public class _PartialSidebarAdmin : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}