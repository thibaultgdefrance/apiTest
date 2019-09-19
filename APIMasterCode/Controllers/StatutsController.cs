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
    public class StatutsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Statuts
        public IQueryable<Statut> GetStatut()
        {
            return db.Statut;
        }

        // GET: api/Statuts/5
        [ResponseType(typeof(Statut))]
        public async Task<IHttpActionResult> GetStatut(int id)
        {
            Statut statut = await db.Statut.FindAsync(id);
            if (statut == null)
            {
                return NotFound();
            }

            return Ok(statut);
        }

        // PUT: api/Statuts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStatut(int id, Statut statut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statut.IdStatut)
            {
                return BadRequest();
            }

            db.Entry(statut).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatutExists(id))
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

        // POST: api/Statuts
        [ResponseType(typeof(Statut))]
        public async Task<IHttpActionResult> PostStatut(Statut statut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statut.Add(statut);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = statut.IdStatut }, statut);
        }

        // DELETE: api/Statuts/5
        [ResponseType(typeof(Statut))]
        public async Task<IHttpActionResult> DeleteStatut(int id)
        {
            Statut statut = await db.Statut.FindAsync(id);
            if (statut == null)
            {
                return NotFound();
            }

            db.Statut.Remove(statut);
            await db.SaveChangesAsync();

            return Ok(statut);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatutExists(int id)
        {
            return db.Statut.Count(e => e.IdStatut == id) > 0;
        }
    }
}