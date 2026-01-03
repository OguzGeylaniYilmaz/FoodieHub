using Foodie.WebUI.Dtos.GalleryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GalleryList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Galleries");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var galleryItems = JsonConvert.DeserializeObject<List<ResultGalleryDto>>(content);
                return View(galleryItems);
            }

            return View();
        }
    }
}
