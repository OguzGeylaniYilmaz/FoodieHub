using Foodie.WebUI.Dtos.EventDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Foodie.WebUI.Controllers
{
    public class EventController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> EventList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Events");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var events = JsonConvert.DeserializeObject<List<ResultEventDto>>(jsonData);
                return View(events);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventDto createEventDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createEventDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7285/api/Events", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EventList");
                }
            }
            return View(createEventDto);
        }

        public async Task<IActionResult> DeleteEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7285/api/Events?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EventList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEvent(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Events/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var eventDto = JsonConvert.DeserializeObject<GetEventByIdDto>(jsonData);
                return View(eventDto);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventDto updateEventDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateEventDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7285/api/Events/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EventList");
                }
            }
            return View();
        }
    }
}