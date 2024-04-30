using Microsoft.EntityFrameworkCore;
using SharpTranslate.Models.DatabaseModels;
using SharpTranslate.Models.DatabaseModels.Core;
using SharpTranslate.Repositories.Interfaces;

namespace SharpTranslate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public User? GetUserByName(string username)
        {
            return _context.Users.FirstOrDefault(user => user.UserName == username);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(user => user.Id == id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}
