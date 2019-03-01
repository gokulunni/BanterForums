using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BanterForums.Models;
using System;
using BanterForums.Data;
using System.Linq;
using BanterForums.Models.Home;
using BanterForums.Models.Post;
using BanterForums.Data.Models;
using BanterForums.Models.Forum;

namespace BanterForums.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;
        public HomeController(IPost PostService)
        {
            _postService = PostService;
        }
        public IActionResult Index()
        {

            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latestPosts = _postService.GetLatestPosts(10);
            var posts = latestPosts.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = GetForumListingForPost(post)

            });

            return new HomeIndexModel
            {
                LatestPosts = posts,
                SearchQuery = string.Empty //DONT FORGET TO IMPLEMENT THIS

            };
        }

        private ForumListingModel GetForumListingForPost(Post post)
        {
            var forum = post.Forum;
            return new ForumListingModel
            {
               Name = forum.Title,
               Id = forum.Id,
               ImageUrl = forum.ImageUrl
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
