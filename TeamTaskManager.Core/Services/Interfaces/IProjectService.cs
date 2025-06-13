using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.DTOs;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.Services.Interfaces
{
    public interface IProjectService
    {
        public Task<IEnumerable<ProjectDTO>> GetAllProjects();
        public Task<ProjectDTO> GetProjectById(int id);
        public Task<ProjectDTO> GetProjectByName(string name);
        public Task<ProjectDTO> AddProject(ProjectDTO projectDTO);
        public Task<ProjectDTO> UpdateProject(int id, ProjectDTO projectDTO);

        public Task<ProjectDTO> DeleteProject(int id);

        public Task<List<ProjectDTO>> FilterByDate(DateTime startDate, DateTime endDate);

        public Task<List<ProjectDTO>> GetCompletedProjects();
        public Task<List<ProjectDTO>> GetLateProjectsWithOpenTasks();
        public Task<List<UserDTO>> GetProjectUsers(int id);
        public Task<UserProjectsDTO> AddUserToProject(UserProjectsDTO userProjectsDTO);
    }
}
