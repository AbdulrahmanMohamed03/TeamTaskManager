namespace TeamTaskManager.Core.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(64)]
        public string FirstName {  get; set; }
        [Required, MaxLength(64)]
        public string LastName { get; set; }

        public ICollection<UserProjects> UserProjects { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public IList<RefreshToken> RefreshTokens { get; set; }
    }
}
