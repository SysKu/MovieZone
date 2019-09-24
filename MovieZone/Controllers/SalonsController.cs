using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MovieZone.Models;

namespace MovieZone.Controllers
{
    public class SalonsController : ApiController
    {
        private MovieZoneEntities db = new MovieZoneEntities();

        // GET: api/Salons
        public IQueryable<Salon> GetSalon()
        {
            return db.Salon;
        }

        // GET: api/Salons/5
        [ResponseType(typeof(Salon))]
        public IHttpActionResult GetSalon(int id)
        {
            Salon salon = db.Salon.Find(id);
            if (salon == null)
            {
                return NotFound();
            }

            return Ok(salon);
        }

        // PUT: api/Salons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSalon(int id, Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salon.SalonID)
            {
                return BadRequest();
            }

            db.Entry(salon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalonExists(id))
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

        // POST: api/Salons
        [ResponseType(typeof(Salon))]
        public IHttpActionResult PostSalon(Salon salon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Salon.Add(salon);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SalonExists(salon.SalonID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = salon.SalonID }, salon);
        }

        // DELETE: api/Salons/5
        [ResponseType(typeof(Salon))]
        public IHttpActionResult DeleteSalon(int id)
        {
            Salon salon = db.Salon.Find(id);
            if (salon == null)
            {
                return NotFound();
            }

            db.Salon.Remove(salon);
            db.SaveChanges();

            return Ok(salon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalonExists(int id)
        {
            return db.Salon.Count(e => e.SalonID == id) > 0;
        }
    }
}