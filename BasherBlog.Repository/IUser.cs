using BasherBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Repository
{
    public interface IUser
    {
        public List<UserRole> GetRoles();

        UserRole GetRole(int id);

        void AddUpdateRole(UserRole userRole);

        void DeleteRole(int id);

        //-----------Users
        public List<User> GetUsers();

        public User GetUser(int id); 
        
        //----------AddUpdateUser--------

        void AddUpdateUser(User user);

        List<UserRole> GetRolesList();
        //----------DltUser--------

        void DeleteUser(int id);
    }
}
