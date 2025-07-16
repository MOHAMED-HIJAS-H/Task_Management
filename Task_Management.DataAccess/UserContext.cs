using Microsoft.EntityFrameworkCore;
using Task_Management.Models;

// Rename Task model to avoid conflict with System.Threading.Tasks.Task
using MyTask = Task_Management.Models.Task;

namespace Task_Management.DataAccess
{
    public class UserContext : DbContext
    {
        //This constructor takes options (like connection string) and passes to base DbContext class
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } //Maps to Users table
        public DbSet<MyTask> Tasks { get; set; } 
        public DbSet<Contact> Contacts { get; set; } //Contact class

        //customizes how tables and columns are created in the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Always call the base method to keep default behavior
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();
            });

            //Configure the Contact entity/table
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Email).IsRequired();
                entity.Property(c => c.Message).IsRequired();
            });

           
            modelBuilder.Entity<MyTask>(entity =>
            {
                entity.Property(t => t.Title).IsRequired();
            });
        }
    }

}
