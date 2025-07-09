
using Task_Management.Models;
using Task_Management;

namespace Task_Management.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<List<Task_Management.Models.Task>> GetTasksAsync();
        System.Threading.Tasks.Task AddNewTaskAsync(Task_Management.Models.Task task);
        System.Threading.Tasks.Task CompleteTaskAsync(int taskId);

        System.Threading.Tasks.Task DeleteTaskAsync(int taskId);

        System.Threading.Tasks.Task SubmitContact(Contact contact);

        Task<(bool Success, string Message)> RegisterUserAsync(User user);

        Task<User?> AuthenticateUserAsync(string email, string plainPassword);

    }
}
