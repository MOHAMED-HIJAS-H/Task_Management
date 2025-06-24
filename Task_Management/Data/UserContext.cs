using Microsoft.EntityFrameworkCore;
using Task_Management.Models; // ✅ Replace with your actual namespace

namespace Task_Management.Data // ✅ Match this with your folder & project name
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
