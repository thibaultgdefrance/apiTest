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
    public class PrioritesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Priorites
        public IQueryable<Priorite> GetPriorite()
        {
            return db.Priorite;
        }

        // GET: api/Priorites/5
        [ResponseType(typeof(Priorite))]
        public async Task<IHttpActionResult> GetPriorite(int id)
        {
            Priorite priorite = await db.Priorite.FindAsync(id);
            if (priorite == null)
            {
                return NotFound();
            }

            return Ok(priorite);
        }

        // PUT: api/Priorites/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPriorite(int id, Priorite priorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priorite.IdPriorite)
            {
                return BadRequest();
            }

            db.Entry(priorite).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrioriteExists(id))
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

        // POST: api/Priorites
        [ResponseType(typeof(Priorite))]
        public async Task<IHttpActionResult> PostPriorite(Priorite priorite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Priorite.Add(priorite);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = priorite.IdPriorite }, priorite);
        }

        // DELETE: api/Priorites/5
        [ResponseType(typeof(Priorite))]
        public async Task<IHttpActionResult> DeletePriorite(int id)
        {
            Priorite priorite = await db.Priorite.FindAsync(id);
            if (priorite == null)
            {
                return NotFound();
            }

            db.Priorite.Remove(priorite);
            await db.SaveChangesAsync();

            return Ok(priorite);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrioriteExists(int id)
        {
            return db.Priorite.Count(e => e.IdPriorite == id) > 0;
        }
    }
}