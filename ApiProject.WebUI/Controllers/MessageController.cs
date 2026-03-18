using ApiProject.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiProject.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _huggingFaceApiKey;

        public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _huggingFaceApiKey = configuration["HuggingFaceSettings:ApiKey"]!;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7072/api/Messages");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7072/api/Messages", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7072/api/Messages?id=" + id);
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7072/api/Messages/GetMessage?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateMessageDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7072/api/Messages/", content);
            return RedirectToAction("MessageList");
        }



        public PartialViewResult SendMessage()
        {
            return PartialView();
        }


        // APIKEY'I CONFIGURE ET


        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {
            //HuggingFace'e istek atmak için client oluşturuyor, token ile kimlik doğruluyor.
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _huggingFaceApiKey);
            try
            {
                //Türkçe metni opus-mt-tr-en modeline gönderiyor.
                var translateRequestBodoy = new
                {
                    inputs = createMessageDto.MessageDetails
                };
                var translateJson = System.Text.Json.JsonSerializer.Serialize(translateRequestBodoy);
                var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");

                var translateResponse = await client.PostAsync("https://router.huggingface.co/hf-inference/models/Helsinki-NLP/opus-mt-tr-en", translateContent);
                var translateResponseString = await translateResponse.Content.ReadAsStringAsync();
                string englishText = createMessageDto.MessageDetails;
                //API'den gelen JSON'ı okuyup İngilizce metni alıyor.
                if (translateResponseString.TrimStart().StartsWith("["))
                {
                    var translateDoc = JsonDocument.Parse(translateResponseString);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                    //ViewBag.v = englishText;
                }

                var toxicRequestBody = new
                {
                    inputs = englishText
                };
                var toxicJson = System.Text.Json.JsonSerializer.Serialize(toxicRequestBody);
                var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
                var toxicResponse = await client.PostAsync("https://router.huggingface.co/hf-inference/models/unitary/toxic-bert", toxicContent);
                var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();

                if (toxicResponseString.TrimStart().StartsWith("["))
                {
                    var toxicDoc = JsonDocument.Parse(toxicResponseString);
                    foreach (var item in toxicDoc.RootElement[0].EnumerateArray())
                    {
                        string label = item.GetProperty("label").GetString();
                        double score = item.GetProperty("score").GetDouble();

                        if (score > 0.5)
                        {
                            createMessageDto.Status = "Toxic Message";
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(createMessageDto.Status))
                {
                    createMessageDto.Status = "Message's Received";
                }

            }
            catch
            {
                createMessageDto.Status = "Waiting for Approval";
            }

            var client2 = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client2.PostAsync("https://localhost:7072/api/Messages", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }
    }
}
