using Foodie.WebUI.Dtos.TestimonialDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> TestimonialList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Testimonials");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var testimonials = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(jsonData);
                return View(testimonials);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7285/api/Testimonials", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("TestimonialList");
                }
            }
            return View(createTestimonialDto);
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7285/api/Testimonials?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TestimonialList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Testimonials/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var testimonial = JsonConvert.DeserializeObject<GetTestimonialByIdDto>(jsonData);
                return View(testimonial);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(int id, UpdateTestimonialDto updateTestimonialDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateTestimonialDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7285/api/Testimonials/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("TestimonialList");
                }
            }
            return View();
        }
    }
}