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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProjectDTO> AddProject(ProjectDTO projectDTO)
        {
            var exist =  _unitOfWork.Projects.GetByName(projectDTO.Title);
            if (exist != null) {
                return new ProjectDTO { message = "This project Title is already exist!"};
            }
            if (projectDTO.StartDate >= projectDTO.EndDate) {
                return new ProjectDTO { message = "Invalid Date!" };
            }
            Project project = new Project { 
                Title = projectDTO.Title,
                StartDate = projectDTO.StartDate,
                EndDate = projectDTO.EndDate,
                Description = projectDTO.Description,
            };
            _unitOfWork.Projects.Add(project);
            _unitOfWork.save();
            return projectDTO;
        }

        public async Task<ProjectDTO> DeleteProject(int id)
        {
            var exist = _unitOfWork.Projects.GetById(id);
            if (exist == null) {
                return new ProjectDTO { message = "Project Not found!" };
            }
            _unitOfWork.Projects.Delete(id);
            _unitOfWork.save();
            return new ProjectDTO { message = "Deleted" };
        }

        public async Task<List<ProjectDTO>> FilterByDate(DateTime startDate, DateTime endDate)
        {
            var finalResult = new List<ProjectDTO>();
            if (startDate >= endDate) {
                finalResult.Add(new ProjectDTO { message = "Invalid Date" });
                return finalResult;
            }
            var projects = _unitOfWork.Projects.GetInRange(startDate, endDate);
            foreach (var project in projects) {
                ProjectDTO temp = new ProjectDTO
                {
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Title = project.Title,
                };
                finalResult.Add(temp);
            }
            return finalResult;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjects()
        {
            var projects = _unitOfWork.Projects.GetAll();
            List<ProjectDTO> final = new List<ProjectDTO>();
            if (projects != null) {
                foreach (var pr in projects) { 
                    var projectDTO = new ProjectDTO();
                    projectDTO.StartDate = pr.StartDate;
                    projectDTO.EndDate = pr.EndDate;
                    projectDTO.Description = pr.Description;
                    projectDTO.Title = pr.Title;


                    final.Add(projectDTO);
                }
            }
            return final;
        }

        public async Task<List<ProjectDTO>> GetCompletedProjects()
        {
            var result = _unitOfWork.Projects.GetCompletedProjects();
            var finalResult = new List<ProjectDTO>();
            foreach (var project in result)
            {
                ProjectDTO temp = new ProjectDTO
                {
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Title = project.Title,
                };
                finalResult.Add(temp);
            }
            return finalResult;
        }

        public async Task<List<ProjectDTO>> GetLateProjectsWithOpenTasks()
        {
            var result = _unitOfWork.Projects.GetLateProjectsWithOpenTasks();
            var finalResult = new List<ProjectDTO>();
            foreach (var project in result)
            {
                ProjectDTO temp = new ProjectDTO
                {
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Title = project.Title,
                };
                finalResult.Add(temp);
            }
            return finalResult;
        }

        public async Task<ProjectDTO> GetProjectById(int id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            ProjectDTO projectDTO = new ProjectDTO();
            if (project == null)
            {
                return new ProjectDTO { message = "Not Found!" };
            }
            projectDTO.StartDate = project.StartDate;
            projectDTO.EndDate = project.EndDate;
            projectDTO.Description = project.Description;
            projectDTO.Title = project.Title;
            return projectDTO;
        }

        public async Task<ProjectDTO> GetProjectByName(string name)
        {
            var project = _unitOfWork.Projects.GetByName(name);
            ProjectDTO projectDTO = new ProjectDTO();
            if (project == null)
            {
                return new ProjectDTO { message = "Not Found!" };
            }
            projectDTO.StartDate = project.StartDate;
            projectDTO.EndDate = project.EndDate;
            projectDTO.Description = project.Description;
            projectDTO.Title = project.Title;
            return projectDTO;
        }

        public async Task<List<UserDTO>> GetProjectUsers(int id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            if (project == null) {
                return null;
            }
            var users = new List<UserDTO>();
            var result = _unitOfWork.Projects.GetProjectUsers(id);
            foreach (var user in result) { 
                UserDTO userDTO = new UserDTO();
                userDTO.FirstName = user.FirstName;
                userDTO.LastName = user.LastName;
                userDTO.Email = user.Email;

                users.Add(userDTO);
            }
            return users;
        }

        public async Task<ProjectDTO> UpdateProject(int id, ProjectDTO projectDTO)
        {
            var exist = _unitOfWork.Projects.GetById(id);
            if (exist == null) {
                return new ProjectDTO { message = "No project with this ID" };
            }
            if (projectDTO.StartDate >= projectDTO.EndDate)
            {
                return new ProjectDTO { message = "Invalid Date!" };
            }
            ProjectDTO oldData = new ProjectDTO
            {
                Title = exist.Title,
                Description = exist.Description,
                StartDate = exist.StartDate,
                EndDate = exist.EndDate,
            };
            ProjectDTO newData = new ProjectDTO
            {
                Title = projectDTO.Title,
                Description = projectDTO.Description,
                StartDate = projectDTO.StartDate,
                EndDate = projectDTO.EndDate,
            };
            if (exist.Title == projectDTO.Title 
                && exist.Description == projectDTO.Description 
                && exist.StartDate == projectDTO.StartDate 
                && exist.EndDate == projectDTO.EndDate){
                return new ProjectDTO { message = "There are no changes" };}
            exist.Title = projectDTO.Title;
            exist.Description = projectDTO.Description;
            exist.StartDate = projectDTO.StartDate;
            exist.EndDate = projectDTO.EndDate;
            _unitOfWork.Projects.Update(exist);
            _unitOfWork.save();
            return newData;
        }
    }
}
