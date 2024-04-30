using SharpTranslate.Controllers;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Models;
using System.Text;
using System.Text.Json;

namespace SharpTranslate.Helpers
{
    public class TranslateHelper : ITranslateHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TranslateHelper> _logger;

        public TranslateHelper(IConfiguration configuration, ILogger<TranslateHelper> logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<TranslationResponse?> TranslateWordAsync(Translate body)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var serviceUrl = _configuration["TranslationServiceUrl"];
                var jsonBody = JsonSerializer.Serialize(body);
                _logger.LogTrace(jsonBody);
                var response = await httpClient.PostAsync(serviceUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                var responseBody = await response.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(responseBody);
                return translationResponse;
            }
        }
    }
}
