using BasherBlog.Data;
using BasherBlog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasherBlog.Repository.Implementation
{
    public class PostRepository : IPost
    {
        private readonly BasheerContext _db;
        public PostRepository(BasheerContext db)
        {
            _db = db;
        }

        //-----------------Post And Post Details
        public List<Post> GetPosts
        {
            get { return _db.Posts.Include(x=>x.Category).Include(x=>x.PostStatus).ToList(); }
        }

       

        public Post GetPost(int id) {
            return _db.Posts.Where(x => x.Id.Equals(id)).Include(x => x.Category).Include(x => x.PostStatus).Include(x => x.User).FirstOrDefault();
        }
        
        //-----------------Post Add Data-----------------

        public void CreatePost( Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }
        public void UpdatePost( Post post)
        {
            _db.Posts.Update(post);
            _db.SaveChanges();
        }
        //______--------PostStatus List---------
        public List<PostStatus> GetPostList()
        {
            return _db.postStatuses.ToList();
        }
        //______--------Delete Post---------
        public void DeletePost(int id)
        {
            Post deletePost = _db.Posts.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(deletePost);
            _db.SaveChanges();
        }
        public List<PostStatus> GetPostStatuses()
        {
            return _db.postStatuses.ToList();
        }
        public PostStatus GetPostStatus(int id)
        {
            return _db.postStatuses.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
        //---addupdate post status--------------
        public void AddUpdatePostStatus(PostStatus postStatus)
        {
            _db.postStatuses.Update(postStatus);
            _db.SaveChanges();
        }
        //----------delete post status---------
        public void DeletePostStatus(int id)
        {
            PostStatus postStatus = _db.postStatuses.Where(x => x.Id.Equals(id)).FirstOrDefault();
            ///PostStatus postStatus = _db.PostStatuses.FirstOrDefault(x => x.Id.Equals(id));/
            _db.Remove(postStatus);
            _db.SaveChanges();
        }
        //---------Category--------
        public List<Category> GetCategories()
        {
            return _db.Categories.ToList();
        }
        public Category GetCategory(int id)
        {
            return _db.Categories.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
        public void AddUpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
        //----------Delete Category------
        public void DeleteCategory(int id)
        {
            Category category = _db.Categories.Where(x => x.Id.Equals(id)).FirstOrDefault();
            ///PostStatus postStatus = _db.PostStatuses.FirstOrDefault(x => x.Id.Equals(id));/
            _db.Remove(category);
            _db.SaveChanges();
        }
        //--------Reaction type--------
        public List<ReactionType> GetReactionTypes()
        {
           return _db.reactionTypes.ToList();
        }
        public ReactionType GetReactionType(int id)
        {
            return _db.reactionTypes.Where(x => x.Id == id).FirstOrDefault();
        }
        public void UpdateReactionTypes(ReactionType reactionType)
        {
            _db.reactionTypes.Update(reactionType);
            _db.SaveChanges();
        }
        public void DeleteReactionType(int id)
        {
           ReactionType reactionType = _db.reactionTypes.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _db.Remove(reactionType);
            _db.SaveChanges();
        }

        public List<PostReaction> GetPostReactions()
        {
            return _db.postReactions.Include(x=> x.User).Include(x=>x.Post).Include(x=> x.Type).ToList();
        }
        public PostReaction GetPostReaction(int id)
        {
            return _db.postReactions.Where(x => x.Id == id).Include(x => x.User).Include(x => x.Post).Include(x => x.Type).FirstOrDefault();
        }

        public void CreatePostReaction(PostReaction postReaction)
        {
            _db.postReactions.Add(postReaction);
            _db.SaveChanges();
        }

        public void UpdatePostReaction(PostReaction postReaction)
        {
          _db.postReactions.Update(postReaction);
           _db.SaveChanges();
        }
        public void DeletePostReaction(int id)
        {
            PostReaction postReaction = _db.postReactions.Where(_x => _x.Id == id).FirstOrDefault();
            _db.Remove(postReaction);
            _db.SaveChanges();
        }
        //------------PostReaction------------------

        /*   public List<PostReaction> GetPostReaction()
           {
               return _db.postReactions.ToList();
           }*/


        //----------Post Comment----
        public List<PostComment> GetPostComments()
        {
            return _db.postComments.Include(x =>x.User).Include(x => x.Post).ToList();
        }
        public PostComment GetPostComment(int id)
        {
            return _db.postComments.Include(x => x.User).Include(x => x.Post).Where(x => x.Id == id).FirstOrDefault();
        }
        public void CreatePostComment(PostComment postComment)
        {
           _db.postComments.Add(postComment);
            _db.SaveChanges();
        }
        public void UpdatePostComment(PostComment postComment)
        {
            _db.postComments.Update(postComment);
            _db.SaveChanges();
        }
        public void DeletePostComment(int id)
        {
          PostComment postComment = GetPostComment(id);
            _db.postComments.Remove(postComment);
            _db.SaveChanges();
        }
        //-----------Show Active Posts-----&&----- Author Implementaion-
        public List<Post> GetActivePosts()
        {
            return _db.Posts.Where(x => x.PostStatus.Name.ToLower().Equals("active")).ToList();
        }        
        
        public List<Post> GetAuthorPosts()
        {
            return _db.Posts.Where(x => x.User.UserRoleId == 2003).Include(x => x.Category).Include(x => x.PostStatus).ToList();
        }
    }
}
