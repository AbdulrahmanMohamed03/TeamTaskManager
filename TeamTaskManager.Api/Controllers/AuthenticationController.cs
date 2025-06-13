using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Services.Interfaces;

namespace TeamTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
         _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _authService.Register(registerDTO);
            if (result.Message != null) { 
            return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO) {
            if (!ModelState.IsValid) { 
            return BadRequest(ModelState);
            }
            var result = await _authService.Login(loginDTO);
            if (result.Message != null) { 
            return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("AddRole")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> AddRole([FromBody] string roleName) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _authService.AddRole(roleName);
            if (role.message != null) {
                return BadRequest(role.message);
            }
            return Ok(role);
        }

        [HttpPost("AssignToRole")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignToRole([FromBody] AssignRoleDTO assignRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = await _authService.AssignToRole(assignRoleDTO);
            if (role.message != null)
            {
                return BadRequest(role.message);
            }
            return Ok(role);
        }
    }
}
