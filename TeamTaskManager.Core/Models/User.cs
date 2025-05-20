namespace TeamTaskManager.Core.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(64)]
        public string FirstName {  get; set; }
        [Required, MaxLength(64)]
        public string LastName { get; set; }

    }
}
