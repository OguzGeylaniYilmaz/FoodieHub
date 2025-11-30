using Foodie.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.ViewComponents
{
    public class _PartialProductList : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PartialProductList(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Products/");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(products);
            }
            return View();
        }
    }
}
