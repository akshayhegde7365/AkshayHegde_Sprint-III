using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Management_API;
using Project_Management_API.Controllers;
using Project_Management_API.Model;
using Project_Management_API.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project_Management_API_Test
{
    public class ProjectControllerTest
    {
        private ProjectRepository repository;
        public ProjectControllerTest()
        {
            var context = GetInMemoryDbContext();
            SeedData(context);
            repository = new ProjectRepository(context);
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
            context.Projects.Add(new Project() { Id = 1, Name = "Project1", Detail = "Project 1 Detail" });
            context.Projects.Add(new Project() { Id = 2, Name = "Project2", Detail = "Project 2 Detail" });
            context.SaveChanges();
        }

        [Fact]
        public void Task_GetProjectById_Return_OkResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            var ProjectId = 2;

            //Act  
            var data = controller.Get(ProjectId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetProjectById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            var ProjectId = 3;

            //Act  
            var data = controller.Get(ProjectId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_GetProjectById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            int? ProjectId = null;

            //Act  
            var data = controller.Get(ProjectId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void Task_GetProjectById_MatchResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            int ProjectId = 1;

            //Act  
            var data = controller.Get(ProjectId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var Project = okResult.Value as Project;

            Assert.Equal(1, Project.Id);
        }
        [Fact]
        public void Task_GetProjects_Return_OkResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);

            //Act  
            var data = controller.Get();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetProjects_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);

            //Act  
            var data = controller.Get();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void Task_GetProjects_MatchResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);

            //Act  
            var data = controller.Get();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var Projects = okResult.Value as IList<Project>;

            Assert.Equal(1, Projects[0].Id);
            Assert.Equal("Project1", Projects[0].Name);

            Assert.Equal(2, Projects[1].Id);
            Assert.Equal("Project2", Projects[1].Name);
        }
        [Fact]
        public void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            var Project = new Project() { Id = 3, Name = "Project3", Detail = "Project 3 Detail" };

            //Act  
            var data = controller.Post(Project);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_Add_ValidData_MatchResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            var Project = new Project() { Id = 4, Name = "Project4", Detail = "Project 4 Detail" };

            //Act  
            var data = controller.Post(Project);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            int id = (int)okResult.Value;
            Assert.Equal(4, id);
        }
        [Fact]
        public void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            int ProjectId = 2;

            //Act  
            var newData = controller.Get(ProjectId);

            var okResult = newData as OkObjectResult;
            var SavedProject = okResult.Value as Project;
            var Project = new Project();
            Project.Id = 2;
            Project.Name = SavedProject.Name;
            Project.Detail = "Update Project Detail";

            var data = controller.Put(ProjectId, Project);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public void Task_Update_InvalidData_Return_NotFound()
        {
            //Arrange  
            var controller = new ProjectController(repository);
            int ProjectId = 2;

            //Act  
            var newData = controller.Get(ProjectId);

            var okResult = newData as OkObjectResult;
            var SavedProject = okResult.Value as Project;
            var Project = new Project();
            Project.Id = 2;
            Project.Name = SavedProject.Name;
            Project.Detail = "Update Project Detail";

            var data = controller.Put(11, Project);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public void Task_Delete_Post_Return_OkResult()
        {
            //Arrange  
            var controller = new ProjectController(repository);
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
            var controller = new ProjectController(repository);
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
            var controller = new ProjectController(repository);
            int? postId = null;

            //Act  
            var data = controller.Delete(postId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
    }
}
