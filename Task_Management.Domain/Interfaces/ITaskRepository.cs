
namespace Task_Management.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Task_Management.Models.Task>> GetAllTasksAsync();
        Task<Task_Management.Models.Task> GetTaskByIdAsync(int id);
        Task AddTaskAsync(Task_Management.Models.Task task);
        Task UpdateTaskAsync(Task_Management.Models.Task task);
        Task DeleteTaskAsync(int id);
    }
}
