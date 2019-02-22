using Microsoft.AspNetCore.Mvc;
using BanterForums.Data;
using System.Collections.Generic;
using BanterForums.Data.Models;
using System.Linq;
using BanterForums.Models.Forum;

namespace BanterForums.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }
        public IActionResult Index()
        {
            /*IEnumerable<ForumListingModel>*/
            var forums = _forumService.GetAll()
                    .Select(forum => new ForumListingModel {
                        Id = forum.Id,
                        Name = forum.Title,
                        Description = forum.Description
                    });

            var model = new ForumIndexModel
            {
                ForumList = forums
            };

            return View(model);
        }

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);
        }

    }
}