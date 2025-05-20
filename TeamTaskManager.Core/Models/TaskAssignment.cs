namespace TeamTaskManager.Core.Models
{
    public class TaskAssignment
    {
        public int TaskId { get; set; }
        public Task Task { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}
