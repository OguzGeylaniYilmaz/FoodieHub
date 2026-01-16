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

        [HttpGet]
        public async Task<IActionResult> GalleryDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Galleries/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var galleryItem = JsonConvert.DeserializeObject<GetGalleryByIdDto>(content);
                return View(galleryItem);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GalleryDetail(UpdateGalleryDto updateGalleryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateGalleryDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7285/api/Galleries", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GalleryList");
            }
            return View();
        }
    }
}