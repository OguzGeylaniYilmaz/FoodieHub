using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialStatistics : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialStatistics(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var clientProductCount = _httpClientFactory.CreateClient();
            var response = await clientProductCount.GetAsync("https://localhost:7285/api/Statistics/ProductCount");
            var productCount = await response.Content.ReadAsStringAsync();
            ViewBag.productCount = productCount;

            var reservationClient = _httpClientFactory.CreateClient();
            var reservationResponse = await reservationClient.GetAsync("https://localhost:7285/api/Statistics/ReservationCount");
            var reservationCount = await reservationResponse.Content.ReadAsStringAsync();
            ViewBag.reservationCount = reservationCount;

            var chefClient = _httpClientFactory.CreateClient();
            var chefResponse = await chefClient.GetAsync("https://localhost:7285/api/Statistics/ChefCount");
            var chefCount = await chefResponse.Content.ReadAsStringAsync();
            ViewBag.chefCount = chefCount;

            var categoryClient = _httpClientFactory.CreateClient();
            var categoryResponse = await categoryClient.GetAsync("https://localhost:7285/api/Statistics/CategoryCount");
            var categoryCount = await categoryResponse.Content.ReadAsStringAsync();
            ViewBag.categoryCount = categoryCount;
            return View();
        }
    }
}
