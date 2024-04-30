using Microsoft.EntityFrameworkCore;
using SharpTranslate.Models.DatabaseModels;
using SharpTranslate.Models.DatabaseModels.Core;
using SharpTranslate.Repositories.Interfaces;

namespace SharpTranslate.Repositories
{
    public class WordPairRepository : IWordPairRepository
    {
        private readonly DataContext _context;

        public WordPairRepository(DataContext context)
        {
            _context = context;
        }

        public void AddWordPair(WordPair word)
        {
            _context.WordPairs.Add(word);
            _context.SaveChanges();
        }

        public void DeleteWordPair(WordPair word)
        {
            _context.WordPairs.Remove(word);
            _context.SaveChanges();
        }

        public List<WordPair> GetAllWordPairs()
        {
            return _context.WordPairs.ToList();
        }

        public WordPair? GetWordPairByWords(Word? word1, Word? word2)
        {
            var result = _context.WordPairs.FirstOrDefault(x => 
            x.OriginalWord!.WordName == word1!.WordName &&
            x.OriginalWord!.Language == word1!.Language &&
            x.TranslationWord!.WordName == word2!.WordName &&
            x.TranslationWord!.Language == word2!.Language);
            return result;
        }

        public void UpdateWordPair(WordPair word)
        {
            _context.Attach(word);
            _context.Entry(word).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
