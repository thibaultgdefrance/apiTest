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
    public class MotClefsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/MotClefs
        public IQueryable<MotClef> GetMotClef()
        {
            return db.MotClef;
        }

        // GET: api/MotClefs/5
        [ResponseType(typeof(MotClef))]
        public async Task<IHttpActionResult> GetMotClef(int id)
        {
            MotClef motClef = await db.MotClef.FindAsync(id);
            if (motClef == null)
            {
                return NotFound();
            }

            return Ok(motClef);
        }

        // PUT: api/MotClefs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMotClef(int id, MotClef motClef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != motClef.IdMotClef)
            {
                return BadRequest();
            }

            db.Entry(motClef).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotClefExists(id))
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

        // POST: api/MotClefs
        [ResponseType(typeof(MotClef))]
        public async Task<IHttpActionResult> PostMotClef(MotClef motClef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MotClef.Add(motClef);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = motClef.IdMotClef }, motClef);
        }

        // DELETE: api/MotClefs/5
        [ResponseType(typeof(MotClef))]
        public async Task<IHttpActionResult> DeleteMotClef(int id)
        {
            MotClef motClef = await db.MotClef.FindAsync(id);
            if (motClef == null)
            {
                return NotFound();
            }

            db.MotClef.Remove(motClef);
            await db.SaveChangesAsync();

            return Ok(motClef);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MotClefExists(int id)
        {
            return db.MotClef.Count(e => e.IdMotClef == id) > 0;
        }
    }
}