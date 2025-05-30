using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManager.Core.Interfaces
{
    public interface ITaskRepository: IBaseRepository<Models.Task>
    {
        public List<Models.Task> GetTasksByProjectId(int id);
        public List<Models.Task> GetTasksByUserId(string id);
        public Models.Task GetTaskByName(string name);
        public List<Models.Task> GetCompletedTasks();
        public List<Models.Task> GetUserCompletedTasks(string id);
        public List<Models.Task> GetAllComingTasks();
        public List<Models.Task> GetAllComingTasksByUser(string id);

        public List<Models.Task> GetDelayedTasks();

    }
}