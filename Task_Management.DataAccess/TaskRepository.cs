using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Task_Management.Domain.Interfaces;


namespace Task_Management.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly UserContext _context;

        public TaskRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<Task_Management.Models.Task>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task_Management.Models.Task> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task AddTaskAsync(Task_Management.Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Task_Management.Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
