using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Projektverwaltung.Service
{
    public class AiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AiService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GenerateDescription(string title)
        {
            var apiKey =
                _configuration["OpenRouter:ApiKey"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            _httpClient.DefaultRequestHeaders.Add(
                "HTTP-Referer",
                "http://localhost:5000");

            var requestBody = new
            {
                model = "openrouter/free",

                messages = new[]
                {
                     new {
                    role = "user",
                    content = $"Write a short, concise project description in English (maximum 3 sentences) for a project titled: '{title}'. Use plain text only. Do not include any markdown format, headings, bullet points, or tables."
                 }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                content);

            var responseString =
                await response.Content.ReadAsStringAsync();


            System.Diagnostics.Debug.WriteLine(responseString);


            using var document =
                JsonDocument.Parse(responseString);

            return document
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
        }

    }
}
