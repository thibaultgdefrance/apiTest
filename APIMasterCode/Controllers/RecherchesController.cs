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
    public class RecherchesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Recherches
        public IQueryable<Recherche> GetRecherche()
        {
            return db.Recherche;
        }

        // GET: api/Recherches/5
        [ResponseType(typeof(Recherche))]
        public async Task<IHttpActionResult> GetRecherche(int id)
        {
            Recherche recherche = await db.Recherche.FindAsync(id);
            if (recherche == null)
            {
                return NotFound();
            }

            return Ok(recherche);
        }

        // PUT: api/Recherches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRecherche(int id, Recherche recherche)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recherche.IdRecherche)
            {
                return BadRequest();
            }

            db.Entry(recherche).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RechercheExists(id))
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

        // POST: api/Recherches
        [ResponseType(typeof(Recherche))]
        public async Task<IHttpActionResult> PostRecherche(Recherche recherche)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Recherche.Add(recherche);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = recherche.IdRecherche }, recherche);
        }

        // DELETE: api/Recherches/5
        [ResponseType(typeof(Recherche))]
        public async Task<IHttpActionResult> DeleteRecherche(int id)
        {
            Recherche recherche = await db.Recherche.FindAsync(id);
            if (recherche == null)
            {
                return NotFound();
            }

            db.Recherche.Remove(recherche);
            await db.SaveChangesAsync();

            return Ok(recherche);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RechercheExists(int id)
        {
            return db.Recherche.Count(e => e.IdRecherche == id) > 0;
        }
    }
}