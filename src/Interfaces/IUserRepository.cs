using ApiBase.Models;

namespace ApiBase.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool UserExists(int id);
    }
}
