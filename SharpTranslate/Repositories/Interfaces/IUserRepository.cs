using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User? GetUserById(int id);

        User? GetUserByName(string username);

        void AddUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);
    }
}
