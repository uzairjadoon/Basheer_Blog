using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Models
{
    public class PostReaction
    {
        public int Id { get; set; }
        public virtual ReactionType Type { get; set; }
        public int ReactionTypeId { get; set; }

        public virtual Post Post { get; set; }
        public int PostId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }


    }
}
