using Task_Management.Domain.Interfaces;
using Task_Management.Service.Helpers;
using Task_Management.Models;


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

        public async System.Threading.Tasks.Task AddNewTaskAsync(Task_Management.Models.Task task)
        {
            await _taskRepo.AddTaskAsync(task);
        }

        public async System.Threading.Tasks.Task CompleteTaskAsync(int taskId)
        {
            var task = await _taskRepo.GetTaskByIdAsync(taskId);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _taskRepo.UpdateTaskAsync(task);
            }
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int taskId)
        {

            var task = await _taskRepo.GetTaskByIdAsync(taskId);
            if (task != null)
            {
                await _taskRepo.DeleteTaskAsync(task.Id);
            }
            else
            {
                throw new Exception($"Task with ID {taskId} not found.");
            }


        }

        public async System.Threading.Tasks.Task SubmitContact(Contact contact)
        {

            await _taskRepo.AddContact(contact);

        }

        // NEW: Register user logic
        public async Task<(bool Success, string Message)> RegisterUserAsync(User user)
        {
            var existingUser = await _taskRepo.GetUserByEmailAsync(user.Email);

            if (existingUser != null)
            {
                return (false, "Email already exists.");
            }

            user.Password = PasswordHasher.Hash(user.Password);
            await _taskRepo.AddUserAsync(user);

            return (true, "Registration successful.");
        }


        public async Task<User?> AuthenticateUserAsync(string email, string plainPassword)
        {
            var user = await _taskRepo.GetUserByEmailAsync(email);

            if (user != null)
            {
                string hashedPassword = PasswordHasher.Hash(plainPassword);
                if (user.Password == hashedPassword)
                {
                    return user;
                }
            }

            return null; // Not authenticated
        }

        public async Task<List<Task_Management.Models.Task>> GetTasksForCalendarAsync()
        {
            var allTasks = await _taskRepo.GetAllTasksAsync();
            return allTasks
                .Where(t => t.Deadline != null)
                .OrderBy(t => t.Deadline)
                .ToList();
        }

        public async Task<List<Models.Task>> GetTasksByUserEmailAsync(string email)
        {
            return await _taskRepo.GetTasksByUserEmailAsync(email);
        }

    }
}