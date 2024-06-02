using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Metrics;
using SharpTranslate.Models;
using SharpTranslate.Repositories.Interfaces;
using SharpTranslate.Services.Interfaces;

namespace SharpTranslate.Controllers
{
    [Route("api/userwords")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly ILogger<UserWordsController> _logger;
        private IUserWordsManager _wordsManager;
        private IWordRequestResponseConverter _responseConverter;
        private ITranslateHelper _translateHelper;
        private IMetrics _metrics;

        public UserWordsController(ILogger<UserWordsController> logger, IUserWordsManager wordsManager, ITranslateHelper translateHelper, IWordRequestResponseConverter responseConverter, IMetrics metrics)
        {
            _logger = logger;
            _wordsManager = wordsManager;
            _translateHelper = translateHelper;
            _responseConverter = responseConverter;
            _metrics = metrics;
        }

        [HttpGet]
        public IActionResult GetAllUsersWords()
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.UserWordsRequestCounter);
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);

            _logger.LogInformation("Получен запрос на получение всех слов пользователя");

            var result = _wordsManager.GetAllUsersWordPairs();
            var response = _responseConverter.Convert(result!);

            _logger.LogInformation("Возврат всех пользовательских слов: {@Response}", response);

            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetWordsByUserId(int id)
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.UserWordsRequestCounter);
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);

            _logger.LogInformation("Получен запрос на получение слов по идентификатору пользователя: {UserId}", id);

            var result = _wordsManager.GetUserWordPairsByUserId(id);
            var response = _responseConverter.Convert(result!);

            _logger.LogInformation("Возврат слов по идентификатору пользователя: {UserId}, {@Response}", id, response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddWord([FromBody] WordRequestBody body)
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.UserWordsRequestCounter);
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);

            _logger.LogInformation("Получен запрос на добавление слова: {@Body}", body);

            try
            {
                var translateBody = new Translate
                {
                    Text = body.Word,
                    SourceLanguage = body.SourceLanguage,
                    TargetLanguage = body.TargetLanguage
                };

                if (body.TargetWord == null)
                {
                    var translationResponse = await _translateHelper.TranslateWordAsync(translateBody);
                    body.TargetWord = translationResponse?.TranslatedText;
                }

                var id = _wordsManager.AddWord(body);

                _logger.LogInformation("Слово успешно добавлено: {Id}, {@Body}", id, body);

                return CreatedAtAction(nameof(AddWord), new { id = id }, body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении слова: {@Body}", body);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userWordPairId}")]
        public IActionResult DeleteWord(int userWordPairId)
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.UserWordsRequestCounter);
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);

            _logger.LogInformation("Получен запрос на удаление слова: {UserWordPairId}", userWordPairId);

            try
            {
                _wordsManager.DeleteWord(userWordPairId);

                _logger.LogInformation("Слово успешно удалено: {UserWordPairId}", userWordPairId);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Ошибка удаления слова: {UserWordPairId}", userWordPairId);
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{userWordPairId}")]
        public IActionResult UpdateWord(int userWordPairId, [FromBody] WordRequestBody body) 
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.UserWordsRequestCounter);
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);

            _logger.LogInformation("Получен запрос на обновление слова: {UserWordPairId}, {@Body}", userWordPairId, body);

            try
            {
                var userWordPair = _wordsManager.UpdateWord(userWordPairId, body);
                var response = _responseConverter.Convert(userWordPair!);

                _logger.LogInformation("Слово успешно обновлено: {UserWordPairId}, {@Response}", userWordPairId, response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обновления слова: {UserWordPairId}, {@Body}", userWordPairId, body);
                return NotFound(ex.Message);
            }
        }
    }
}
