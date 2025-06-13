using System;
using System.Linq;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.Core.Models;
using TeamTaskManager.EF.Context;

namespace TeamTaskManager.EF.Repositories
{
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskAssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public TaskAssignment Add(TaskAssignment entity)
        {
            _context.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.TaskAssignments.Remove(entity);
        }

        public TaskAssignment ExistingAssignment(int taskId, string userId)
        {
            return _context.TaskAssignments
                           .FirstOrDefault(ta => ta.TaskId == taskId && ta.UserId == userId);
        }

        public IEnumerable<TaskAssignment> GetAll()
        {
            return _context.TaskAssignments.ToList();
        }

        public TaskAssignment GetById(int id)
        {
            return _context.TaskAssignments.Find(id);
        }

        public List<User> GetUsersAssinedToTask(int taskId)
        {
            return _context.TaskAssignments
                .Where(t => t.TaskId == taskId)
                .Select(t => t.User)
                .ToList();
        }

        public TaskAssignment Update(TaskAssignment entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
