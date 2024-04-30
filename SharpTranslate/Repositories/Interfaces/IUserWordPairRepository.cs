using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Repositories.Interfaces
{
    public interface IUserWordPairRepository
    {
        List<UserWordPair> GetAllUserWordPairs();

        void AddUserWordPair(UserWordPair userWordPair);

        void UpdateUserWordPair(UserWordPair userWordPair);

        List<UserWordPair> GetUserWordPairs(User user);

        UserWordPair? GetUserWordPairByWordPair(User user, WordPair wordPair);

        UserWordPair? GetUserWordPairById(int id);

        void DeleteUserWordPair(UserWordPair userWordPair);
    }
}
