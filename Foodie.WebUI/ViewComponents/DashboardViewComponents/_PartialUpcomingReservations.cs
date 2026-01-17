using Foodie.WebUI.Dtos.GroupReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialUpcomingReservations : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialUpcomingReservations(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(
                "https://localhost:7285/api/GroupReservations/upcoming"
            );

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<ResultGroupReservationDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultGroupReservationDto>>(jsonData);

            return View(values);
        }
    }
}