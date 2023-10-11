using BasherBlog.Data;
using BasherBlog.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Repository.Implementation
{
    public class AccountRepository : IUserAccount
    {
        private readonly BasheerContext _db;

        public AccountRepository(BasheerContext db)
        {
            _db = db;
        }

        public string Register(User user)
        {
            user.UserRoleId = 2005;
            user.IsConfirmed = false;
            user.JoinedOn = DateTime.UtcNow.AddHours(5);
            user.AccessToken = Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks;
            _db.Users.Add(user);
            _db.SaveChanges();
            return user.AccessToken+user.JoinedOn.Ticks.ToString();
        }
        public User GetUserForLogin(string email, string password)
        {
            return _db.Users.Where(x => x.EmailAddress.ToLower().Equals(email.ToLower()) && x.Password.Equals(password)).FirstOrDefault();
        }

        public User GetUserInfo(string accessToken)
        {
            return _db.Users.Where(x => x.AccessToken.Equals(accessToken)).FirstOrDefault();
        }
    }
}
