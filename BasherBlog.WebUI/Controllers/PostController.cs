using BasherBlog.Models;
using BasherBlog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Drawing;

namespace BasherBlog.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _post;
        private readonly IUserAccount _account;
        public PostController(IPost post, IUserAccount account)
        {
            _post = post;
            _account = account;
        }

        [AdminOrAuthor]
        [HttpGet]
        public IActionResult GetPosts()
        {
            var user = new CommonController(_account).GetUser(HttpContext);
            if (user.UserRoleId == 2002)
            {
                return View(_post.GetPosts);
            }
            else if (user.UserRoleId == 2003)
            {
                return View(_post.GetAuthorPosts());
            }
            return View();
        }

        [AdminOrAuthor]
        [HttpGet]
        public IActionResult DetailsPost(int id)
        {
            return View(_post.GetPost(id));
        }
        [AdminOrAuthor]
        [HttpGet]
        public IActionResult CreatePost()
        {
            var user = new CommonController(_account).GetUser(HttpContext);
            ViewBag.UserId = user.UserRoleId;
            
            if(user.UserRoleId == 2003)
            {
                ViewBag.Categories = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
                return View(new Post());
            }

            ViewBag.Categories = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
            ViewBag.PostStatus = new SelectList(_post.GetPostList().ToList(), "Id", "Name");
            return View(new Post());
        }

        [AdminOrAuthor]
        [HttpPost]
        public IActionResult CreatePost(Post post, IFormFile PostImage)
        {
            string imagepath = "";
            var extension = "";
            IList<String> allowfileextension = new List<string> { ".jpg", "jpeg", ".png" };
            int maxContentLehgth = 1024 * 1024 * 10; //10mb file
            if (PostImage != null && PostImage.Length > 0)
            {
                extension = PostImage.FileName.Substring(PostImage.FileName.LastIndexOf('.')).ToLower();
                if (PostImage.Length > maxContentLehgth)
                {
                    ViewBag.error = "File Must Be Less Then 10MB";
                }
                else if (!allowfileextension.Contains(extension)) {
                    ViewBag.error = "Please Upload Image Of Type .Jpg , .jpeg , .png";
                }
                else
                {
                    string relativeImagePath=$"/Images/posts/{post.Id}-{Path.GetFileNameWithoutExtension(PostImage.FileName)}-{DateTime.UtcNow.Ticks}.jpg";
                    string absoluteImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{relativeImagePath}");
                    using (var stream = new FileStream(absoluteImagePath, FileMode.Create))
                    {
                        using (var memorystream = new MemoryStream())
                        {
                            PostImage.CopyTo(memorystream);
                            using (var img = Image.FromStream(memorystream))
                            {
                                int width = img.Width;
                                int height = img.Height;
                                if (width > 2000 || height > 1280)
                                {
                                    ViewBag.error = "Please Upload Image With Dimension 2000*1280 or less";
                                }
                                else
                                {
                                    PostImage.CopyTo(stream);
                                    post.Image = relativeImagePath;
                                    if (post.UserId == 0)
                                    {
                                        var user = new CommonController(_account).GetUser(HttpContext);
                                        post.UserId = user.Id;
                                    }
                                  
                                    _post.CreatePost(post);
                                    return RedirectToAction("GetPosts");
                                }
                            }
                        }
                    }
                }
            }
            return View();
            //post.PostedOn = DateTime.UtcNow.AddHours(5);

        }
        [AdminOrAuthor]      
        [HttpGet]
        public IActionResult UpdatePost(int id = 0)
        {
            ViewBag.Categories = new SelectList(_post.GetCategories().ToList(), "Id", "Name");
            ViewBag.PostStatus = new SelectList(_post.GetPostList().ToList(), "Id", "Name");
            return View(_post.GetPost(id));
        }
        [AdminOrAuthor]    
        [HttpPost]
        public IActionResult UpdatePost(Post post)
        {
            _post.UpdatePost(post);
            return RedirectToAction("GetPosts");
        }
        [AdminOrAuthor]
        [HttpGet]
        public IActionResult DeletePost(int id) {
            _post.DeletePost(id);
            return RedirectToAction("GetPosts");
        }
        [Admin]
      
        [HttpGet]
        public IActionResult PostStatus()
        {
            return View(_post.GetPostStatuses());
        }
        //------ADD POST STATUS
        [Admin]
       
        [HttpGet]
        public IActionResult AddUpdatePostStatus(int id = 0)
        {
            return View(_post.GetPostStatus(id));
        }
        [Admin]
       
        [HttpPost]
        public IActionResult AddUpdatePostStatus(PostStatus postStatus)
        {
            _post.AddUpdatePostStatus(postStatus);
            return RedirectToAction("PostStatus");
        }
        //----Delete post status-----------
        [Admin]
        [HttpGet]
        public IActionResult DeletePostStatus(int id)
        {
            _post.DeletePostStatus(id);
            return RedirectToAction("PostStatus");
        }
        //-------Category--------
        [Admin]
       
        [HttpGet]
        public IActionResult Category()
        {
            return View(_post.GetCategories());
        }
        //------ADD CATEGORY---------
        [Admin]
       
        [HttpGet]
        public IActionResult AddUpdateCategory(int id = 0)
        {
            return View(_post.GetCategory(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult AddUpdateCategory(Category category)
        {
            _post.AddUpdateCategory(category);
            return RedirectToAction("Category");
        }
        //-------Delete category
        [Admin]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            _post.DeleteCategory(id);
            return RedirectToAction("Category");
        }
        //-------Reaction Types-----------
        [Admin]
        [HttpGet]
        public IActionResult ReactionTypes() {
            return View(_post.GetReactionTypes());
        }
        [Admin]
        [HttpGet]
        public IActionResult UpdateReactionTypes(int id = 0)
        {
            return View(_post.GetReactionType(id));
        }

        [Admin]
        [HttpPost]
        public IActionResult UpdateReactionTypes(ReactionType reactionType)
        {
            _post.UpdateReactionTypes(reactionType);
            return RedirectToAction("ReactionTypes");
        }
        [Admin]
        [HttpGet]
        public IActionResult DeleteReactionType(int id)
        {
            _post.DeleteReactionType(id);
            return RedirectToAction("ReactionTypes");
        }
        [Admin]
        [HttpGet]
        public IActionResult PostReactions()
        {
            return View(_post.GetPostReactions());
        }
        [Admin]
        [HttpGet]
        public IActionResult GetPostReaction(int id = 0)
        {
            return View(_post.GetPostReaction(id));
        }
        [Admin]
        [HttpGet]
        public IActionResult CreatePostReaction()
        {
            ViewBag.Posts = new SelectList(_post.GetPosts.ToList(), "Id", "Title");
            ViewBag.Reactions = new SelectList(_post.GetReactionTypes().ToList(), "Id", "Name");
            return View(new PostReaction());
        }
        [Admin]
        [HttpPost]
        public IActionResult CreatePostReaction(PostReaction postReaction)
        {
            User user = new CommonController(_account).GetUser(HttpContext);
            postReaction.UserId = user.Id;

            _post.CreatePostReaction(postReaction);
            return RedirectToAction("PostReactions");
        }
        [Admin]
        [HttpGet]
        public IActionResult UpdatePostReaction(int id = 0)
        {
            ViewBag.Posts = new SelectList(_post.GetPosts.ToList(), "Id", "Title");
            ViewBag.Reactions = new SelectList(_post.GetReactionTypes().ToList(), "Id", "Name");
            return View(_post.GetPostReaction(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult UpdatePostReaction(PostReaction postReaction)
        {
            _post.UpdatePostReaction(postReaction);
            return RedirectToAction("PostReactions");
        }
        [Admin]
        [HttpGet]
        public IActionResult DeletePostReaction(int id) 
        { 
            _post.DeletePostReaction(id);
            return RedirectToAction("PostReactions");
        }

        //------------------Post Comments---------
        [Admin]
        [HttpGet]
        public IActionResult PostComments()
        {
            return View(_post.GetPostComments());
        }
        [Admin]
        [HttpGet]
        public IActionResult GetPostComment(int id=0)
        {
            return View(_post.GetPostComment(id));
        }
        [Admin]
        [HttpGet]
        public IActionResult CreatePostComment()
        {
            ViewBag.Posts=new SelectList(_post.GetPosts.ToList(), "Id", "Title");
            return View(new PostComment());
        }
        [Admin]
        [HttpPost]
        public IActionResult CreatePostComment(PostComment postComment)
        {
            User user = new CommonController(_account).GetUser(HttpContext);
            postComment.UserId=user.Id;
            _post.CreatePostComment(postComment);
            return RedirectToAction("PostComments");
        }
        [Admin]
        [HttpGet]
        public IActionResult UpdatePostComment(int id=0)
        {
            ViewBag.Posts = new SelectList(_post.GetPosts.ToList(), "Id", "Title");
            return View(_post.GetPostComment(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult UpdatePostComment(PostComment postComment)
        {
           _post.UpdatePostComment(postComment);
            return RedirectToAction("PostComments");
        }
        [Admin]
        [HttpGet]
        public IActionResult DeletePostComment(int id)
        {
            _post.DeletePostComment(id);
            return RedirectToAction("PostComments");
        }


    }
}
