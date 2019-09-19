using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APIMasterCode.Models;

namespace APIMasterCode.Controllers
{
    public class PublicationsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Publications
        public IQueryable<Publication> GetPublication()
        {
            return db.Publication;
        }

        // GET: api/Publications/5
        [ResponseType(typeof(Publication))]
        public async Task<IHttpActionResult> GetPublication(int id)
        {
            Publication publication = await db.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            return Ok(publication);
        }

        // PUT: api/Publications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPublication(int id, Publication publication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publication.IdPublication)
            {
                return BadRequest();
            }

            db.Entry(publication).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(id))
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

        // POST: api/Publications
        [ResponseType(typeof(Publication))]
        public async Task<IHttpActionResult> PostPublication(Publication publication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Publication.Add(publication);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = publication.IdPublication }, publication);
        }

        // DELETE: api/Publications/5
        [ResponseType(typeof(Publication))]
        public async Task<IHttpActionResult> DeletePublication(int id)
        {
            Publication publication = await db.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            db.Publication.Remove(publication);
            await db.SaveChangesAsync();

            return Ok(publication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PublicationExists(int id)
        {
            return db.Publication.Count(e => e.IdPublication == id) > 0;
        }
    }
}