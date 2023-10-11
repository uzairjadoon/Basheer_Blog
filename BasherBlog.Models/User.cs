using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinedOn { get; set; }
        public string AccessToken { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }


    }
}
