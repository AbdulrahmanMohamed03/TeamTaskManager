using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Models;

namespace TeamTaskManager.Core.DTOs
{
    public class UserProjectsDTO
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string? message { get; set; }
    }
}
