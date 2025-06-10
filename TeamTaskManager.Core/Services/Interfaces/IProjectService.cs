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
        public Task<IEnumerable<ProjectDTO>> GetAll();
        public Task<ProjectDTO> GetById(int id);
        public Task<ProjectDTO> GetByName(string name);

    }
}
