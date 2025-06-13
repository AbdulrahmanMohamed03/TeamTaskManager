using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.Core;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Models;
using TeamTaskManager.Core.Services.Interfaces;
using TeamTaskManager.EF;

namespace TeamTaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("AddTaskToProject")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddTaskToProject(int projectId, TaskDTO taskDTO) {
            if (!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            var result = await _taskService.AddTaskToProject(projectId, taskDTO);
            if (result.message != null) {
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpPost("AssignTaskToUser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AssignTaskToUser(TaskAssignmentDTO taskAssignmentDTO) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskService.AssignTaskToUser(taskAssignmentDTO);
            if (result.message != "Assigned Successfully!")
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpPut("UpdateTask")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTask(int taskId, TaskDTO taskDTO) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskService.UpdateTask(taskId, taskDTO);
            if (result.message != null)
            {
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpDelete("DeleteTask")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTask(int id) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var result = await _taskService.DeleteTask(id);
            if (result.message != "Deleted Successfully") { 
                return BadRequest(result.message);
            }
            return Ok(result);
        }

        [HttpGet("GetAllTasks")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllTasks() {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetAllTasks()).ToList();
            if (result.Count == 1 && result[0].message != null) {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }

        [HttpGet("GetCompletedTasks")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCompletedTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetCompletedTasks()).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }

        [HttpGet("GetAllComingTasks")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllComingTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetAllComingTasks()).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }
        
        [HttpGet("GetDelayedTasks")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetDelayedTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetDelayedTasks()).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }

        [HttpGet("GetAllComingTasksByUser")]
        [Authorize(Roles = "admin, Member")]
        public async Task<IActionResult> GetAllComingTasksByUser(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetAllComingTasksByUser(id)).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }

        [HttpGet("GetUserCompletedTasks")]
        [Authorize(Roles = "admin, Member")]
        public async Task<IActionResult> GetUserCompletedTasks(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetUserCompletedTasks(id)).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }

        [HttpGet("GetTasksByProjectId")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetTasksByProjectId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetTasksByProjectId(id)).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }


        [HttpGet("GetTasksByUserId")]
        [Authorize(Roles = "admin, Member")]
        public async Task<IActionResult> GetTasksByUserId(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = (await _taskService.GetTasksByUserId(id)).ToList();
            if (result.Count == 1 && result[0].message != null)
            {
                return BadRequest(result[0].message);
            }
            return Ok(result);
        }

        [HttpGet("GetTaskByName")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetTaskByName(string name) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _taskService.GetTaskByName(name);
            if (result.message != null)
            {
                return BadRequest(result.message);
            }
            return Ok(result);

        }
    }
}
