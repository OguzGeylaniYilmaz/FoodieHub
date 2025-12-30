using Foodie.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.Controllers
{
    public class ChefController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChefController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ChefList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Chefs");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var chefs = JsonConvert.DeserializeObject<List<ResultChefDto>>(jsonData);
                return View(chefs);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateChef()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createChefDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7285/api/Chefs", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChefList");
                }
            }
            return View(createChefDto);
        }

        public async Task<IActionResult> DeleteChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7285/api/Chefs?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ChefList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateChef(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Chefs/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var chef = JsonConvert.DeserializeObject<GetChefByIdDto>(jsonData);
                return View(chef);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateChefDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync("https://localhost:7285/api/Chefs", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ChefList");
                }
            }
            return View();
        }
    }
}