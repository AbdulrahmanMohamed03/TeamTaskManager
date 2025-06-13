using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskManager.Core.Models;
using TaskStatus = TeamTaskManager.Core.Models.TaskStatus;

namespace TeamTaskManager.Core.DTOs
{
    public class TaskDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? CompleteDate { get; set; }

        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public string? message {  get; set; }
    }
}
