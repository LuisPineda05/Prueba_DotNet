using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PruebaTecnica.Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string HuggingFaceApiKey = "PON TU CLAVE";
        private const string HuggingFaceUrl = "https://api-inference.huggingface.co/models/mistralai/Mistral-Nemo-Instruct-2407"; //google/flan-t5-large    -> Opcion mas ligera

        [HttpPost("response")]
        public async Task<IActionResult> GetResponse([FromBody] AiRequest request)
        {
            var body = new
            {
                inputs = request.Input
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {HuggingFaceApiKey}");

            var response = await _httpClient.PostAsync(HuggingFaceUrl, content);
            var responseData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, responseData);
            }

            var responseList = JsonConvert.DeserializeObject<List<AiHuggingFaceResponse>>(responseData);
            var answer = responseList?[0]?.GeneratedText ?? "No se pudo generar una respuesta.";

            return Ok(new { answer });
        }
    }

    public class AiRequest
    {
        public string Input { get; set; }
    }

    public class AiHuggingFaceResponse
    {
        [JsonProperty("generated_text")]
        public string GeneratedText { get; set; }
    }
}
