using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        public Project GetByName(string name);
        public List<Project> GetInRange(DateTime start, DateTime end);
        public List<Project> GetCompletedProjects();
        public List<Project> GetLateProjectsWithOpenTasks();
        public List<User> GetProjectUsers(int id);
    }
}
