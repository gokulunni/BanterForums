using Microsoft.AspNetCore.Mvc;
using BanterForums.Data;
using System.Collections.Generic;
using BanterForums.Data.Models;
using System.Linq;
using BanterForums.Models.Forum;
using BanterForums.Models.Post;
using System;

namespace BanterForums.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            /*var type is IEnumerable<ForumListingModel>*/
            var forums = _forumService.GetAll()
                    .Select(forum => new ForumListingModel {
                        Id = forum.Id,
                        Name = forum.Title,
                        Description = forum.Description
                    }); //Mapping each forum from database to a ForumListingModel Object

            var model = new ForumIndexModel
            {
                ForumList = forums
            }; //assigning the collection of the ForumListingModels to the ForumList member of the ForumIndexModel object


            return View(model); 
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
            var posts = forum.Posts;

            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };

            return View(model);
        }

        private ForumListingModel BuildForumListing(Post post)
        {
            var forum = post.Forum;

            return BuildForumListing(forum);
        }

        private ForumListingModel BuildForumListing(Forum forum)
        {
            return
                new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description,
                    ImageUrl = forum.ImageUrl
                };
        }
    }
}