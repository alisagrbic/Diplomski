using RentApp.Models.Entities;
using RentApp.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RentApp.Controllers
{
    [System.Web.Mvc.RequireHttps]
    public class CommentController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        public object locking = new object();

        public CommentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Services
        public IEnumerable<Comment> GetComment()
        {
            lock (locking)
            {
                return unitOfWork.Comments.GetAllComments();
            }
        }

        // GET: api/Vehicle/1
        [Route("api/GetCommentsForService/{serviceId}")]
        public IEnumerable<Comment> GetCommentsForService(int serviceId)
        {
            lock (locking)
            {
                return unitOfWork.Comments.GetCommentsForService(serviceId);
            }
        }

        // GET: api/Services/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            lock (locking)
            {
                Comment comment = unitOfWork.Comments.Get(id);
                if (comment == null)
                {
                    return NotFound();
                }

                return Ok(comment);
            }
        }

        [Authorize(Roles = "AppUser")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.CommentID)
            {
                return BadRequest();
            }

            try
            {
                lock (locking)
                {
                    unitOfWork.Comments.Update(comment);
                    unitOfWork.Complete();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Services
        [ResponseType(typeof(Comment))]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lock (locking)
            {
                unitOfWork.Comments.Add(comment);
                unitOfWork.Complete();
            }

            return CreatedAtRoute("DefaultApi", new { id = comment.CommentID }, comment);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Comment))]
        [Authorize(Roles = "AppUser")]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = unitOfWork.Comments.Get(id);
            if (comment == null)
            {
                return NotFound();
            }
            lock (locking)
            {
                unitOfWork.Comments.Remove(comment);
                unitOfWork.Complete();

                return Ok(comment);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            lock (locking)
            {
                return unitOfWork.Comments.Get(id) != null;
            }
        }
    }
}
