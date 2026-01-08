using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Foodie.WebUI.Controllers
{
    public class AIController : Controller
    {
        public IActionResult GenerateRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateRecipe(string ingredients)
        {
            var apiKey = "";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant that creates recipes based on given ingredients." },
                    new { role = "user", content = $"Create a recipe using the following ingredients: {ingredients}" }
                },
                max_tokens = 500,
                temperature = 0.7
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                var content = result.choices[0].message.content;
                ViewBag.Recipe = content;
            }
            else
            {
                ViewBag.Recipe = "Error generating recipe." + response.StatusCode;
            }

            return View();

        }

        public class OpenAIResponse
        {
            public List<Choice>? choices { get; set; }
        }

        public class Choice
        {
            public Message? message { get; set; }
        }

        public class Message
        {
            public string? role { get; set; }
            public string? content { get; set; }
        }
    }
}
