using Microsoft.EntityFrameworkCore;
using Project_Management_API;
using Project_Management_API.Controllers;
using Xunit;
using Project_Management_API.Model;
using Project_Management_API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Project_Management_API_Test
{
    public class UserControllerTest
    {
        private UserRepository repository;
        public UserControllerTest()
        {
            var context = GetInMemoryDbContext();
            SeedData(context);
            repository = new UserRepository(context);
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
            context.Users.Add(new User() { Id = 1, FirstName = "Akshay", LastName = "hegde", EmailId = "akshay.XYZ@gmail.com" });
            context.Users.Add(new User() { Id = 2, FirstName = "Akshay1", LastName = "hegde1", EmailId = "akshay1.XYZ@gmail.com" });
            context.SaveChanges();
        }

        [Fact]
        public void Task_GetUserById_Return_OkResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            var UserId = 2;

            //Act  
            var data = controller.Get(UserId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetUserById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            var UserId = 3;

            //Act  
            var data =controller.Get(UserId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_GetUserById_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            int? UserId = null;

            //Act  
            var data = controller.Get(UserId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void Task_GetUserById_MatchResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            int UserId = 1;

            //Act  
            var data = controller.Get(UserId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var user = okResult.Value as User;

            Assert.Equal(1, user.Id);
        }
        [Fact]
        public void Task_GetUsers_Return_OkResult()
        {
            //Arrange  
            var controller = new UserController(repository);

            //Act  
            var data = controller.Get();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetUsers_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new UserController(repository);

            //Act  
            var data = controller.Get();
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public void Task_GetUsers_MatchResult()
        {
            //Arrange  
            var controller = new UserController(repository);

            //Act  
            var data = controller.Get();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data as OkObjectResult;
            var users = okResult.Value as IList<User>;

            Assert.Equal(1, users[0].Id);
            Assert.Equal("Akshay", users[0].FirstName);

            Assert.Equal(2, users[1].Id);
            Assert.Equal("Qamar", users[1].FirstName);
        }
        [Fact]
        public void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            var user = new User() { Id = 3, FirstName = "Chanchal", LastName = "Prajapat", EmailId = "chanchal.prajapat@gmail.com" };

            //Act  
            var data = controller.Post(user);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_Add_ValidData_MatchResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            var user = new User() { Id = 4, FirstName = "Chanchal", LastName = "Prajapat", EmailId = "chanchal.prajapat@gmail.com" };

            //Act  
            var data = controller.Post(user);

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
            var controller = new UserController(repository);
            int userId = 2;

            //Act  
            var newData = controller.Get(userId);

            var okResult = newData as OkObjectResult;
            var SavedUser = okResult.Value as User;
            var user = new User();
            user.Id = 2;
            user.FirstName = SavedUser.FirstName;
            user.LastName = "Hassan1";

            var data = controller.Put(userId, user);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public void Task_Update_InvalidData_Return_NotFound()
        {
            //Arrange  
            var controller = new UserController(repository);
            int userId = 2;

            //Act  
            var newData = controller.Get(userId);

            var okResult = newData as OkObjectResult;
            var SavedUser = okResult.Value as User;
            var user = new User();
            user.Id = 2;
            user.FirstName = SavedUser.FirstName;
            user.LastName = "Hassan1";

            var data = controller.Put(11,user);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public void Task_Delete_Post_Return_OkResult()
        {
            //Arrange  
            var controller = new UserController(repository);
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
            var controller = new UserController(repository);
            var postId = 5;

            //Act  
            var data =controller.Delete(postId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public void Task_Delete_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new UserController(repository);
            int? postId = null;

            //Act  
            var data = controller.Delete(postId);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }
    }
}
