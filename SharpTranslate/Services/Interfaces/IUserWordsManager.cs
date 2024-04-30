using SharpTranslate.Models;
using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Services.Interfaces
{
    public interface IUserWordsManager
    {
        int? AddWord(WordRequestBody? body);

        void DeleteWord(int userWordPairId);

        List<UserWordPair?> GetAllUsersWordPairs();

        List<UserWordPair?> GetUserWordPairsByUserId(int userId);

        UserWordPair? UpdateWord(int userWordId, WordRequestBody body);
    }
}
