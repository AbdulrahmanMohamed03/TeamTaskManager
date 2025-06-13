using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TeamTaskManager.Core;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.Core.Models;
using TeamTaskManager.EF.Context;
using TeamTaskManager.EF.Repositories;

namespace TeamTaskManager.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Tasks = new TaskRepository(context);
            Projects = new ProjectRepository(context);
            TaskAssignments = new TaskAssignmentRepository(context);
        }
        public ITaskRepository Tasks { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public ITaskAssignmentRepository TaskAssignments { get; private set; }
        public void Dispose()
        {
            context.Dispose();
        }

        public int save()
        {
            return context.SaveChanges();
        }
    }
}
