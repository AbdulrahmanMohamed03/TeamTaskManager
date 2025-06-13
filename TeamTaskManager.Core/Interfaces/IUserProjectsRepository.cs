using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Interfaces
{
    public interface IUserProjectsRepository : IBaseRepository<UserProjects>
    {
        public UserProjects ExistingAssignment(int projectId, string userId);
    }
}
