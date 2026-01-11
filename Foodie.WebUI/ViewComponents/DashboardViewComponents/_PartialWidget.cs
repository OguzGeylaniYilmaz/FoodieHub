using Foodie.WebUI.Dtos.ReservationDtos;
using Foodie.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.ViewComponents.DashboardViewComponents
{
    public class _PartialWidget : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialWidget(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<List<ResultReservationDto>>("https://localhost:7285/api/Reservations");

            var model = new ReservationStatsViewModel
            {
                TotalReservations = response?.Count ?? 0,
                ConfirmedReservations = response?.Count(r => r.ReservationStatus == "Confirmed") ?? 0,
                PendingReservations = response?.Count(r => r.ReservationStatus == "Pending") ?? 0,
                CustomerCount = response?.Sum(r => r.NumberOfPeople) ?? 0
            };

            return View(model);
        }
    }
}
