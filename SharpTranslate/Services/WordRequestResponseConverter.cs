using SharpTranslate.Models;
using SharpTranslate.Models.DatabaseModels;
using SharpTranslate.Services.Interfaces;

namespace SharpTranslate.Services
{
    public class WordRequestResponseConverter : IWordRequestResponseConverter
    {
        public WordRequestResponse Convert(UserWordPair userWordPair)
        {
            var response = new WordRequestResponse
            {
                UserId = userWordPair.UserId,
                UserName = userWordPair.User?.UserName,
                UserWordPairId = userWordPair.Id,
                SourceWord = userWordPair.WordPair?.OriginalWord?.WordName,
                SourceLanguage = userWordPair.WordPair?.OriginalWord?.Language,
                TargetWord = userWordPair.WordPair?.TranslationWord?.WordName,
                TargetLanguage = userWordPair.WordPair?.TranslationWord?.Language
            };

            return response;
        }

        public List<WordRequestResponse> Convert(List<UserWordPair> userWordPairs)
        {
            var response = userWordPairs.Select(x => Convert(x)).ToList();

            return response;
        }
    }
}
