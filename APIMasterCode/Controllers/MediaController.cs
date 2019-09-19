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
    public class MediaController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Media
        public IQueryable<Media> GetMedia()
        {
            return db.Media;
        }

        // GET: api/Media/5
        [ResponseType(typeof(Media))]
        public async Task<IHttpActionResult> GetMedia(int id)
        {
            Media media = await db.Media.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            return Ok(media);
        }

        // PUT: api/Media/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedia(int id, Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != media.IdMedia)
            {
                return BadRequest();
            }

            db.Entry(media).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
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

        // POST: api/Media
        [ResponseType(typeof(Media))]
        public async Task<IHttpActionResult> PostMedia(Media media)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Media.Add(media);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = media.IdMedia }, media);
        }

        // DELETE: api/Media/5
        [ResponseType(typeof(Media))]
        public async Task<IHttpActionResult> DeleteMedia(int id)
        {
            Media media = await db.Media.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            db.Media.Remove(media);
            await db.SaveChangesAsync();

            return Ok(media);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MediaExists(int id)
        {
            return db.Media.Count(e => e.IdMedia == id) > 0;
        }
    }
}