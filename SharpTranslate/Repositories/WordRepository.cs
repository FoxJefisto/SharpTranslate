using Microsoft.EntityFrameworkCore;
using SharpTranslate.Models.DatabaseModels;
using SharpTranslate.Models.DatabaseModels.Core;
using SharpTranslate.Repositories.Interfaces;

namespace SharpTranslate.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly DataContext _context;

        public WordRepository(DataContext context)
        {
            _context = context;
        }
        public void AddWord(Word word)
        {
            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public void DeleteWord(Word word)
        {
            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public List<Word> GetAllWords()
        {
            return _context.Words.ToList();
        }

        public Word? GetWordByWordName(string? wordName)
        {
            if (wordName == null)
                return null;
            var result = _context.Words.FirstOrDefault(x => x.WordName == wordName);
            return result;
        }

        public List<Word> GetWordsByLanguage(string languageCode)
        {
            return _context.Words.Where(x => x.Language == languageCode).ToList();
        }

        public void UpdateWord(Word word)
        {
            _context.Attach(word);
            _context.Entry(word).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
