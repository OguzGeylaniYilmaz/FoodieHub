using Foodie.WebUI.Dtos.ContactDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialContact : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialContact(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Contacts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contactInfo = JsonConvert.DeserializeObject<List<ResultContactDto>>(content);
                return View(contactInfo);
            }
            return View();
        }
    }
}
