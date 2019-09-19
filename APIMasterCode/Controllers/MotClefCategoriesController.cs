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
    public class MotClefCategoriesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/MotClefCategories
        public IQueryable<MotClefCategorie> GetMotClefCategorie()
        {
            return db.MotClefCategorie;
        }

        // GET: api/MotClefCategories/5
        [ResponseType(typeof(MotClefCategorie))]
        public async Task<IHttpActionResult> GetMotClefCategorie(int id)
        {
            MotClefCategorie motClefCategorie = await db.MotClefCategorie.FindAsync(id);
            if (motClefCategorie == null)
            {
                return NotFound();
            }

            return Ok(motClefCategorie);
        }

        // PUT: api/MotClefCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMotClefCategorie(int id, MotClefCategorie motClefCategorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != motClefCategorie.IdMotClefCategorie)
            {
                return BadRequest();
            }

            db.Entry(motClefCategorie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotClefCategorieExists(id))
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

        // POST: api/MotClefCategories
        [ResponseType(typeof(MotClefCategorie))]
        public async Task<IHttpActionResult> PostMotClefCategorie(MotClefCategorie motClefCategorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MotClefCategorie.Add(motClefCategorie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = motClefCategorie.IdMotClefCategorie }, motClefCategorie);
        }

        // DELETE: api/MotClefCategories/5
        [ResponseType(typeof(MotClefCategorie))]
        public async Task<IHttpActionResult> DeleteMotClefCategorie(int id)
        {
            MotClefCategorie motClefCategorie = await db.MotClefCategorie.FindAsync(id);
            if (motClefCategorie == null)
            {
                return NotFound();
            }

            db.MotClefCategorie.Remove(motClefCategorie);
            await db.SaveChangesAsync();

            return Ok(motClefCategorie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MotClefCategorieExists(int id)
        {
            return db.MotClefCategorie.Count(e => e.IdMotClefCategorie == id) > 0;
        }
    }
}