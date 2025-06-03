﻿namespace Webapi_EFCore.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }


        public ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
