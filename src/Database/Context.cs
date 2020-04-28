using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Database
{
    public class Context : DbContext
    {
        public Context() {}
        public Context(DbContextOptions<Context> options) : base(options) {}

        public DbSet<Users> Users { get; set; }
    }
}
