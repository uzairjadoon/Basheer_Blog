using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Models
{
    public class Post

    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string shortDescription { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
        public DateTime PostedOn { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual PostStatus PostStatus { get; set; }

        public int PostStatusId { get; set; }


    }
}
