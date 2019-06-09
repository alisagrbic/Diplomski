using Newtonsoft.Json;
using RentApp.Models.Entities;
using RentApp.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace RentApp.Controllers
{
    public class AppUsersController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private Object locking = new Object();


        public AppUsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Services
        public IEnumerable<AppUser> GetAppUsers()
        {
            return unitOfWork.AppUsers.GetAll();
        }

        
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(int id)
        {
            lock (locking)
            {
                AppUser appUser = unitOfWork.AppUsers.Get(id);
                if (appUser == null)
                {
                    return NotFound();
                }
                return Ok(appUser);
            }
        }

        [Route("api/GetActiveUserId")]
        [Authorize(Roles = "Admin, Manager, AppUser")]
        public int GetActiveUserId()
        {
            lock (locking)
            {
                return unitOfWork.AppUsers.GetActiveUserId(User.Identity.Name);
            }
        }

      
        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppUser(int id, AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appUser.Id)
            {
                return BadRequest();
            }

            try
            {
                
                string root = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/users");
                var extionsion = new FileInfo(appUser.Image).Extension;
                var fileName = Guid.NewGuid() + extionsion;
                var fileSavePath = Path.Combine(root, fileName);

                while (File.Exists(fileSavePath))
                {
                    fileName = Guid.NewGuid() + extionsion;
                    fileSavePath = Path.Combine(root, fileName);
                }
                appUser.Image = "http://localhost:51111/Content/images/users/" + fileName;

                lock (locking)
                {
                    unitOfWork.AppUsers.Update(appUser);
                    unitOfWork.Complete();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lock (locking)
            {
                unitOfWork.AppUsers.Add(appUser);
                unitOfWork.Complete();
            }
            return CreatedAtRoute("DefaultApi", new { id = appUser.Id }, appUser);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(AppUser))]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteAppUser(int id)
        {
            lock (locking)
            {
                AppUser appUser = unitOfWork.AppUsers.Get(id);
                if (appUser == null)
                {
                    return NotFound();
                }

                unitOfWork.AppUsers.Remove(appUser);
                unitOfWork.Complete();

                return Ok(appUser);
            }
        }

        [ResponseType(typeof(AppUser))]
        public async Task<IHttpActionResult> PutAppUserPhoto()
        {
            AppUser user = new AppUser();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string rooot = HttpContext.Current.Server.MapPath("~/Content/images/");
            var provider = new MultipartFormDataStreamProvider(rooot);

            try
            {
                var f = HttpContext.Current.Request.Files[0];
                FileInfo ff = new FileInfo(f.FileName);
                var fileName = Guid.NewGuid() + ff.Extension;
                var fullPath = rooot + fileName;

                if (File.Exists(fullPath))
                {
                    fileName = Guid.NewGuid() + ff.Extension;
                    fullPath = rooot + fileName;
                }

                var relativePath = "/Content/images/";
                f.SaveAs(fullPath);


                if (HttpContext.Current.Request.Form.Count > 0)
                {
                    user = JsonConvert.DeserializeObject<AppUser>(HttpContext.Current.Request.Form[0]);
                    user.Image = relativePath + fileName;
                }
                else
                {
                    //formData se nije popunio
                }
            }
            catch (System.Exception e)
            {

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            unitOfWork.AppUsers.Update(user);
            unitOfWork.Complete();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserExists(int id)
        {
            lock (locking)
            {
                return unitOfWork.AppUsers.Get(id) != null;
            }
        }
    }
}
