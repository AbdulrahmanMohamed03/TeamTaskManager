using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Interfaces;
using TeamTaskManager.EF.Context;

namespace TeamTaskManager.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Delete(string id)
        {
            var user =  GetById(id);
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(string id)
        {
            var user = context.Users.Find(id);
            return user;
        }

        public User GetByName(string name)
        {
            var user = context.Users.FirstOrDefault(u => u.FirstName == name);
            return user;
        }

        public User Update(User entity)
        {
            context.Users.Update(entity);
            return entity;
        }
    }
}
