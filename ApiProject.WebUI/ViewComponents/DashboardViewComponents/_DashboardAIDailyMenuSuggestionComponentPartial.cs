using ApiProject.WebUI.Dtos.AISuggestionDtos;
using ApiProject.WebUI.Dtos.CategoryDtos;
using ApiProject.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ApiProject.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardAIDailyMenuSuggestionComponentPartial : ViewComponent
    {
        private const string GROQ_API_KEY = "YOUR API KEY";
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardAIDailyMenuSuggestionComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groqClient = _httpClientFactory.CreateClient();
            groqClient.BaseAddress = new Uri("https://api.groq.com/openai/");
            groqClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GROQ_API_KEY);

            // Her istekte farklı seed → farklı sonuç
            var randomSeed = new Random().Next(1, 99999);

            string prompt = $@"
Generate a daily menu from 4 randomly selected world cuisines. (seed:{randomSeed})
IMPORTANT RULES:
- Select exactly 4 DIFFERENT cuisines each time.
- Choose from: Turkey, France, Germany, Italy, Spain, Portugal, Bulgaria, Georgia, Greece, Iran, China, Belgium, Russia, Romania, Switzerland, Denmark, India, Australia.
- Select DIFFERENT countries EVERY TIME.
- All content must be in ENGLISH.
- Write cuisine name in English (e.g., ""Italian Cuisine"").
- ISO Country Code is mandatory (e.g., IT, TR, BG, GE, GR).
- Be completely original, do not repeat previous selections.
- Response must be valid JSON only, no extra text.
JSON format:
[
  {{
    ""Cuisine"": ""X Cuisine"",
    ""CountryCode"": ""XX"",
    ""MenuTitle"": ""Daily Menu"",
    ""Items"": [
      {{ ""Name"": ""Dish 1"", ""Description"": ""Description"", ""Price"": 100 }},
      {{ ""Name"": ""Dish 2"", ""Description"": ""Description"", ""Price"": 120 }},
      {{ ""Name"": ""Dish 3"", ""Description"": ""Description"", ""Price"": 90 }},
      {{ ""Name"": ""Dish 4"", ""Description"": ""Description"", ""Price"": 70 }}
    ]
  }}
]
";

            var body = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
            new { role = "system", content = "You are a chef. Return valid JSON only, no markdown, no explanation." },
            new { role = "user", content = prompt }
        },
                temperature = 0.9  // ✅ Çeşitlilik için
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var response = await groqClient.PostAsync("v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            dynamic obj = JsonConvert.DeserializeObject(responseJson);
            string aiContent = obj.choices[0].message.content.ToString();

            // ```json bloğu temizle
            aiContent = aiContent
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            List<MenuSuggestionDto> menus;
            try
            {
                menus = JsonConvert.DeserializeObject<List<MenuSuggestionDto>>(aiContent);
            }
            catch
            {
                menus = new();
            }

            return View(menus);
        }

    }
}

