using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Repositories.Interfaces
{
    public interface IWordRepository
    {
        List<Word> GetAllWords();

        List<Word> GetWordsByLanguage(string? languageCode);

        void AddWord(Word word);

        void UpdateWord(Word word);

        Word? GetWordByWordName(string? wordName);

        void DeleteWord(Word word);
    }
}
