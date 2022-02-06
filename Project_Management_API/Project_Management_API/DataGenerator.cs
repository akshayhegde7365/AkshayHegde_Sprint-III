using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project_Management_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
            {
                if (context.Users.Any() && context.Projects.Any() && context.ProjectTasks.Any())
                {
                    return;   // Data was already seeded
                }

                context.Users.Add(new User() { Id = 1, FirstName = "Akshay", LastName = "Hegde", EmailId = "akshay.XYZ@gmail.com" });
                context.Projects.Add(new Project() { Id = 1, Name = "PROJECT1", Detail = "Its comes under XYZ project and mainly deals with HOME Lending and Borrowing Products in X COMPANY.", CreatedOn = DateTime.Now.ToString() });
                context.ProjectTasks.Add(new Model.Task() { Id = 1, AssignedToUserId = 1, ProjectId = 1, Status = 1, Detail = "Production Issue L3", CreatedOn = DateTime.Now.ToString() });

                context.SaveChanges();
            }
        }
    }
}
