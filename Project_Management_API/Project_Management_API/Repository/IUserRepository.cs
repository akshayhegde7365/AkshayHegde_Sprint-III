using Project_Management_API.Model;
using System.Collections.Generic;

namespace Project_Management_API.Repository
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void UpdateUser(int id, User user);
        int DeleteUser(int? UserId);
        int AddUser(User User);
        User GetUser(int? UserId);
        List<User> GetUsers();
    }
}