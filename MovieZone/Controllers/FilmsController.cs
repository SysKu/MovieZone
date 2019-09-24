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
    public class FilmsController : ApiController
    {
        private MovieZoneEntities db = new MovieZoneEntities();

        // GET: api/Films
        public IQueryable<Film> GetFilm()
        {
            return db.Film;
        }

        // GET: api/Films/5
        [ResponseType(typeof(Film))]
        public IHttpActionResult GetFilm(int id)
        {
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return NotFound();
            }

            return Ok(film);
        }

        // PUT: api/Films/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFilm(int id, Film film)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != film.FilmID)
            {
                return BadRequest();
            }

            db.Entry(film).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        // POST: api/Films
        [ResponseType(typeof(Film))]
        public IHttpActionResult PostFilm(Film film)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Film.Add(film);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FilmExists(film.FilmID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = film.FilmID }, film);
        }

        // DELETE: api/Films/5
        [ResponseType(typeof(Film))]
        public IHttpActionResult DeleteFilm(int id)
        {
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return NotFound();
            }

            db.Film.Remove(film);
            db.SaveChanges();

            return Ok(film);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FilmExists(int id)
        {
            return db.Film.Count(e => e.FilmID == id) > 0;
        }
    }
}