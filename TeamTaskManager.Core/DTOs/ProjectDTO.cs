﻿namespace TeamTaskManager.Core.DTOs
{
    public class ProjectDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string? message { get; set; }
    }
}
