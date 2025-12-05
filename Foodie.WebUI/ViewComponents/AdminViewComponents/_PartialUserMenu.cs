using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.AdminViewComponents
{
    public class _PartialUserMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}