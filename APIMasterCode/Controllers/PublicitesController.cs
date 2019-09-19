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
    public class PublicitesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Publicites
        public IQueryable<Publicite> GetPublicite()
        {
            return db.Publicite;
        }

        // GET: api/Publicites/5
        [ResponseType(typeof(Publicite))]
        public async Task<IHttpActionResult> GetPublicite(int id)
        {
            Publicite publicite = await db.Publicite.FindAsync(id);
            if (publicite == null)
            {
                return NotFound();
            }

            return Ok(publicite);
        }

        // PUT: api/Publicites/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPublicite(int id, Publicite publicite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publicite.IdPublicite)
            {
                return BadRequest();
            }

            db.Entry(publicite).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PubliciteExists(id))
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

        // POST: api/Publicites
        [ResponseType(typeof(Publicite))]
        public async Task<IHttpActionResult> PostPublicite(Publicite publicite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Publicite.Add(publicite);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = publicite.IdPublicite }, publicite);
        }

        // DELETE: api/Publicites/5
        [ResponseType(typeof(Publicite))]
        public async Task<IHttpActionResult> DeletePublicite(int id)
        {
            Publicite publicite = await db.Publicite.FindAsync(id);
            if (publicite == null)
            {
                return NotFound();
            }

            db.Publicite.Remove(publicite);
            await db.SaveChangesAsync();

            return Ok(publicite);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PubliciteExists(int id)
        {
            return db.Publicite.Count(e => e.IdPublicite == id) > 0;
        }
    }
}