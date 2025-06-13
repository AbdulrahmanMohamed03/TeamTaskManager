using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetById(string id);
        public Task<UserDTO> GetByName(string name);
        public Task<UserDTO> Delete(string id);
        public Task<UserDTO> Update(string id, UserDTO userDTO);
    }
}
