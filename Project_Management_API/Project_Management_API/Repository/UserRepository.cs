using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Project_Management_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Repository
{
    public class UserRepository : IUserRepository
    {
        ApiContext db;
        public UserRepository(ApiContext _db)
        {
            db = _db;
        }

        public List<User> GetUsers()
        {
            if (db != null)
            {
                return db.Users.ToList();
            }

            return null;
        }

        public User GetUser(int? UserId)
        {
            if (db != null)
            {
                return db.Users.FirstOrDefault(f => f.Id == UserId);
            }

            return null;
        }

        public int AddUser(User User)
        {
            if (db != null)
            {
                db.Users.Add(User);
                db.SaveChanges();

                return User.Id;
            }
            return 0;
        }

        public int DeleteUser(int? UserId)
        {
            int result = 0;
            var savedUser = db.Users.FirstOrDefault(i => i.Id == UserId);
            if (savedUser != null)
            {
                db.Users.Remove(savedUser);
                result = db.SaveChanges();
            }
            return result;
        }


        public void UpdateUser(int id , User user)
        {
            if (db != null)
            {
                if (user != null || user.Id == id)
                {
                    var savedUser = db.Users.FirstOrDefault(i => i.Id == id);
                    if (savedUser != null)
                    {
                        savedUser.FirstName = user.FirstName;
                        savedUser.LastName = user.LastName;
                        savedUser.EmailId = user.EmailId;

                        db.Users.Update(savedUser);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Users.Update(user);
                        db.SaveChanges();
                    }
                }
            }
        }
        public User GetUserByEmail(string email)
        {
            if (db != null)
            {
                return db.Users.FirstOrDefault(f => f.EmailId == email);
            }

            return null;
        }
    }
}
