using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.EF.Context;

namespace TeamTaskManager.EF.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext context;
        public TaskRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Core.Models.Task Add(Core.Models.Task entity)
        {
            context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            context.Remove(entity);
        }

        public IEnumerable<Core.Models.Task> GetAll()
        {
            return context.Tasks.AsNoTracking().ToList();
        }

        public List<Core.Models.Task> GetAllComingTasks()
        {
            var tsks = context.Tasks.Where(t=>t.EndDate > DateTime.Now && t.CompleteDate == null).ToList();
            return tsks;
        }

        public List<Core.Models.Task> GetAllComingTasksByUser(string id)
        {
            var tsks = context.TaskAssignments
                .Where(u => u.UserId == id && u.Task.EndDate > DateTime.Now && u.Task.CompleteDate == null)
                .Select(u => u.Task)
                .ToList();
            return tsks;
        }

        public Core.Models.Task GetById(int id)
        {
            return context.Tasks.Find(id);
        }

        public Core.Models.Task GetTaskByName(string name)
        {
            var tsk = context.Tasks.FirstOrDefault(s => s.Name == name);
            return tsk;
        }

        public List<Core.Models.Task> GetTasksByProjectId(int id)
        {
            var tsks = context.Tasks.Where(t => t.ProjectId == id).ToList();
            return tsks;
        }

        public List<Core.Models.Task> GetTasksByUserId(string id)
        {
            var tsks = context.TaskAssignments
                .Where(t => t.UserId == id)
                .Select(t => t.Task)
                .ToList();
            return tsks;
        }

        public List<Core.Models.Task> GetCompletedTasks()
        {
            var tsks = context.Tasks.Where(t => t.Status == Core.Models.TaskStatus.Completed).ToList();
            return tsks;
        }

        public List<Core.Models.Task> GetUserCompletedTasks(string id)
        {
            var tsks = context.TaskAssignments
                .Where(u => u.UserId == id && u.Task.Status == Core.Models.TaskStatus.Completed)
                .Select(u => u.Task)
                .ToList();
            return tsks;
        }

        public Core.Models.Task Update(Core.Models.Task entity)
        {
            context.Update(entity);
            return entity;
        }

        public List<Core.Models.Task> GetDelayedTasks()
        {
            var tsks = context.Tasks
                .Where(t => DateTime.Now > t.EndDate && t.CompleteDate == null)
                .ToList();
            return tsks;
        }
    }
}
