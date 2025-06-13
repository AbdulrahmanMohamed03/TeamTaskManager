using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Interfaces
{
    public interface ITaskAssignmentRepository : IBaseRepository<TaskAssignment>
    {
        public TaskAssignment ExistingAssignment(int taskId, string userId);
        public List<User> GetUsersAssinedToTask(int taskId);
    }
}
