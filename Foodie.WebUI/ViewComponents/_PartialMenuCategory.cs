using Foodie.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialMenuCategory : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialMenuCategory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Categories/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(categories);
            }
            return View();
        }
    }
}
