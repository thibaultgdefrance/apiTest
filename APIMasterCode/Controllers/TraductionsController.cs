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
    public class TraductionsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Traductions
        public IQueryable<Traduction> GetTraduction()
        {
            return db.Traduction;
        }

        // GET: api/Traductions/5
        [ResponseType(typeof(Traduction))]
        public async Task<IHttpActionResult> GetTraduction(int id)
        {
            Traduction traduction = await db.Traduction.FindAsync(id);
            if (traduction == null)
            {
                return NotFound();
            }

            return Ok(traduction);
        }

        // PUT: api/Traductions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTraduction(int id, Traduction traduction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traduction.IdTraduction)
            {
                return BadRequest();
            }

            db.Entry(traduction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraductionExists(id))
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

        // POST: api/Traductions
        [ResponseType(typeof(Traduction))]
        public async Task<IHttpActionResult> PostTraduction(Traduction traduction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Traduction.Add(traduction);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = traduction.IdTraduction }, traduction);
        }

        // DELETE: api/Traductions/5
        [ResponseType(typeof(Traduction))]
        public async Task<IHttpActionResult> DeleteTraduction(int id)
        {
            Traduction traduction = await db.Traduction.FindAsync(id);
            if (traduction == null)
            {
                return NotFound();
            }

            db.Traduction.Remove(traduction);
            await db.SaveChangesAsync();

            return Ok(traduction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TraductionExists(int id)
        {
            return db.Traduction.Count(e => e.IdTraduction == id) > 0;
        }
    }
}