namespace TeamTaskManager.Core.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate  { get; set; }

        [Required]
        public DateTime EndDate { get; set; }


        public ICollection<UserProjects>? UserProjects { get; set; }
        public ICollection<Task>? Tasks { get; set; }

    }
}
