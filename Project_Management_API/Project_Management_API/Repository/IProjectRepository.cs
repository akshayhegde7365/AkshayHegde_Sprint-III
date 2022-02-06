using Project_Management_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Repository
{
    public interface IProjectRepository
    {
        void UpdateProject(int id, Project Project);
        int DeleteProject(int? ProjectId);
        int AddProject(Project Project);
        Project GetProject(int? ProjectId);
        List<Project> GetProjects();
    }
}
