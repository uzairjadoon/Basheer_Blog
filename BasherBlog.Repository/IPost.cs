using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasherBlog.Models;

namespace BasherBlog.Repository
{
    public interface IPost
    {

        //______________Post And Detail Post
        List<Post> GetPosts { get; }
        Post GetPost(int id);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(int id);
        List<PostStatus> GetPostList();


        //-------post status----
        List<PostStatus> GetPostStatuses();
        PostStatus GetPostStatus(int id);
        //-----Add post status-----------
        void AddUpdatePostStatus(PostStatus postStatus);
        void DeletePostStatus(int id);
        //-----Category---------
        List<Category> GetCategories();
        Category GetCategory(int id);
        //-----Add category------
        void AddUpdateCategory(Category category);
        //------Delete category--------
        void DeleteCategory(int id);
        //---------Post Reaction Type----
        List <ReactionType> GetReactionTypes();
       ReactionType GetReactionType(int id);
        void UpdateReactionTypes(ReactionType reactionType);
        void DeleteReactionType(int id);
        //----------post reaction------
        List<PostReaction> GetPostReactions();
        PostReaction GetPostReaction(int id);
        void CreatePostReaction(PostReaction postReaction);
        void UpdatePostReaction(PostReaction postReaction);
        void DeletePostReaction(int id);


       //---------Post Comment------

        List<PostComment> GetPostComments();
        PostComment GetPostComment(int id);

        void CreatePostComment(PostComment postComment);
        void UpdatePostComment(PostComment postComment);
        void DeletePostComment(int id);

        //------------Valid Post Post Show On Home Page-------

        List<Post> GetActivePosts();
        List<Post> GetAuthorPosts();


    }
}
