using RentApp.Models.Entities;
using RentApp.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentApp.Persistance.Repository
{
    public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        protected RADBContext Context { get { return context as RADBContext; } }

        public CommentRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<Comment> GetAllComments()
        {
            return Context.Comments.Where(p => p.Deleted != true).Include(c => c.Client).ToList();
        }

        public IEnumerable<Comment> GetCommentsForService(int serviceId)
        {
            return Context.Comments.Where(p => p.ServiceID == serviceId && !p.Deleted).Include(c => c.Client).ToList();
        }
    }
}