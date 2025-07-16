using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Task_Management.Domain.Interfaces;
using Task_Management.Models;


namespace Task_Management.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly UserContext _context;

        public TaskRepository(UserContext context)
        {
            _context = context;
        }

        //Get All tasks from DB and ListAsync is an EF Core method that retrieves all records asynchronously
        public async Task<List<Task_Management.Models.Task>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        // Get a specific task by ID from the database 
        public async Task<Task_Management.Models.Task> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        //Add a new task to Tasks table and wait for changes to be saved asynchronously
        public async System.Threading.Tasks.Task AddTaskAsync(Task_Management.Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        //update an existing task and toggling the IsCompleted property(title,deadline)
        public async System.Threading.Tasks.Task UpdateTaskAsync(Task_Management.Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        //Delete a task by ID from the database
        public async System.Threading.Tasks.Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        // Add a contact to the Contacts table and wait for changes to be saved asynchronously
        public async System.Threading.Tasks.Task AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        // Get a user using email(login/registration) and returns null if user not found
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Add a new user to the Users table and wait for changes to be saved asynchronously
        public async System.Threading.Tasks.Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Get all tasks for a specific user by their email address
        public async Task<List<Task_Management.Models.Task>> GetTasksByUserEmailAsync(string email)
        {
            return await _context.Tasks.Where(t => t.UserEmail == email).ToListAsync();



        }
        // ✅ expose all tasks if needed
        //public IQueryable<Models.Task> Tasks => _context.Tasks;
    }
}
