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

        public async Task<IEnumerable<ProjectDTO>> GetAll()
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

        public async Task<ProjectDTO> GetById(int id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            ProjectDTO projectDTO = new ProjectDTO();
            if (project != null)
            {
                projectDTO.StartDate = project.StartDate;
                projectDTO.EndDate = project.EndDate;
                projectDTO.Description = project.Description;
                projectDTO.Title = project.Title;
            }
            else projectDTO = null;
            return projectDTO;
        }

        public async Task<ProjectDTO> GetByName(string name)
        {
            var project = _unitOfWork.Projects.GetByName(name);
            ProjectDTO projectDTO = new ProjectDTO();
            if (project != null)
            {
                projectDTO.StartDate = project.StartDate;
                projectDTO.EndDate = project.EndDate;
                projectDTO.Description = project.Description;
                projectDTO.Title = project.Title;
            }
            else projectDTO = null;
            return projectDTO;
        }
    }
}
