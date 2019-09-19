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
    public class TropheesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Trophees
        public IQueryable<Trophee> GetTrophee()
        {
            return db.Trophee;
        }

        // GET: api/Trophees/5
        [ResponseType(typeof(Trophee))]
        public async Task<IHttpActionResult> GetTrophee(int id)
        {
            Trophee trophee = await db.Trophee.FindAsync(id);
            if (trophee == null)
            {
                return NotFound();
            }

            return Ok(trophee);
        }

        // PUT: api/Trophees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTrophee(int id, Trophee trophee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trophee.IdTrophee)
            {
                return BadRequest();
            }

            db.Entry(trophee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TropheeExists(id))
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

        // POST: api/Trophees
        [ResponseType(typeof(Trophee))]
        public async Task<IHttpActionResult> PostTrophee(Trophee trophee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trophee.Add(trophee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = trophee.IdTrophee }, trophee);
        }

        // DELETE: api/Trophees/5
        [ResponseType(typeof(Trophee))]
        public async Task<IHttpActionResult> DeleteTrophee(int id)
        {
            Trophee trophee = await db.Trophee.FindAsync(id);
            if (trophee == null)
            {
                return NotFound();
            }

            db.Trophee.Remove(trophee);
            await db.SaveChangesAsync();

            return Ok(trophee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TropheeExists(int id)
        {
            return db.Trophee.Count(e => e.IdTrophee == id) > 0;
        }
    }
}