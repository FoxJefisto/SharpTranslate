using Microsoft.EntityFrameworkCore;
using SharpTranslate.Models.DatabaseModels;
using SharpTranslate.Models.DatabaseModels.Core;
using SharpTranslate.Repositories.Interfaces;

namespace SharpTranslate.Repositories
{
    public class UserWordPairRepository : IUserWordPairRepository
    {
        private readonly DataContext _context;

        public UserWordPairRepository(DataContext context)
        {
            _context = context;
        }
        public void AddUserWordPair(UserWordPair userWordPair)
        {
            _context.UsersWordPairs.Add(userWordPair);
            _context.SaveChanges();
        }

        public void DeleteUserWordPair(UserWordPair userWordPair)
        {
            _context.UsersWordPairs.Remove(userWordPair);
            _context.SaveChanges();
        }

        public List<UserWordPair> GetAllUserWordPairs()
        {
            return _context.UsersWordPairs.ToList();
        }

        public List<UserWordPair> GetUserWordPairs(User user)
        {
            return _context.UsersWordPairs.Where(x => x.UserId == user.Id).ToList();
        }

        public UserWordPair? GetUserWordPairByWordPair(User user, WordPair wordPair)
        {
            var result = _context.UsersWordPairs.FirstOrDefault(x => x.UserId == user.Id && x.WordPairId == wordPair.Id);
            return result;
        }

        public void UpdateUserWordPair(UserWordPair userWordPair)
        {
            _context.Attach(userWordPair);
            _context.Entry(userWordPair).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public UserWordPair? GetUserWordPairById(int id)
        {
            return _context.UsersWordPairs.FirstOrDefault(x => x.Id == id);
        }
    }
}
