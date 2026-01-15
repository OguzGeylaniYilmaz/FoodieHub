using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialBookTable : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}