using BasherBlog.Data;
using BasherBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Repository.Implementation
{
    public class UserRepository : IUser
    {
        private readonly BasheerContext _db;

        public UserRepository(BasheerContext db)
        {
            _db = db;
        }

        public void AddUpdateRole(UserRole userRole)
        {
            _db.UserRoles.Update(userRole);
            _db.SaveChanges();
        }

        public void DeleteRole(int id)
        {
            UserRole userRole = _db.UserRoles.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(userRole);
            _db.SaveChanges();
        }

        public UserRole GetRole(int id)
        {
            return _db.UserRoles.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<UserRole> GetRoles()
        {
            return _db.UserRoles.ToList();


        }

        //------------------Users-------------
        public List<User> GetUsers()
        {

            return _db.Users.Include(x => x.UserRole).ToList();

        }

        public User GetUser(int id)
        {
            return _db.Users.Where(x => x.Id == id).FirstOrDefault();
        }


        public void DeleteUser(int id)
        {
            User deleteUser = _db.Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(deleteUser);
            _db.SaveChanges();
        }
        //______________AddUpdateUser
        public void AddUpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();

        }

        //-----------UpdateList

       public List <UserRole> GetRolesList()
        {
            return _db.UserRoles.ToList();
        }
    }
}
