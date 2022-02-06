using Project_Management_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Repository
{
    public class ProjectRepository :IProjectRepository
    {
        ApiContext db;
        public ProjectRepository(ApiContext _db)
        {
            db = _db;
        }

        public List<Project> GetProjects()
        {
            if (db != null)
            {
                return db.Projects.ToList();
            }

            return null;
        }

        public Project GetProject(int? ProjectId)
        {
            if (db != null)
            {
                return db.Projects.FirstOrDefault(f => f.Id == ProjectId);
            }

            return null;
        }

        public int AddProject(Project Project)
        {
            if (db != null)
            {
                db.Projects.Add(Project);
                db.SaveChanges();

                return Project.Id;
            }
            return 0;
        }

        public int DeleteProject(int? ProjectId)
        {
            int result = 0;
            var savedProject = db.Projects.FirstOrDefault(i => i.Id == ProjectId);
            if (savedProject != null)
            {
                db.Projects.Remove(savedProject);
                result = db.SaveChanges();
            }
            return result;
        }


        public void UpdateProject(int id, Project Project)
        {
            if (db != null)
            {
                if (Project != null || Project.Id == id)
                {
                    var savedProject = db.Projects.FirstOrDefault(i => i.Id == id);
                    if (savedProject != null)
                    {
                        savedProject.Name = Project.Name;
                        savedProject.Detail = Project.Detail;

                        db.Projects.Update(savedProject);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Projects.Update(Project);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
