using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context() {}
        public Context(DbContextOptions<Context> options) : base(options) {}
    }
}
