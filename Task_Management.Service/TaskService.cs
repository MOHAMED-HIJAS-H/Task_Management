using Task_Management.Domain.Interfaces;

namespace Task_Management.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;

        public TaskService(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<List<Task_Management.Models.Task>> GetTasksAsync()
        {
            return await _taskRepo.GetAllTasksAsync();
        }

        public async Task AddNewTaskAsync(Task_Management.Models.Task task)
        {
            // Optional: Validation logic
            await _taskRepo.AddTaskAsync(task);
        }

        public async Task CompleteTaskAsync(int taskId)
        {
            var task = await _taskRepo.GetTaskByIdAsync(taskId);
            if (task != null)
            {
                task.IsCompleted = true;
                await _taskRepo.UpdateTaskAsync(task);
            }
        }
    }
}
