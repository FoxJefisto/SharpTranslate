using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Models;

namespace SharpTranslate.Controllers
{
    [Route("api/translate")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private ITranslateHelper _translateHelper;

        public TranslateController(ITranslateHelper translateHelper)
        {
            _translateHelper = translateHelper;
        }
        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody]Translate body)
        {
            try
            {
                var response = await _translateHelper.TranslateWordAsync(body);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
