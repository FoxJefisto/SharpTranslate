using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Repositories.Interfaces
{
    public interface IWordPairRepository
    {
        List<WordPair> GetAllWordPairs();

        void AddWordPair(WordPair wordPair);

        void UpdateWordPair(WordPair wordPair);

        WordPair? GetWordPairByWords(Word? word1, Word? word2);

        void DeleteWordPair(WordPair wordPair);
    }
}
