using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.AdminViewComponents
{
    public class _PartialHeadAdmin : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
