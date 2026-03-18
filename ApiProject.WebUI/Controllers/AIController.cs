using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ApiProject.WebUI.Controllers
{
    public class AIController : Controller
    {
        public IActionResult CreateRecipeWithOpenAI()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
        {
            var apiKey = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] //AI'ya rol bazlı konuşma sağlar
                {
                    new
                    {
                        role = "system", //AI'ya karakter tanımı yapılır.
                        content = "You are an AI tool which recommends recipes to people who uses a restraunt website. Our goal is to come up with a recipe accordingly to the data provided as ingredients by the users."
                    },
                    new
                    {
                        role = "user", //kullanıcının verdiği prompt
                        content = prompt
                    }
                },
                temperature = 0.5 //Yaratıcılık düzeyi (0-1, 0.0=deterministik, 1.0=yaratıcı)
            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData); //OpenAI'ya istek atılır.
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                var content = result.choices[0].message.content; //Yapay zekanın verdiği yemek tarifi
                ViewBag.recipe = content;
            }
            else
            {
                ViewBag.recipe = "There has been an error: " + response.StatusCode;
            }

            return View();
        }

        //OpenAI'dan dönen JSON cevabını karşılamak için yazılan model sınıflar:

        public class OpenAIResponse
        {
            public List<Choice> choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
    }
}

