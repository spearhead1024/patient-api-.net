using PatientAdviceAPI.Interfaces;
using PatientAdviceAPI.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PatientAdviceAPI.Services
{
    public class OpenAIService : ILLMService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenAISettings _settings;

        public OpenAIService(HttpClient httpClient, IOptions<OpenAISettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;

            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", _settings.ApiKey);
        }

        public async Task<string> GetAdviceAsync(string prompt)
        {
            var requestBody = new
            {
                model = _settings.ModelName,
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                },
                max_tokens = _settings.MaxTokens,
                temperature = _settings.Temperature
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await _httpClient.PostAsync("/v1/chat/completions", content);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);
                var advice = jsonResponse
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return advice ?? "No advice received from OpenAI.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
