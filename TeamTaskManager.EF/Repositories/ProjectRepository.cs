using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.EF.Context;

namespace TeamTaskManager.EF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Project Add(Project entity)
        {
            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            _context.Remove(id);
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public Project GetById(int id)
        {
            return _context.Projects.Find(id);
        }

        public Project GetByName(string name)
        {
            var project = _context.Projects.FirstOrDefault(n => n.Title == name);
            return project;
        }

        public List<Project> GetCompletedProjects()
        {
            var projects = _context.Projects
                .Where(p => p.Tasks.All(t => t.Status == Core.Models.TaskStatus.Completed))
                .ToList();

            return projects;
        }
        public List<Project> GetInRange(DateTime start, DateTime end)
        {
            var projects = _context.Projects
                .Where(d => d.StartDate >= start && d.EndDate<= end)
                .ToList();
            return projects;
        }

        public List<Project> GetLateProjectsWithOpenTasks()
        {
            var projects = _context.Projects
                .Where(p => p.Tasks.Any(t => t.Status == Core.Models.TaskStatus.ToDo || t.Status == Core.Models.TaskStatus.InProgress) && DateTime.Now > p.EndDate)
                .ToList();
            return projects;
        }

        public List<User> GetProjectUsers(int id)
        {
            var users = _context.UserProjects
                .Where(u => u.ProjectId == id)
                .Select(u => u.User)
                .Distinct()
                .ToList();
            return users;
        }

        public Project Update(Project entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
