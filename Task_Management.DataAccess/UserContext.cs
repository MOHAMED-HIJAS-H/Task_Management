using Microsoft.EntityFrameworkCore;
using Task_Management.Models; // ✅ Replace with your actual namespace
using MyTask = Task_Management.Models.Task;

namespace Task_Management.DataAccess // ✅ Match this with your folder & project name
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } //user class
        public DbSet<MyTask> Tasks { get; set; } //Task class
        public DbSet<Contact> Contacts { get; set; } //Contact class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Email).IsRequired();
                entity.Property(c => c.Message).IsRequired();
            });

            // Tasks - Title is optional
            modelBuilder.Entity<MyTask>(entity =>
            {
                entity.Property(t => t.Title).IsRequired(false);
            });
        }
    }

}
