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
    public class MessageriesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Messageries
        public IQueryable<Messagerie> GetMessagerie()
        {
            return db.Messagerie;
        }

        // GET: api/Messageries/5
        [ResponseType(typeof(Messagerie))]
        public async Task<IHttpActionResult> GetMessagerie(int id)
        {
            Messagerie messagerie = await db.Messagerie.FindAsync(id);
            if (messagerie == null)
            {
                return NotFound();
            }

            return Ok(messagerie);
        }

        // PUT: api/Messageries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessagerie(int id, Messagerie messagerie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messagerie.IdMessagerie)
            {
                return BadRequest();
            }

            db.Entry(messagerie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagerieExists(id))
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

        // POST: api/Messageries
        [ResponseType(typeof(Messagerie))]
        public async Task<IHttpActionResult> PostMessagerie(Messagerie messagerie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messagerie.Add(messagerie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = messagerie.IdMessagerie }, messagerie);
        }

        // DELETE: api/Messageries/5
        [ResponseType(typeof(Messagerie))]
        public async Task<IHttpActionResult> DeleteMessagerie(int id)
        {
            Messagerie messagerie = await db.Messagerie.FindAsync(id);
            if (messagerie == null)
            {
                return NotFound();
            }

            db.Messagerie.Remove(messagerie);
            await db.SaveChangesAsync();

            return Ok(messagerie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessagerieExists(int id)
        {
            return db.Messagerie.Count(e => e.IdMessagerie == id) > 0;
        }
    }
}