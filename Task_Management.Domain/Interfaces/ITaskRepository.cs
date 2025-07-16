
using System.Security.Authentication;
using Task_Management.Models;


namespace Task_Management.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Task_Management.Models.Task>> GetAllTasksAsync();
        Task<Task_Management.Models.Task> GetTaskByIdAsync(int id);
        Task<List<Task_Management.Models.Task>> GetTasksByUserEmailAsync(string email);
        System.Threading.Tasks.Task AddTaskAsync(Task_Management.Models.Task task);
        System.Threading.Tasks.Task UpdateTaskAsync(Task_Management.Models.Task task);
        System.Threading.Tasks.Task DeleteTaskAsync(int id);

        System.Threading.Tasks.Task AddContact(Contact contact);

        System.Threading.Tasks.Task<User?> GetUserByEmailAsync(string email);

        System.Threading.Tasks.Task AddUserAsync(User user);


    }
}
