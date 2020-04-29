using ApiBase.Models;
using System.Collections.Generic;

namespace ApiBase.Interfaces
{
    public interface IUserService
    {
        User FindUserByLogin(string login);
        User GetUser(int id);
        List<User> GetUsers();
        void CreateUser(User user);
        void UpdateUser(int id, User user);
        void DeleteUser(User user);
    }
}
