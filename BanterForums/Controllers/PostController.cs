using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BanterForums.Data;
using BanterForums.Data.Models;
using BanterForums.Models.Post;
using BanterForums.Models.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BanterForums.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private static UserManager<ApplicationUser> _userManager; //UserManager is built-in and gives us the APIs for interacting with the user
        public PostController(IPost postservice, IForum forumservice, UserManager<ApplicationUser> userManager)
        {
            _postService = postservice;
            _forumService = forumservice;
            _userManager = userManager;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies);
            var model = new PostIndexModel
            {
                Id = post.Id,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                Title = post.Title,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                PostContent = post.Content

            };
            return View(model);
        }

        public IActionResult Create(int id)
        {
            //Note that id is Forum.id

            var forum = _forumService.GetById(id);
            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name /*we are using the ClaimsPrincipal "User" object to be able to access 
                                                the User in the current http context to get their userName*/
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result; //result extension ensues user is not null by waiting for a result
            var post = BuildPost(model, user);

             _postService.Add(post).Wait(); //Blocks the current thread and waits until the task is complete

            //TO DO: User rating management

            return RedirectToAction("Index", "Post", new { id = post.Id });
        }

        private Post BuildPost(NewPostModel model, ApplicationUser user)
        {
            var forum = _forumService.GetById(model.ForumId);
            return new Post
            {
                Title = model.Title,
                Content = model.Content,
                Created = DateTime.Now,
                User = user
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel 
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content
            });
        }
    }
}