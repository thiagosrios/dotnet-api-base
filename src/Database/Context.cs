using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context() {}
        public Context(DbContextOptions<Context> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ApiBase");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(e => e.Id);

            base.OnModelCreating(builder);
        }
    }
}
