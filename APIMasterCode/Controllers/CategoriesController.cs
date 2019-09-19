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
using Cryptage2;

namespace APIMasterCode.Controllers
{
    public class CategoriesController : ApiController
    {
        ClefDeCryptage2 clef = new ClefDeCryptage2();
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Categories
        public IQueryable<Categorie> GetCategorie()
        {
            return db.Categorie;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Categorie))]
        public async Task<IHttpActionResult> GetCategorie(int id)
        {
            Categorie categorie = await db.Categorie.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            return Ok(categorie);
        }
        public async Task<IHttpActionResult> GetCategorie(string token, int id)
        {
            if (clef.verify(token)==true)
            {
                Categorie categorie = await db.Categorie.FindAsync(id);
                if (categorie == null)
                {
                    return NotFound();
                }

                return Ok(categorie);
            }
            else
            {
                return null;
            }
           
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategorie(int id, Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categorie.IdCategorie)
            {
                return BadRequest();
            }

            db.Entry(categorie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategorieExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Categorie))]
        public async Task<IHttpActionResult> PostCategorie(Categorie categorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorie.Add(categorie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = categorie.IdCategorie }, categorie);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Categorie))]
        public async Task<IHttpActionResult> DeleteCategorie(int id)
        {
            Categorie categorie = await db.Categorie.FindAsync(id);
            if (categorie == null)
            {
                return NotFound();
            }

            db.Categorie.Remove(categorie);
            await db.SaveChangesAsync();

            return Ok(categorie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategorieExists(int id)
        {
            return db.Categorie.Count(e => e.IdCategorie == id) > 0;
        }

    }
}