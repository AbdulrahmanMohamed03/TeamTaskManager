using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Interfaces;

namespace TeamTaskManager.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        IProjectRepository Projects { get; }
        ITaskAssignmentRepository TaskAssignments { get; }

        int save();
    }
}
