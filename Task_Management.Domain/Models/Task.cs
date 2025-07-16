using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class Task
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime? Deadline { get; set; }

        public string? UserEmail { get; set; } 

    }
}
