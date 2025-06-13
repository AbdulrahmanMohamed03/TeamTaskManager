using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Models;
using TeamTaskManager.Core.Services.Interfaces;
using Task = TeamTaskManager.Core.Models.Task;

namespace TeamTaskManager.Core.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public TaskService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<TaskDTO> AddTaskToProject(int projectId, TaskDTO taskDTO)
        {
            var project = _unitOfWork.Projects.GetById(projectId);
            if (project == null) {
                return new TaskDTO { message = "There is  not project with this ID!" };
            }
            var tsk = _unitOfWork.Tasks.GetTaskByName(taskDTO.Name);
            if (tsk != null) {
                return new TaskDTO { message = "This Task Name already exists!" };
            }
            if (taskDTO.StartDate >= taskDTO.EndDate) {
                return new TaskDTO { message = "Invalid Date!" };
            }
            if (taskDTO.CompleteDate != null) {
                if (taskDTO.CompleteDate <= taskDTO.StartDate) {
                    return new TaskDTO { message = "Task End Date Cannot be less than Start Date!" };
                }
            }

            Task task = new Task { 
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                StartDate = taskDTO.StartDate,
                EndDate = taskDTO.EndDate,
                Priority = taskDTO.Priority,
                Status = taskDTO.Status,
                ProjectId = projectId
            };

            _unitOfWork.Tasks.Add(task);
            _unitOfWork.save();
            return taskDTO;

        }

        public async Task<TaskAssignmentDTO> AssignTaskToUser(TaskAssignmentDTO taskAssignmentDTO)
        {
            var exist = _unitOfWork.TaskAssignments.ExistingAssignment(taskAssignmentDTO.TaskId, taskAssignmentDTO.UserId);
            if (exist != null) {
                return new TaskAssignmentDTO { message = "This task is already assigned to this user!" };
            }
            var user = await _userManager.FindByIdAsync(taskAssignmentDTO.UserId);
            if (user == null) {
                return new TaskAssignmentDTO { message = "This user is not found!" };
            }
            var task = _unitOfWork.Tasks.GetById(taskAssignmentDTO.TaskId);
            if (task == null)
            {
                return new TaskAssignmentDTO { message = "This task is not found!" };
            }
            var tsk = new TaskAssignment
            {
                UserId = taskAssignmentDTO.UserId,
                TaskId = taskAssignmentDTO.TaskId,
            };
            _unitOfWork.TaskAssignments.Add(tsk);
            _unitOfWork.save();
            return new TaskAssignmentDTO { TaskId = taskAssignmentDTO.TaskId,UserId = taskAssignmentDTO.UserId ,message = "Assigned Successfully!" };
        }

        public async Task<TaskDTO> DeleteTask(int id)
        {
            var exist = _unitOfWork.Tasks.GetById(id);
            if (exist == null) {
                return new TaskDTO { message = "No Task with this ID" };
            }
            _unitOfWork.Tasks.Delete(id);
            _unitOfWork.save();
            return new TaskDTO { message = "Deleted Successfully" };
        }

        public async Task<IEnumerable<TaskDTO>> GetAllComingTasks()
        {
            var tasks = _unitOfWork.Tasks.GetAllComingTasks();
            if (tasks == null) {
                return new TaskDTO[] { new TaskDTO { message = "No Coming Tasks" } };
            }
            List<TaskDTO> tmp = new List<TaskDTO>(); 
            foreach (var task in tasks) {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllComingTasksByUser(string id)
        {
            var tasks = _unitOfWork.Tasks.GetAllComingTasksByUser(id);
            if (tasks == null)
            {
                return new TaskDTO[] { new TaskDTO { message = "No Coming Tasks for this user!" } };
            }
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasks()
        {
            var tasks = _unitOfWork.Tasks.GetAll();
            if (tasks == null)
            {
                return new TaskDTO[] { new TaskDTO { message = "No available Tasks!" } };
            }
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;
                taskDTO.CompleteDate = task.CompleteDate;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<IEnumerable<TaskDTO>> GetCompletedTasks()
        {
            var tasks = _unitOfWork.Tasks.GetCompletedTasks();
            if (tasks == null)
            {
                return new TaskDTO[] { new TaskDTO { message = "No Completed Tasks" } };
            }
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;
                taskDTO.CompleteDate = task.CompleteDate;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<IEnumerable<TaskDTO>> GetDelayedTasks()
        {
            var tasks = _unitOfWork.Tasks.GetDelayedTasks();
            if (tasks == null)
            {
                return new TaskDTO[] { new TaskDTO { message = "No Coming Tasks" } };
            }
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<TaskDTO> GetTaskByName(string name)
        {
            var task = _unitOfWork.Tasks.GetTaskByName(name);
            if (task == null) {
                return new TaskDTO { message = "No task with this name!" };
            }
            TaskDTO taskDTO = new TaskDTO {
                Status = task.Status,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority,
                CompleteDate = task.CompleteDate,
            };
            return taskDTO;
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByProjectId(int id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            if (project == null) {
                return new TaskDTO[] { new TaskDTO { message = "This Project is not found!" } };
            }
            var tasks = _unitOfWork.Tasks.GetTasksByProjectId(id);
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;
                taskDTO.CompleteDate = task.CompleteDate;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new TaskDTO[] { new TaskDTO { message = "This User is not found!" } };
            }
            var tasks = _unitOfWork.Tasks.GetTasksByUserId(id);
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;
                taskDTO.CompleteDate = task.CompleteDate;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<IEnumerable<TaskDTO>> GetUserCompletedTasks(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new TaskDTO[] { new TaskDTO { message = "This User is not found!" } };
            }
            var tasks = _unitOfWork.Tasks.GetUserCompletedTasks(id);
            List<TaskDTO> tmp = new List<TaskDTO>();
            foreach (var task in tasks)
            {
                var taskDTO = new TaskDTO();
                taskDTO.Status = task.Status;
                taskDTO.StartDate = task.StartDate;
                taskDTO.EndDate = task.EndDate;
                taskDTO.Name = task.Name;
                taskDTO.Description = task.Description;
                taskDTO.Priority = task.Priority;
                taskDTO.CompleteDate = task.CompleteDate;

                tmp.Add(taskDTO);
            }
            return tmp;
        }

        public async Task<TaskDTO> UpdateTask(int taskId, TaskDTO taskDTO)
        {
            var exist = _unitOfWork.Tasks.GetById(taskId);
            if (exist == null) {
                return new TaskDTO { message = "Task is not found!" };
            }
            if (taskDTO.Status == exist.Status
                && taskDTO.StartDate == exist.StartDate
                && taskDTO.EndDate == exist.EndDate
                && taskDTO.CompleteDate == exist.CompleteDate
                && taskDTO.Description == exist.Description
                && taskDTO.Name == exist.Name
                && taskDTO.Priority == exist.Priority) {
                return new TaskDTO { message = "There are no updates in your input!" };
            }
            if (taskDTO.StartDate >= taskDTO.EndDate) {
                return new TaskDTO { message = "Invalud Date!" };
            }
            exist.Name = taskDTO.Name;
            exist.Status = taskDTO.Status;
            exist.StartDate = taskDTO.StartDate;
            exist.EndDate = taskDTO.EndDate;
            exist.Priority = taskDTO.Priority;
            exist.CompleteDate = taskDTO.CompleteDate;
            exist.Description = taskDTO.Description;
            _unitOfWork.Tasks.Update(exist);
            _unitOfWork.save();
            return taskDTO;
        }
    }
}
