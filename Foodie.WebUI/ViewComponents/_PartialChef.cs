using Foodie.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialChef : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialChef(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = client.GetAsync("https://localhost:7285/api/Chefs/");

            if (response.Result.IsSuccessStatusCode)
            {
                var jsonData = await response.Result.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
