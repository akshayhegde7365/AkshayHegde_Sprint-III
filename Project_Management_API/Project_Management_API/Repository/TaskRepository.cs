using Project_Management_API;
using Project_Management_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Repository
{
    public class TaskRepository : ITaskRepository
    {
        ApiContext db;
        public TaskRepository(ApiContext _db)
        {
            db = _db;
        }

        public List<Project_Management_API.Model.Task> GetTasks()
        {
            if (db != null)
            {
                return db.ProjectTasks.ToList();
            }

            return null;
        }

        public Project_Management_API.Model.Task GetTask(int? TaskId)
        {
            if (db != null)
            {
                return db.ProjectTasks.FirstOrDefault(f => f.Id == TaskId);
            }

            return null;
        }

        public int AddTask(Project_Management_API.Model.Task Task)
        {
            if (db != null)
            {
                if (db.ProjectTasks.Any())
                {
                    Task.Id = db.ProjectTasks.OrderByDescending(o => o.Id).First().Id + 1;
                }
                else
                    Task.Id = 1;

                Task.CreatedOn = DateTime.Now.ToString();
                Task.Status = 1;
                db.ProjectTasks.Add(Task);
                db.SaveChanges();

                return Task.Id;
            }
            return 0;
        }

        public int DeleteTask(int? TaskId)
        {
            int result = 0;
            var savedTask = db.ProjectTasks.FirstOrDefault(i => i.Id == TaskId);
            if (savedTask != null)
            {
                db.ProjectTasks.Remove(savedTask);
                result = db.SaveChanges();
            }
            return result;
        }


        public void UpdateTask(int id, Project_Management_API.Model.Task Task)
        {
            if (db != null)
            {
                if (Task != null || Task.Id == id)
                {
                    var savedTask = db.ProjectTasks.FirstOrDefault(i => i.Id == id);
                    if (savedTask != null)
                    {
                        savedTask.ProjectId = Task.ProjectId;
                        savedTask.Status = Task.Status;
                        savedTask.AssignedToUserId = Task.AssignedToUserId;
                        savedTask.Detail = Task.Detail;

                        db.ProjectTasks.Update(savedTask);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.ProjectTasks.Update(Task);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
