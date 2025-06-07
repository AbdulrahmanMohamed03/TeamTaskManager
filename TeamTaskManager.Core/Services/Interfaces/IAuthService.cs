using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;

namespace TeamTaskManager.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDTO> Register(RegisterDTO registerDTO);
        Task<TokenDTO> Login(LoginDTO loginDTO);
        //Task<TokenDTO> RefreshToken(string token);
        //Task<bool> RevokeToken(string token);
    }
}
