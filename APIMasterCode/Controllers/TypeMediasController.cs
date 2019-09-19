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
    public class TypeMediasController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/TypeMedias
        public IQueryable<TypeMedia> GetTypeMedia()
        {
            return db.TypeMedia;
        }

        // GET: api/TypeMedias/5
        [ResponseType(typeof(TypeMedia))]
        public async Task<IHttpActionResult> GetTypeMedia(int id)
        {
            TypeMedia typeMedia = await db.TypeMedia.FindAsync(id);
            if (typeMedia == null)
            {
                return NotFound();
            }

            return Ok(typeMedia);
        }

        // PUT: api/TypeMedias/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypeMedia(int id, TypeMedia typeMedia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeMedia.IdTypeMedia)
            {
                return BadRequest();
            }

            db.Entry(typeMedia).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeMediaExists(id))
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

        // POST: api/TypeMedias
        [ResponseType(typeof(TypeMedia))]
        public async Task<IHttpActionResult> PostTypeMedia(TypeMedia typeMedia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeMedia.Add(typeMedia);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typeMedia.IdTypeMedia }, typeMedia);
        }

        // DELETE: api/TypeMedias/5
        [ResponseType(typeof(TypeMedia))]
        public async Task<IHttpActionResult> DeleteTypeMedia(int id)
        {
            TypeMedia typeMedia = await db.TypeMedia.FindAsync(id);
            if (typeMedia == null)
            {
                return NotFound();
            }

            db.TypeMedia.Remove(typeMedia);
            await db.SaveChangesAsync();

            return Ok(typeMedia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeMediaExists(int id)
        {
            return db.TypeMedia.Count(e => e.IdTypeMedia == id) > 0;
        }
    }
}