using Foodie.WebUI.Dtos.GroupReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialGroupReservation : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialGroupReservation(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/GroupReservations/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var groupReservations = JsonConvert.DeserializeObject<List<ResultGroupReservationDto>>(jsonData);

                return View(groupReservations);
            }
            return View();
        }
    }
}