using System.ComponentModel.DataAnnotations;

namespace Task_Management.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        public string? UserEmail { get; set; } // optional: link to logged-in user
    }
}
