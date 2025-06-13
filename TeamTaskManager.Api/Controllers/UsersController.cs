using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Services.Interfaces;

namespace TeamTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id) {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            var result = await _userService.Delete(id);
            if (result.message != "Deleted Successfully") {
                return BadRequest(result.message);
            }
            return Ok(result.message);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.GetById(id);
            if (result.message != null) 
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.GetByName(id);
            if (result.message != null)
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(string id, UserDTO userDTO) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Update(id, userDTO);
            if (result.message != null)
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }
    }
}
