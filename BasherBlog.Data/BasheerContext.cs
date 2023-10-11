using BasherBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Data
{
    public class BasheerContext : DbContext
    {
        public BasheerContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<User> Users{ get; set;}
        public DbSet<UserRole> UserRoles{ get; set;}
        public DbSet<Post> Posts{ get; set;}
        public DbSet<Category> Categories{ get; set;}
        public DbSet<PostComment> postComments{ get; set;}
        public DbSet<PostReaction> postReactions{ get; set;}
        public DbSet<PostStatus> postStatuses{ get; set;}
        public DbSet<ReactionType> reactionTypes{ get; set;}
    }
}
