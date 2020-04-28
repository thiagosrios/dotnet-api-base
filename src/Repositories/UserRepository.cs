using ApiBase.Database;
using ApiBase.Interfaces;
using ApiBase.Models;
using System.Collections.Generic;

namespace ApiBase.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context){}

        public User Get(int id)
        {
            return this.FindById(id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.FindAll();
        }

        public void Save(User user)
        {
            this.Save(user);
        }

        public void Update(User user)
        {
            this.Update(user);
        }

        public void Delete(User user)
        {
            this.Delete(user);
        }

        public bool UserExists(int id)
        {
            return this.Exists(x => x.Id == id);
        }
    }
}
