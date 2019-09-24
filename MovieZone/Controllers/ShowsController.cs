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
    public class ShowsController : ApiController
    {
        private MovieZoneEntities db = new MovieZoneEntities();

        // GET: api/Shows
        public IQueryable<Shows> GetShows()
        {
            return db.Shows;
        }

        // GET: api/Shows/5
        [ResponseType(typeof(Shows))]
        public IHttpActionResult GetShows(int id)
        {
            Shows shows = db.Shows.Find(id);
            if (shows == null)
            {
                return NotFound();
            }

            return Ok(shows);
        }

        // PUT: api/Shows/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShows(int id, Shows shows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shows.ShowID)
            {
                return BadRequest();
            }

            db.Entry(shows).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowsExists(id))
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

        // POST: api/Shows
        [ResponseType(typeof(Shows))]
        public IHttpActionResult PostShows(Shows shows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shows.Add(shows);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ShowsExists(shows.ShowID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = shows.ShowID }, shows);
        }

        // DELETE: api/Shows/5
        [ResponseType(typeof(Shows))]
        public IHttpActionResult DeleteShows(int id)
        {
            Shows shows = db.Shows.Find(id);
            if (shows == null)
            {
                return NotFound();
            }

            db.Shows.Remove(shows);
            db.SaveChanges();

            return Ok(shows);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShowsExists(int id)
        {
            return db.Shows.Count(e => e.ShowID == id) > 0;
        }
    }
}