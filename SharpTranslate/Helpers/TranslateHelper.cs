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

        public string ServiceUrl { get; set; }

        public List<string> AvailableLanguages { get; set; }

        public TranslateHelper(IConfiguration configuration, ILogger<TranslateHelper> logger) 
        {
            _configuration = configuration;
            _logger = logger;
            ServiceUrl = _configuration["TranslationServiceUrl"];
            AvailableLanguages = _configuration["AVAILABLE_LANGUAGES"].Split(',').ToList();
        }
        public async Task<TranslationResponse?> TranslateWordAsync(Translate body)
        {
            if(!AvailableLanguages.Any(x => x == body.SourceLanguage) || !AvailableLanguages.Any(x => x == body.TargetLanguage))
            {
                throw new ArgumentException($"Данный язык не поддерживается.\nПоддерживаемые языки: {string.Join(", ", AvailableLanguages)}");
            }
            using (HttpClient httpClient = new HttpClient())
            {
                var jsonBody = JsonSerializer.Serialize(body);
                _logger.LogTrace(jsonBody);
                var response = await httpClient.PostAsync(ServiceUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                var responseBody = await response.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(responseBody);
                return translationResponse;
            }
        }
    }
}
