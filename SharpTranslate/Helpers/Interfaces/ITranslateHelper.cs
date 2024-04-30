using SharpTranslate.Models;

namespace SharpTranslate.Helpers.Interfaces
{
    public interface ITranslateHelper
    {
        Task<TranslationResponse?> TranslateWordAsync(Translate translateBody);
    }
}
