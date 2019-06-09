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
    public class ItemController : ApiController
    {

        private IUnitOfWork db;
        private object locking = new object();

        public ItemController(IUnitOfWork context)
        {
            db = context;
        }

        // GET: api/Services
        [Route("api/GetItemVehicleId/{VehicleId}")]
        public IEnumerable<Item> GetItemVehicleId(int VehicleId)
        {
            lock (locking)
            {
                return db.Items.GetItemForVehicle(VehicleId);
            }
        }

        // GET: api/Services/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetService(int id)
        {
            lock (locking)
            {
                Item item = db.Items.Get(id);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutService(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.ItemID)
            {
                return BadRequest();
            }

            try
            {
                lock (locking)
                {
                    db.Items.Update(item);
                    db.Complete();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostService(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lock (locking)
            {
                db.Items.Add(item);
                db.Complete();
            }

            return CreatedAtRoute("DefaultApi", new { id = item.ItemID }, item);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteService(int id)
        {
            Item item = db.Items.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            lock (locking)
            {
                db.Items.Remove(item);
                db.Complete();
            }
            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Get(id) != null;
        }
    }
}
