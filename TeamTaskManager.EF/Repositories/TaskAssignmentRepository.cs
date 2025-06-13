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

        public TaskAssignment Add(int taskId, string userId)
        {
            var tskAssign = new TaskAssignment
            {
                TaskId = taskId,
                UserId = userId
            };
            _context.Add(tskAssign);
            return tskAssign;
        }

        public TaskAssignment ExistingAssignment(int taskId, string userId)
        {
            return _context.TaskAssignments
                           .FirstOrDefault(ta => ta.TaskId == taskId && ta.UserId == userId);
        }

        public List<User> GetUsersAssinedToTask(int taskId)
        {
            return _context.TaskAssignments
                .Where(t => t.TaskId == taskId)
                .Select(t => t.User)
                .ToList();
        }
    }
}
