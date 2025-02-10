using Microsoft.EntityFrameworkCore;
using NotUyg.Entity;

namespace NotUyg.Data
{
    public class NotContext: DbContext
    {
        public NotContext(DbContextOptions<NotContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }

        public DbSet<Tag> Tag { get; set; }
        public DbSet<Not> Not { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }


    }
}
