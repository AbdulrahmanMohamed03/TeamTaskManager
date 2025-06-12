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
            var result = await _projectService.GetAllProjects();
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
            var project = await _projectService.GetProjectById(id);
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
            var project = await _projectService.GetProjectByName(name);
            if (project == null)
            {
                return NotFound("There is no project with this name!");
            }
            return Ok(project);
        }

        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreatProject(ProjectDTO projectDTO) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var project  = await _projectService.AddProject(projectDTO);
            if (project.message != null) { 
                return BadRequest(project.message);
            }
            return Ok(project);
        }
        [HttpPut("UpdateProject")]
        public async Task<IActionResult> Update(int id, ProjectDTO projectDTO) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _projectService.UpdateProject(id, projectDTO);
            if (result.message != null) {
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _projectService.DeleteProject(id);

            if (result.message == "Project not found!" || result == null)
            {
                return BadRequest("Project not found!");
            }

            return Ok();
        }

        [HttpGet("FilterByDate")]
        public async Task<IActionResult> FilterByDate(DateTime startDate, DateTime endDateDate) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _projectService.FilterByDate(startDate, endDateDate);

            if (result.Count == 1 && result[0].message == "Invalid Date")
            {
                return BadRequest(result[0].message);
            }

            return Ok(result);

        }

        [HttpGet("GetCompletedProjects")]
        public async Task<IActionResult> GetCompletedProjects() {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _projectService.GetCompletedProjects();
            return Ok(result);
        }

        [HttpGet("GetLateProjectsWithOpenTasks")]
        public async Task<IActionResult> GetLateProjectsWithOpenTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _projectService.GetLateProjectsWithOpenTasks();
            return Ok(result);
        }

        [HttpGet("GetProjectUsers")]
        public async Task<IActionResult> GetProjectUsers(int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _projectService.GetProjectUsers(id);
            if (result == null) { 
                return NotFound("No Project with this ID");
            }
            return Ok(result);
        }
    }
}
