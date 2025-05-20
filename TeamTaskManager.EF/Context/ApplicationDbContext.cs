namespace TeamTaskManager.EF.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Core.Models.Task> Tasks { get; set; }
        public DbSet<UserProjects> UserProjects { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProjects>()
                .HasKey(u => new { u.UserId, u.ProjectId });
            modelBuilder.Entity<UserProjects>()
                .HasOne(u => u.Project)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(u => u.ProjectId);
            modelBuilder.Entity<UserProjects>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<TaskAssignment>()
                .HasKey(t => new { t.TaskId, t.UserId });

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(t => t.Task)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(t => t.TaskId);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(t => t.User)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(t => t.UserId);
        }
    }
}
