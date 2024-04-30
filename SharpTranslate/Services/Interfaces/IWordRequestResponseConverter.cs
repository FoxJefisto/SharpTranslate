using SharpTranslate.Models;
using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Services.Interfaces
{
    public interface IWordRequestResponseConverter
    {
        public WordRequestResponse Convert(UserWordPair userWordPair);

        public List<WordRequestResponse> Convert(List<UserWordPair> userWordPairs);
    }
}
