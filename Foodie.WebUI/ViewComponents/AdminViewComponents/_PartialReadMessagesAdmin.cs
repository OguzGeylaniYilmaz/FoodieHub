using Foodie.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents.AdminViewComponents
{
    public class _PartialReadMessagesAdmin : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialReadMessagesAdmin(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Messages/GetReadMessages");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<List<GetReadMessagesDto>>(jsonData);
                return View(messages);
            }
            return View();
        }
    }
}
