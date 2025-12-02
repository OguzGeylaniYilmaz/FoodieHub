using Foodie.WebUI.Dtos.EventDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialEvent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialEvent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Events/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var events = JsonConvert.DeserializeObject<List<ResultEventDto>>(jsonData);
                return View(events);
            }
            return View();
        }
    }
}
