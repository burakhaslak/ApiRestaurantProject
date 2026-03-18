using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiProject.WebUI.Models
{
    public class ChatHub : Hub
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _modelGpt;
        public ChatHub(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["GroqSettings:ApiKey"]!;
            _modelGpt = configuration["GroqSettings:Model"]!;
        }

        private static readonly Dictionary<string, List<Dictionary<string, string>>> _history = new();

        public override Task OnConnectedAsync()
        {
            _history[Context.ConnectionId] = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                 ["role"] = "system",["content"] = "You are a helpful assistant. Keep answers concise."

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
            history.Add(new() { ["role"] = "user", ["content"] = userMessage });
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(60));
            await StreamOpenAI(history, cts.Token);
        }

        public async Task StreamOpenAI(List<Dictionary<string, string>> history, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("openai");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            var payload = new
            {
                model = _modelGpt,
                messages = history,
                stream = true,
                temperature = 0.2
            };

            using var req = new HttpRequestMessage(HttpMethod.Post, "v1/chat/completions");
            req.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            using var resp = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (!resp.IsSuccessStatusCode)
            {
                var errorBody = await resp.Content.ReadAsStringAsync();
                await Clients.Caller.SendAsync("ReceiveToken", $"HATA: {resp.StatusCode} - {errorBody}");
                return;
            }

            using var stream = await resp.Content.ReadAsStreamAsync(cancellationToken);
            using var reader = new StreamReader(stream);

            var sb = new StringBuilder();
            while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.StartsWith("data:")) continue;
                var data = line["data:".Length..].Trim();
                if (data == "[DONE]") break;

                try
                {
                    var chunk = JsonSerializer.Deserialize<ChatStreamChunk>(data);
                    var delta = chunk?.Choices?.FirstOrDefault()?.Delta?.Content;
                    if (!string.IsNullOrEmpty(delta))
                    {
                        sb.Append(delta);
                        await Clients.Caller.SendAsync("ReceiveToken", delta, cancellationToken);
                    }
                }
                catch
                {
                    //Hata Mesajları
                }
            }

            var full = sb.ToString();
            history.Add(new() { ["role"] = "assistant", ["content"] = full });
            await Clients.Caller.SendAsync("CompleteMessage", full, cancellationToken);
        }

        //Stream Parse Modelleri
        private sealed class ChatStreamChunk
        {
            [JsonPropertyName("choices")] public List<Choice>? Choices { get; set; }
        }
        private sealed class Choice
        {
            [JsonPropertyName("delta")] public Delta? Delta { get; set; }
            [JsonPropertyName("finish_reason")] public string? FinishReason { get; set; }
        }

        private sealed class Delta
        {
            [JsonPropertyName("content")] public string? Content { get; set; }
            [JsonPropertyName("role")] public string? Role { get; set; }
        }
    }
}

