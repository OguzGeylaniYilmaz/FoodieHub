using Foodie.WebUI.Dtos.NotificationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents.AdminViewComponents
{
    public class _PartialNotificationAdmin : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialNotificationAdmin(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Notifications/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var notifications = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(jsonData);
                return View(notifications);
            }
            return View();
        }
    }
}
