using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Services.Interfaces
{
    public interface ITaskService
    {
        public Task<IEnumerable<TaskDTO>> GetAllTasks();
        public Task<IEnumerable<TaskDTO>> GetCompletedTasks();
        public Task<IEnumerable<TaskDTO>> GetAllComingTasks();
        public Task<IEnumerable<TaskDTO>> GetDelayedTasks();
        public Task<IEnumerable<TaskDTO>> GetAllComingTasksByUser(string id);
        public Task<IEnumerable<TaskDTO>> GetUserCompletedTasks(string id);
        public Task<IEnumerable<TaskDTO>> GetTasksByProjectId(int id);
        public Task<IEnumerable<TaskDTO>> GetTasksByUserId(string id);

        public Task<TaskDTO> GetTaskByName(string name);

        public Task<TaskDTO> AddTaskToProject(int projectId, TaskDTO taskDTO);
        public Task<TaskAssignmentDTO> AssignTaskToUser(int taskId, string userId);
        public Task<TaskDTO> UpdateTask(int taskId, TaskDTO taskDTO);
        public Task<TaskDTO> DeleteTask(int id);
    }
}
