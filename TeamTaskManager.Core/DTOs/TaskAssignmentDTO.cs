using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTaskManager.Core.DTOs
{
    public class TaskAssignmentDTO
    {
        public string UserId { get; set; }
        public string TaskId { get; set; }
        public string? message { get; set; }  
    }
}
