using Boilerplate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Image> Images { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //To put the email has unique key
            /* modelBuilder.Entity<User>()
             .HasAlternateKey(u => u.Email);*/
        }
    }
}
