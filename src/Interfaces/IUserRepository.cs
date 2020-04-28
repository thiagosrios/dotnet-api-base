using ApiBase.Models;

namespace ApiBase.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        bool UserExists(int id);
    }
}
