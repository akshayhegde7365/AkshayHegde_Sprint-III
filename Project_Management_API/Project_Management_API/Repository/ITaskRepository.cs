using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Repository
{
    public interface ITaskRepository
    {
        void UpdateTask(int id, Project_Management_API.Model.Task Task);
        int DeleteTask(int? TaskId);
        int AddTask(Project_Management_API.Model.Task Task);
        Project_Management_API.Model.Task GetTask(int? TaskId);
        List<Project_Management_API.Model.Task> GetTasks();
    }
}
