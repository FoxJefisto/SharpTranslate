using Microsoft.AspNetCore.Mvc;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Models;
using SharpTranslate.Repositories.Interfaces;
using SharpTranslate.Services.Interfaces;

namespace SharpTranslate.Controllers
{
    [Route("api/userwords")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private IUserWordsManager _wordsManager;
        private IWordRequestResponseConverter _responseConverter;
        private ITranslateHelper _translateHelper;

        public UserWordsController(IUserWordsManager wordsManager, ITranslateHelper translateHelper, IWordRequestResponseConverter responseConverter)
        {
            _wordsManager = wordsManager;
            _translateHelper = translateHelper;
            _responseConverter = responseConverter;
        }

        [HttpGet]
        public IActionResult GetAllUsersWords()
        {
            var result = _wordsManager.GetAllUsersWordPairs();
            var response = _responseConverter.Convert(result!);
            return Ok(response);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetWordsByUserId(int id)
        {
            var result = _wordsManager.GetUserWordPairsByUserId(id);
            var response = _responseConverter.Convert(result!);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddWord([FromBody] WordRequestBody body)
        {
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

                return CreatedAtAction(nameof(AddWord), new { id = id }, body);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userWordPairId}")]
        public IActionResult DeleteWord(int userWordPairId)
        {
            try
            {
                _wordsManager.DeleteWord(userWordPairId);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{userWordPairId}")]
        public IActionResult UpdateWord(int userWordPairId, [FromBody] WordRequestBody body) 
        {
            try
            {
                var userWordPair = _wordsManager.UpdateWord(userWordPairId, body);
                var response = _responseConverter.Convert(userWordPair!);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
