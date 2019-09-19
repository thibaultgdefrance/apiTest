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
    public class CommentairesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Commentaires
        public IQueryable<Commentaire> GetCommentaire()
        {
            return db.Commentaire;
        }

        // GET: api/Commentaires/5
        [ResponseType(typeof(Commentaire))]
        public async Task<IHttpActionResult> GetCommentaire(int id)
        {
            Commentaire commentaire = await db.Commentaire.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }

            return Ok(commentaire);
        }

        // PUT: api/Commentaires/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCommentaire(int id, Commentaire commentaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commentaire.IdCommentaire)
            {
                return BadRequest();
            }

            db.Entry(commentaire).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentaireExists(id))
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

        // POST: api/Commentaires
        [ResponseType(typeof(Commentaire))]
        public async Task<IHttpActionResult> PostCommentaire(Commentaire commentaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Commentaire.Add(commentaire);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = commentaire.IdCommentaire }, commentaire);
        }

        // DELETE: api/Commentaires/5
        [ResponseType(typeof(Commentaire))]
        public async Task<IHttpActionResult> DeleteCommentaire(int id)
        {
            Commentaire commentaire = await db.Commentaire.FindAsync(id);
            if (commentaire == null)
            {
                return NotFound();
            }

            db.Commentaire.Remove(commentaire);
            await db.SaveChangesAsync();

            return Ok(commentaire);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentaireExists(int id)
        {
            return db.Commentaire.Count(e => e.IdCommentaire == id) > 0;
        }
    }
}