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

        public async void Delete(string id)
        {
            var user = await GetById(id);
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public async Task<User> GetById(string id)
        {
            var users = context.Users.FirstOrDefault(u => u.Id == id);
            return users;
        }

        public User Update(User entity)
        {
            context.Users.Update(entity);
            return entity;
        }
    }
}
