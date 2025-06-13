using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.EF.Context;

namespace TeamTaskManager.EF.Repositories
{
    public class UserProjectsRepository : IUserProjectsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProjectsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserProjects Add(UserProjects entity)
        {
            _context.UserProjects.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            _context.UserProjects.Remove(entity);
        }

        public UserProjects ExistingAssignment(int projectId, string userId)
        {
            return _context.UserProjects
               .FirstOrDefault(pr => pr.ProjectId == projectId && pr.UserId == userId);
        }

        public IEnumerable<UserProjects> GetAll()
        {
            return _context.UserProjects.ToList();
        }

        public UserProjects GetById(int id)
        {
            return _context.UserProjects.Find(id);
        }

        public UserProjects Update(UserProjects entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
