using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Foodie.WebUI.Models
{
    public class ChatHub : Hub
    {
        private const string apiKey = "";
        private const string modelGpt = "gpt-3.5-turbo";

        private readonly IHttpClientFactory _httpClientFactory;

        public ChatHub(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private static readonly Dictionary<string, List<Dictionary<string, string>>> _history = new();

        public override Task OnConnectedAsync()
        {
            _history[Context.ConnectionId] = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    ["role"] = "system",
                    ["content"] = "You are a helpful assistant."
                }
            };

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _history.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string userMessage)
        {
            await Clients.Caller.SendAsync("ReceiveUserEcho", userMessage);
            var history = _history[Context.ConnectionId];
            history.Add(new Dictionary<string, string>
            {
                ["role"] = "user",
                ["content"] = userMessage
            });

            await StreamOpenAI(history, Context.ConnectionAborted);

        }

        public async Task StreamOpenAI(List<Dictionary<string, string>> history, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                model = modelGpt,
                messages = history,
                stream = true,
                temperature = 0.2
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var reader = new StreamReader(stream);
        }
    }
}
