using System.Collections.Generic;

namespace ApiBase.Interfaces
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Entity Get(int id);
        IEnumerable<Entity> GetAll();
        void Save(Entity user);
        void Update(Entity user);
        void Delete(Entity user);
    }
}
