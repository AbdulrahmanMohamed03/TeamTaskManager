using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        Task<User> GetById(string id);
        void Delete(string id);
        User Update(User entity);
    }
}
