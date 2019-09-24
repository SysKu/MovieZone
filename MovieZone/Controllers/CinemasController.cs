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
    public class CinemasController : ApiController
    {
        private MovieZoneEntities db = new MovieZoneEntities();

        // GET: api/Cinemas
        public IQueryable<Cinema> GetCinema()
        {
            return db.Cinema;
        }

        // GET: api/Cinemas/5
        [ResponseType(typeof(Cinema))]
        public IHttpActionResult GetCinema(int id)
        {
            Cinema cinema = db.Cinema.Find(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

        // PUT: api/Cinemas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCinema(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cinema.CinemaID)
            {
                return BadRequest();
            }

            db.Entry(cinema).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(id))
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

        // POST: api/Cinemas
        [ResponseType(typeof(Cinema))]
        public IHttpActionResult PostCinema(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cinema.Add(cinema);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CinemaExists(cinema.CinemaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cinema.CinemaID }, cinema);
        }

        // DELETE: api/Cinemas/5
        [ResponseType(typeof(Cinema))]
        public IHttpActionResult DeleteCinema(int id)
        {
            Cinema cinema = db.Cinema.Find(id);
            if (cinema == null)
            {
                return NotFound();
            }

            db.Cinema.Remove(cinema);
            db.SaveChanges();

            return Ok(cinema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CinemaExists(int id)
        {
            return db.Cinema.Count(e => e.CinemaID == id) > 0;
        }
    }
}