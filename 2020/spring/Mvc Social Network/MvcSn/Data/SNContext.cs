using MvcSn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcSn.Data
{
    public class SNContext : IdentityDbContext<User>
    {
        public SNContext(DbContextOptions<SNContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.Posts)
                .WithOne(e => e.Sender)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>()
                .HasOne(e => e.Sender)
                .WithMany(c => c.Posts)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Comments)
                .WithOne(e => e.Sender)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
                .HasOne(e => e.Sender)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(c=> c.Comments)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
               .HasOne(e => e.Post)
               .WithMany(c => c.Comments)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasKey(c => new { c.Email });
            modelBuilder.Entity<Post>()
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<Comment>()
                .HasKey(c => new { c.Id });
        }*/
    }
}
