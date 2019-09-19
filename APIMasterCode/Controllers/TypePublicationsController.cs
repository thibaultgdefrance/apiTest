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
    public class TypePublicationsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/TypePublications
        public IQueryable<TypePublication> GetTypePublication()
        {
            return db.TypePublication;
        }

        // GET: api/TypePublications/5
        [ResponseType(typeof(TypePublication))]
        public async Task<IHttpActionResult> GetTypePublication(int id)
        {
            TypePublication typePublication = await db.TypePublication.FindAsync(id);
            if (typePublication == null)
            {
                return NotFound();
            }

            return Ok(typePublication);
        }

        // PUT: api/TypePublications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypePublication(int id, TypePublication typePublication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typePublication.IdTypePublication)
            {
                return BadRequest();
            }

            db.Entry(typePublication).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypePublicationExists(id))
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

        // POST: api/TypePublications
        [ResponseType(typeof(TypePublication))]
        public async Task<IHttpActionResult> PostTypePublication(TypePublication typePublication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypePublication.Add(typePublication);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typePublication.IdTypePublication }, typePublication);
        }

        // DELETE: api/TypePublications/5
        [ResponseType(typeof(TypePublication))]
        public async Task<IHttpActionResult> DeleteTypePublication(int id)
        {
            TypePublication typePublication = await db.TypePublication.FindAsync(id);
            if (typePublication == null)
            {
                return NotFound();
            }

            db.TypePublication.Remove(typePublication);
            await db.SaveChangesAsync();

            return Ok(typePublication);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypePublicationExists(int id)
        {
            return db.TypePublication.Count(e => e.IdTypePublication == id) > 0;
        }
    }
}