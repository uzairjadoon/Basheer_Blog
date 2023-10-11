using BasherBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Repository
{
    public interface IUserAccount
    {

        User GetUserForLogin(string email, string password);

        string Register(User user);

        User GetUserInfo(string accessToken);
    }

    
    
}
