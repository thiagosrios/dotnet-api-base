using ApiBase.Database;
using ApiBase.Interfaces;
using ApiBase.Models;
using System.Collections.Generic;

namespace ApiBase.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context){}

        public User GetUser(int id)
        {
            return this.FindById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.FindAll();
        }

        public void SaveUser(User user)
        {
            this.Save(user);
        }

        public void UpdateUser(User user)
        {
            this.UpdateUser(user);
        }

        public void DeleteUser(User user)
        {
            this.DeleteUser(user);
        }

        public bool UserExists(int id)
        {
            return this.Exists(x => x.Id == id);
        }
    }
}
