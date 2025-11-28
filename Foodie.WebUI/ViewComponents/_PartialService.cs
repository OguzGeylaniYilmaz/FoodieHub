using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialService : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
