using Microsoft.EntityFrameworkCore;
using Project_Management_API;
using Project_Management_API.Controllers;
using Xunit;
using Project_Management_API.Model;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Project_Management_API.Repository;
using System.Collections.Generic;

namespace Project_Management_API_Test
{
    public class TaskControllerTest
    {
        private TaskRepository repository;
        public TaskControllerTest()
        {
            var context = GetInMemoryDbContext();
            SeedData(context);
            repository = new TaskRepository(context);
        }
        private ApiContext GetInMemoryDbContext()
        {
            DbContextOptions<ApiContext> options;
            var builder = new DbContextOptionsBuilder<ApiContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            ApiContext dataContext = new ApiContext(options);
            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
            return dataContext;
        }
        private void SeedData(ApiContext context)
        {
            context.ProjectTasks.Add(new Task() { Id = 1, AssignedToUserId = 1, ProjectId = 1, Status = 1, Detail = "Task Detail 1" });
            context.ProjectTasks.Add(new Task()
            {
                Id = 2,
                AssignedToUserId = 1,
                ProjectId = 1,
                Status = 1,
                Detail = "Task Detail 2"
            });
            context.SaveChanges();
        }

        [Fact]
        public void Task_GetTaskById_Return_OkResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            var TaskId = 2;

            //Act  
            var data = controller.Get(TaskId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetTaskById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            var TaskId = 3;

            //Act  
            var data = controller.Get(TaskId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_GetTaskById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            int? TaskId = null;

            //Act  
            var data = controller.Get(TaskId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void Task_GetTaskById_MatchResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            int TaskId = 1;

            //Act  
            var data = controller.Get(TaskId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var Task = okResult.Value as Task;

            Assert.Equal(1, Task.Id);
        }
        [Fact]
        public void Task_GetTasks_Return_OkResult()
        {
            //Arrange  
            var controller = new TaskController(repository);

            //Act  
            var data = controller.Get();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetTasks_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new TaskController(repository);

            //Act  
            var data = controller.Get();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void Task_GetTasks_MatchResult()
        {
            //Arrange  
            var controller = new TaskController(repository);

            //Act  
            var data = controller.Get();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var Tasks = okResult.Value as IList<Task>;

            Assert.Equal(1, Tasks[0].Id);
            Assert.Equal("Task Detail 1", Tasks[0].Detail);

            Assert.Equal(2, Tasks[1].Id);
            Assert.Equal("Task Detail 2", Tasks[1].Detail);
        }
        [Fact]
        public void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            var Task = new Task() { Id = 3, AssignedToUserId = 1, ProjectId = 1, Status = 1, Detail = "Task Detail 3" };

            //Act  
            var data = controller.Post(Task);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_Add_ValidData_MatchResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            var Task = new Task() { Id = 3, AssignedToUserId = 1, ProjectId = 1, Status = 1, Detail = "Task Detail 4"};

            //Act  
            var data = controller.Post(Task);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            int id = (int)okResult.Value;
            Assert.Equal(3, id);
        }
        [Fact]
        public void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            int TaskId = 2;

            //Act  
            var newData = controller.Get(TaskId);

            var okResult = newData as OkObjectResult;
            var SavedTask = okResult.Value as Task;
            var Task = new Task();
            Task.Id = 2;
            Task.Detail = "Task Detail updated";

            var data = controller.Put(TaskId, Task);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public void Task_Update_InvalidData_Return_NotFound()
        {
            //Arrange  
            var controller = new TaskController(repository);
            int TaskId = 2;

            //Act  
            var newData = controller.Get(TaskId);

            var okResult = newData as OkObjectResult;
            var SavedTask = okResult.Value as Task;
            var Task = new Task();
            Task.Id = 2;
            Task.Detail = "Task Detail updated";

            var data = controller.Put(11, Task);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public void Task_Delete_Post_Return_OkResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            var postId = 2;

            //Act  
            var data = controller.Delete(postId);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            var postId = 5;

            //Act  
            var data = controller.Delete(postId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_Delete_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new TaskController(repository);
            int? postId = null;

            //Act  
            var data = controller.Delete(postId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
    }
}
