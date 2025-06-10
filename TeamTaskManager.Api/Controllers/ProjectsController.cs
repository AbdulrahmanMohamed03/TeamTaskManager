using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Services.Implementation;
using TeamTaskManager.Core.Services.Interfaces;

namespace TeamTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("GettAllProjects")]
        public async Task<IActionResult> GettAll() {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _projectService.GetAll();
            if (result == null) {
                return BadRequest("No Projects");
            }
            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromBody] int id ) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var project = await _projectService.GetById(id);
            if (project == null) {
                return NotFound("There is no project with this ID!");
            }
            return Ok(project);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName([FromBody] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var project = await _projectService.GetByName(name);
            if (project == null)
            {
                return NotFound("There is no project with this name!");
            }
            return Ok(project);
        }
    }
}
