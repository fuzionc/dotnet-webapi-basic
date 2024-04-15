using Microsoft.EntityFrameworkCore;

namespace TODO.WebApi.Models
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("User");
                user.HasKey(p => p.UserId);
                user.HasMany(u => u.UsersBooks).WithOne().OnDelete(DeleteBehavior.Cascade);


            });

            modelBuilder.Entity<Book>(book =>
            {
                book.ToTable("Book");
                book.HasKey(p => p.BookId);

            });
        }
    }
}
