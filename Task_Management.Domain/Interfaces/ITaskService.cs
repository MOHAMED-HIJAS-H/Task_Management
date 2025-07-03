
namespace Task_Management.Domain.Interfaces
{
    public interface ITaskService
    {
        Task<List<Task_Management.Models.Task>> GetTasksAsync();
        Task AddNewTaskAsync(Task_Management.Models.Task task);
        Task CompleteTaskAsync(int taskId);
    }
}
