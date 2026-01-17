using Foodie.WebUI.Dtos.GroupReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialTodayReservations : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialTodayReservations(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/GroupReservations/today");

            var json = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultGroupReservationDto>>(json);

            return View(values);
        }
    }

}
