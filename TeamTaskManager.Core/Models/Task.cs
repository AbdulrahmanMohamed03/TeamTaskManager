namespace TeamTaskManager.Core.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        [Required]
        public TaskPriority Priority { get; set; }
        [Required]  
        public TaskStatus Status { get; set; }
    }
}
