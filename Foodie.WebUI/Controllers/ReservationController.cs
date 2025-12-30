using Foodie.WebUI.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace Foodie.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ReservationList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Reservations");

            if (response.IsSuccessStatusCode)
            {
                var reservations = await response.Content.ReadFromJsonAsync<List<ResultReservationDto>>();
                return View(reservations);
            }
            return View();
        }


    }
}
