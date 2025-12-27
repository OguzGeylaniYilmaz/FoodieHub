using Foodie.WebUI.Dtos.WhyUsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.Controllers
{
    public class WhyUsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhyUsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> WhyUsList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Services");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var services = JsonConvert.DeserializeObject<List<ResultWhyUsDto>>(jsonData);

                return View(services);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateWhyUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhyUs(CreateWhyUsDto createWhyUsDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createWhyUsDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7285/api/Services", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("WhyUsList");
                }
            }
            return View(createWhyUsDto);
        }

        public async Task<IActionResult> DeleteWhyUs(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7285/api/Services?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("WhyUsList");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWhyUs(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Services/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var service = JsonConvert.DeserializeObject<GetWhyUsByIdDto>(jsonData);
                return View(service);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWhyUs(int id, UpdateWhyUsDto updateWhyUsDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateWhyUsDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7285/api/Services/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("WhyUsList");
                }
            }
            return View(updateWhyUsDto);
        }
    }
}