using BanterForums.Data;
using BanterForums.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanterForums.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Create(Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.Forums.Include(forum => forum.Posts); 
            //need to explicitly indicate we want to get Posts since it is a virtual function in the Forum class definition
        }

        public IEnumerable<ApplicationUser> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }

        public Forum GetById(int id)
        {
            var forum = _context.Forums.Where(f => f.Id == id) //get forum by the primary key (Id)
                //Include posts and the user made the posts
                .Include(f => f.Posts).ThenInclude(p => p.User)
                //Include replies to posts with the user who made the reply
                .Include(f => f.Posts).ThenInclude(p => p.Replies).ThenInclude(r => r.User)
                //Make sure we return a single instance of a forum and not a collection
                .FirstOrDefault();

            return forum;
        }   

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
