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
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace RentApp.Controllers
{
    [System.Web.Mvc.RequireHttps]
    public class VehicleController : ApiController
    {

        private IUnitOfWork db;
        private object locking = new object();

        public VehicleController(IUnitOfWork context)
        {
            db = context;
        }

        [AllowAnonymous]
        public IEnumerable<Vehicle> GetVehicles(/*int serviceId = 0*/)
        {
            lock (locking)
            {
                return db.Vehicles.GetAllVehicles();
            }
        }


        [AllowAnonymous]
        [Route("api/GetVehicleForService/{serviceId}")]
        public IEnumerable<Vehicle> GetVehicleForService(int serviceId)
        {
            lock (locking)
            {
                return db.Vehicles.GetVehiclesForService(serviceId);
            }
        }

        [AllowAnonymous]
        public IEnumerable<Vehicle> GetSearchVehicle(string text)
        {
            return db.Vehicles.GetSearchVehicle(text);
        }

        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetService(int id)
        {
            lock (locking)
            {
                Vehicle vehicle = db.Vehicles.Get(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                return Ok(vehicle);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleID)
            {
                return BadRequest();
            }

            try
            {
                lock (locking)
                {
                    db.Vehicles.Update(vehicle);
                    db.Complete();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiclesExists(id))
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

        [Authorize(Roles = "Admin, Manager")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle()
            {
            HttpRequestMessage request = this.Request;
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.HttpContext.Current.Server.MapPath("~/Content/images/vehicles");

            // Get the uploaded image from the Files collection
            var httpPostedFile = HttpContext.Current.Request.Files["image"];
            var vehicle = JsonConvert.DeserializeObject<Vehicle>(HttpContext.Current.Request.Form[0]);

            Validate(vehicle);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (httpPostedFile != null)
            {
                // Validate the uploaded image(optional)
                var extionsion = new FileInfo(httpPostedFile.FileName).Extension;
                var fileName = Guid.NewGuid() + extionsion;
                // Get the complete file path
                var fileSavePath = Path.Combine(root, fileName);

                while (File.Exists(fileSavePath))
                {
                    fileName = Guid.NewGuid() + extionsion;
                    fileSavePath = Path.Combine(root, fileName);
                }
                // Save the uploaded file to "UploadedFiles" folder
                httpPostedFile.SaveAs(fileSavePath);
                vehicle.Image = "http://localhost:51111/Content/images/vehicles/" + fileName;
            }
            lock (locking)
            {
                db.Vehicles.Add(vehicle);
                db.Complete();
            }
            return CreatedAtRoute("DefaultApi", new { id = vehicle.VehicleID }, vehicle);
        }

        [Authorize(Roles = "Admin, Manager")]
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            lock (locking)
            {
                Vehicle vehicle = db.Vehicles.Get(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                db.Vehicles.Remove(vehicle);
                db.Complete();

                return Ok(vehicle);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiclesExists(int id)
        {
            lock (locking)
            {
                return db.Vehicles.Get(id) != null;
            }
        }
    }
}
