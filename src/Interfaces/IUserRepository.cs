using ApiBase.Models;
using System.Collections.Generic;

namespace ApiBase.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        void SaveUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool UserExists(int id);
    }
}
