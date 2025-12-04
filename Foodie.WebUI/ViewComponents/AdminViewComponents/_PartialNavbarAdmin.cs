using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.AdminViewComponents
{
    public class _PartialNavbarAdmin : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
