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
    }
}
