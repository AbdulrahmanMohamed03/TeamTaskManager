using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Models;
using TeamTaskManager.Core.Services.Interfaces;

namespace TeamTaskManager.Core.Services.Implementation
{
    public class UserService : IUserService
    {
        public readonly UserManager<User> _userManager;
        public readonly IUnitOfWork _unitOfWork;
        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDTO> Delete(string id)
        {
            var exist = await _userManager.FindByIdAsync(id);
            if (exist == null) {
                return new UserDTO { message = "User not found!" };
            }
           var result =  await _userManager.DeleteAsync(exist);
            if (!result.Succeeded)
            {
                return new UserDTO { message = "Error"};
            }
            return new UserDTO { message = "Deleted Successfully" };
        }


        public async Task<UserDTO> GetById(string id)
        {
            var exist = await _userManager.FindByIdAsync(id);
            if (exist == null) {
                return new UserDTO { message = "User Not found" };
            }
            var user = new UserDTO
            {
                FirstName = exist.FirstName,
                LastName = exist.LastName,
                Email = exist.Email
            };
            return user;
        }

        public async Task<UserDTO> GetByName(string name)
        {
            var exist = await _userManager.FindByNameAsync(name);
            if (exist == null)
            {
                return new UserDTO { message = "User Not found" };
            }
            var user = new UserDTO
            {
                FirstName = exist.FirstName,
                LastName = exist.LastName,
                Email = exist.Email
            };
            return user;
        }

        public async Task<UserDTO> Update(string id, UserDTO userDTO)
        {
            var exist = await _userManager.FindByIdAsync(id);
            if (exist == null)
            {
                return new UserDTO { message = "User is not found!" };
            }
            if (userDTO.FirstName == exist.FirstName
                && userDTO.LastName == exist.LastName
                && userDTO.Email == exist.Email) {
                return new UserDTO { message = "The are no changes in your input!" };
            }
            exist.FirstName = userDTO.FirstName;
            exist.LastName = userDTO.LastName;
            exist.Email = userDTO.Email;
            var result = await _userManager.UpdateAsync(exist);
            if (!result.Succeeded) {
                return new UserDTO { message = "Error" };
            }
            return userDTO;
        }
    }
}
