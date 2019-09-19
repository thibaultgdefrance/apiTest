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
    public class LanguesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Langues
        public IQueryable<Langue> GetLangue()
        {
            return db.Langue;
        }

        // GET: api/Langues/5
        [ResponseType(typeof(Langue))]
        public async Task<IHttpActionResult> GetLangue(int id)
        {
            Langue langue = await db.Langue.FindAsync(id);
            if (langue == null)
            {
                return NotFound();
            }

            return Ok(langue);
        }

        // PUT: api/Langues/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLangue(int id, Langue langue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != langue.IdLangue)
            {
                return BadRequest();
            }

            db.Entry(langue).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LangueExists(id))
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

        // POST: api/Langues
        [ResponseType(typeof(Langue))]
        public async Task<IHttpActionResult> PostLangue(Langue langue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Langue.Add(langue);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = langue.IdLangue }, langue);
        }

        // DELETE: api/Langues/5
        [ResponseType(typeof(Langue))]
        public async Task<IHttpActionResult> DeleteLangue(int id)
        {
            Langue langue = await db.Langue.FindAsync(id);
            if (langue == null)
            {
                return NotFound();
            }

            db.Langue.Remove(langue);
            await db.SaveChangesAsync();

            return Ok(langue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LangueExists(int id)
        {
            return db.Langue.Count(e => e.IdLangue == id) > 0;
        }
    }
}