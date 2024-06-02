using App.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Metrics;
using SharpTranslate.Models;

namespace SharpTranslate.Controllers
{
    [Route("api/translate")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private readonly ILogger<TranslateController> _logger;
        private ITranslateHelper _translateHelper;
        private IMetrics _metrics;

        public TranslateController(ILogger<TranslateController> logger, ITranslateHelper translateHelper, IMetrics metrics)
        {
            _logger = logger;
            _translateHelper = translateHelper;
            _metrics = metrics;
        }
        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody]Translate body)
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.TranslateRequestCounter);
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);
            _logger.LogInformation("Получен запрос на перевод: {@Body}", body);

            try
            {
                var response = await _translateHelper.TranslateWordAsync(body);
                _logger.LogInformation("Перевод выполнен успешно: {@Response}", response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка перевода слова: {@Body}", body);
                return BadRequest(ex.Message);
            }
        }
    }
}
