using Foodie.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using static Foodie.WebUI.Controllers.AIController;

namespace Foodie.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7285/api/Messages");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<List<GetReadMessagesDto>>(responseContent);
                return View(messages);
            }
            return View();
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7285/api/Messages/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Messages/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<UpdateMessageDto>(responseContent);
                return View(message);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateMessageDto);
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync("https://localhost:7285/api/Messages", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MessageList");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AnswerMessageWithAI(int id, string prompt)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7285/api/Messages/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject<GetMessageByIdDto>(responseContent);

            if (message?.MessageDetail == null)
            {
                ViewBag.AIAnswer = "Message detail is missing.";
                return View(message);
            }

            prompt = message.MessageDetail;

            var apiKey = "";

            using var clientAI = new HttpClient();
            clientAI.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new {
                        role = "system",
                        content = "You are an AI tool that makes food suggestions for a restaurant. Our goal is to suggest recipes based on the ingredients entered by the user."
                    },
                    new {
                        role = "user",
                        content = prompt
                    }
                },
                temperature = 0.7
            };

            var responseAI = await clientAI.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

            if (responseAI.IsSuccessStatusCode)
            {
                var result = await responseAI.Content.ReadFromJsonAsync<OpenAIResponse>();
                var content = result.choices[0].message.content;
                ViewBag.AIAnswer = content;
            }
            else
            {
                ViewBag.AIAnswer = "Error generating answer." + responseAI.StatusCode;
            }

            return View(message);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {

            var translationClient = _httpClientFactory.CreateClient();
            var apiKey = "";
            translationClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            try
            {
                var translationRequestBody = new
                {
                    inputs = createMessageDto.MessageDetail,
                };

                var translationJson = JsonConvert.SerializeObject(translationRequestBody);
                var translationContent = new StringContent(translationJson, System.Text.Encoding.UTF8, "application/json");

                var translationResponse = await translationClient.PostAsync("https://api-inference.huggingface.com/models/Helsinki-NLP/opus-mt-tr-en", translationContent);

                var translationResponseContent = await translationResponse.Content.ReadAsStringAsync();

                string englishText = createMessageDto.MessageDetail;
                if (translationResponseContent.TrimStart().StartsWith("["))
                {
                    var translateDoc = JsonDocument.Parse(translationResponseContent);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString() ?? string.Empty;
                    ViewBag.Translation = englishText;
                }


            }
            catch (Exception ex)
            {
                ViewBag.TranslationError = "Translation error: " + ex.Message;
            }


            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7285/api/Messages", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }

    }





}
